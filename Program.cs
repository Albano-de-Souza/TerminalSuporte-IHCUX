using System;
using System.Threading;

namespace TerminalSuporte.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Heurística #6: Reconhecimento em vez de Recordação (Menu Visível)
            while (true)
            {
                ExibirMenuPrincipal();
                Console.Write("Digite um comando: ");
                string entrada = Console.ReadLine();
                string comando = (entrada ?? "").ToLower().Trim();

                switch (comando)
                {
                    case "ping":
                        ExecutarPing();
                        break;
                    case "reset":
                        ExecutarReset();
                        break;
                    case "help":
                    case "?":
                        ExibirAjuda();
                        break;
                    case "sair":
                        return;
                    default:
                        // UX Writing: Mensagem instrutiva e clara
                        NotificarErro($"Comando '{comando}' não reconhecido. Digite 'HELP' para ajuda.");
                        break;
                }
            }
        }

        static void ExibirMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("Terminal de Diagnóstico v2.0");
            Console.WriteLine("STATUS DO SISTEMA: [OPERACIONAL]");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("COMANDOS DISPONÍVEIS:");
            Console.WriteLine("> [PING]  - Testar conexão");
            Console.WriteLine("> [RESET] - Reiniciar servidor (Crítico)");
            Console.WriteLine("> [HELP]  - Ajuda rápida");
            Console.WriteLine("> [SAIR]  - Fechar terminal");
            Console.WriteLine("-----------------------------------\n");
        }

        static void ExecutarPing()
        {
            Console.Clear();
            Console.WriteLine("=== DIAGNÓSTICO DE REDE ===");
            // Heurística #10: Documentação contextual
            Console.WriteLine("Formato esperado: 192.168.0.1 (Somente números e pontos)");
            Console.Write("Digite o IP de destino: ");
            string ip = Console.ReadLine();

            // Heurística #5: Prevenção de Erros (Validação simples)
            if (string.IsNullOrWhiteSpace(ip) || !ip.Contains("."))
            {
                NotificarErro("IP Inválido! Certifique-se de usar o formato correto (ex: 127.0.0.1).");
                return;
            }

            Console.WriteLine($"\n[IHC] Enviando pacotes para {ip}...");
            Thread.Sleep(1500); 
            
            Console.ForegroundColor = ConsoleColor.Green; // Gestão de Cores (Sucesso)
            Console.WriteLine("Resposta recebida com sucesso! Latência: 15ms.");
            Console.ResetColor();
            
            Console.WriteLine("\nPressione qualquer tecla para retornar...");
            Console.ReadKey();
        }

        static void ExecutarReset()
        {
            Console.Clear();
            // Gestão de Cores: Vermelho para Perigo/Atenção
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("!!! AVISO DE SEGURANÇA !!!");
            Console.ResetColor();

            Console.WriteLine("\nVocê solicitou o REBOOT do servidor central.");
            Console.WriteLine("Isso desconectará todos os usuários ativos.");
            
            // Heurística #5: Confirmação extra antes de ação crítica
            Console.Write("Tem certeza que deseja continuar? (S/N): ");
            string confirma = (Console.ReadLine() ?? "").ToUpper();

            if (confirma == "S")
            {
                Console.WriteLine("\nReiniciando sistema...");
                Thread.Sleep(2000);
                Console.WriteLine("Servidor Online.");
            }
            else
            {
                Console.WriteLine("\nOperação cancelada pelo usuário.");
            }
            Console.ReadKey();
        }

        static void ExibirAjuda()
        {
            Console.Clear();
            Console.WriteLine("=== CENTRAL DE AJUDA ==="); 
            Console.WriteLine("PING:  Verifica se um servidor está respondendo.");
            Console.WriteLine("RESET: Desliga e liga o servidor (Uso restrito).");
            Console.WriteLine("SAIR:  Encerra a sessão atual com segurança.");
            Console.WriteLine("\nPressione qualquer tecla para retornar.");
            Console.ReadKey();
        }

        static void NotificarErro(string mensagem)
        {
            // Pulo do Gato: Erro em vermelho é percebido mais rápido
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nERRO: {mensagem}");
            Console.ResetColor();
            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }
    }
}