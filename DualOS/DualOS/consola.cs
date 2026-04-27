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
            Console.WriteLine("File System Commands");
            Console.ResetColor();

            PrintCommand("disks", "Show available disks");
            PrintCommand("peek", "List directories and files");
            PrintCommand("jump <path>", "Change directory");
            PrintCommand("forge <name>", "Create directory");
            PrintCommand("wipe <name>", "Delete directory");
            PrintCommand("write <file> <text>", "Write file");
            PrintCommand("read <file>", "Read file");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("System Commands");
            Console.ResetColor();

            PrintCommand("guide", "Show help");
            PrintCommand("origin", "Show version");
            PrintCommand("clearvoid", "Clear screen");
            PrintCommand("shutdown off", "Shutdown system");
            PrintCommand("shutdown reboot", "Restart system");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Calculator");
            Console.ResetColor();

            PrintCommand("calc add a b", "Add numbers");
            PrintCommand("calc sub a b", "Subtract");
            PrintCommand("calc mul a b", "Multiply");
            PrintCommand("calc div a b", "Divide");
            PrintCommand("calc mod a b", "Modulo");
            PrintCommand("calc sqrt a", "Square root");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("History");
            Console.ResetColor();

            PrintCommand("history", "Show last 5 commands");
            PrintCommand("!n", "Execute command from history");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Examples:");
            Console.ResetColor();

            Console.WriteLine("  history");
            Console.WriteLine("  !0");
            Console.WriteLine("  calc add 5 3");

            Console.ResetColor();
        }

        private static void PrintCommand(string command, string description)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  " + command);

            if (command.Length < 25)
                Console.Write(new string(' ', 25 - command.Length));
            else
                Console.Write("  ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("- " + description);
            Console.ResetColor();
        }
    }
}