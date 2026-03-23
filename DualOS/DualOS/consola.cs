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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Examples:");
            Console.ResetColor();

            Console.WriteLine("  peek");
            Console.WriteLine("  jump docs");
            Console.WriteLine("  jump ..");
            Console.WriteLine("  forge projects");
            Console.WriteLine("  wipe projects");
            Console.WriteLine("  read notes.txt");
            Console.WriteLine("  say Hello DualOS");
            Console.WriteLine("  shutdown off");
            Console.WriteLine("  shutdown reboot");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Tip: use command names exactly as shown.");
            Console.ResetColor();
        }

        private static void PrintCommand(string command, string description)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  " + command);

            if (command.Length < 18)
            {
                Console.Write(new string(' ', 18 - command.Length));
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