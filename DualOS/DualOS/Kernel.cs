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

            input = input.ToLower();

            switch (input)
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

                default:
                    Console.WriteLine("Unknown command. Type 'guide' for help.");
                    break;
            }
        }
    }
}