using System;

namespace DualOS
{
    public static class Consola
    {
        public static void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==============================================");
            Console.WriteLine("                DualOS Help                   ");
            Console.WriteLine("==============================================");
            Console.ResetColor();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("File and Directory Commands");
            Console.ResetColor();

            PrintCommand("peek", "List the contents of the current directory");
            PrintCommand("jump <path>", "Change directory");
            PrintCommand("forge <name>", "Create a new directory");
            PrintCommand("wipe <name>", "Delete an empty directory");
            PrintCommand("read <file>", "Show the contents of a file");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("System Information");
            Console.ResetColor();

            PrintCommand("guide", "Show this help screen");
            PrintCommand("origin", "Show OS name and version");
            PrintCommand("pulse", "Show available and total memory");
            PrintCommand("uptick", "Show system uptime");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Utility Commands");
            Console.ResetColor();

            PrintCommand("clearvoid", "Clear the screen");
            PrintCommand("say <text>", "Print text to the screen");
            PrintCommand("shutdown off", "Power off the system");
            PrintCommand("shutdown reboot", "Restart the system");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Calculator Commands");
            Console.ResetColor();

            PrintCommand("calc add a b", "Add two numbers");
            PrintCommand("calc sub a b", "Subtract two numbers");
            PrintCommand("calc mul a b", "Multiply two numbers");
            PrintCommand("calc div a b", "Divide two numbers");
            PrintCommand("calc mod a b", "Modulo of two numbers");
            PrintCommand("calc sqrt a", "Square root of a number");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Examples:");
            Console.ResetColor();

            Console.WriteLine("  guide");
            Console.WriteLine("  origin");
            Console.WriteLine("  shutdown off");
            Console.WriteLine("  shutdown reboot");
            Console.WriteLine("  calc add 5 3");
            Console.WriteLine("  calc sub 9 2");
            Console.WriteLine("  calc mul 4 6");
            Console.WriteLine("  calc div 8 2");
            Console.WriteLine("  calc mod 10 3");
            Console.WriteLine("  calc sqrt 25");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Tip: use command names exactly as shown.");
            Console.ResetColor();
        }

        private static void PrintCommand(string command, string description)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  " + command);

            if (command.Length < 20)
            {
                Console.Write(new string(' ', 20 - command.Length));
            }
            else
            {
                Console.Write("  ");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("- " + description);
            Console.ResetColor();
        }
    }
}