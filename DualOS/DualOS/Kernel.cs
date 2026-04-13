using System;
using Sys = Cosmos.System;

namespace DualOS
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Utilities.PrintDualOSLogo();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("DualOS started successfully!");
            Console.ResetColor();
        }

        protected override void Run()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("DualOS> ");
            Console.ResetColor();

            string input = Console.ReadLine();

            if (input == null || input.Trim() == "")
            {
                return;
            }

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0].ToLower();

            switch (command)
            {
                case "guide":
                    Consola.ShowHelp();
                    break;

                case "clearvoid":
                    Console.Clear();
                    break;

                case "origin":
                    Console.WriteLine("DualOS v1.0");
                    break;

                case "shutdown":
                    HandleShutdown(parts);
                    break;

                case "calc":
                    Calculadora.Execute(parts);
                    break;

                default:
                    Console.WriteLine("Unknown command. Type 'guide' for help.");
                    break;
            }
        }

        private void HandleShutdown(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Usage: shutdown off | shutdown reboot");
                return;
            }

            string option = parts[1].ToLower();

            switch (option)
            {
                case "off":
                    Sys.Power.Shutdown();
                    break;

                case "reboot":
                    Sys.Power.Reboot();
                    break;

                default:
                    Console.WriteLine("Invalid option. Use: shutdown off | shutdown reboot");
                    break;
            }
        }
    }
}