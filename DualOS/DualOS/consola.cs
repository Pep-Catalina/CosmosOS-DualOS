using System;

namespace DualOS{
    public static class Consola{
        public static void ShowHelp(){
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
            PrintCommand("peek", "List directories and files in root (0:\\)");
            PrintCommand("jump <path>", "[TODO] Change directory");
            PrintCommand("forge <name>", "Create a directory");
            PrintCommand("wipe <name>", "Delete an empty directory");
            PrintCommand("write <file> <text>", "Create/write a file");
            PrintCommand("read <file>", "Read a file");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("System Information");
            Console.ResetColor();

            PrintCommand("guide", "Show this help screen");
            PrintCommand("origin", "Show OS name and version");
            PrintCommand("pulse", "[TODO] Show available and total memory");
            PrintCommand("uptick", "[TODO] Show system uptime");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Utility Commands");
            Console.ResetColor();

            PrintCommand("clearvoid", "Clear the screen");
            PrintCommand("say <text>", "[TODO] Print text to the screen");
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

            Console.WriteLine("  disks");
            Console.WriteLine("  peek");
            Console.WriteLine("  forge test");
            Console.WriteLine("  write hola.txt Hola DualOS");
            Console.WriteLine("  read hola.txt");
            Console.WriteLine("  wipe test");
            Console.WriteLine("  jump docs");
            Console.WriteLine("  say Hello DualOS");
            Console.WriteLine("  shutdown off");
            Console.WriteLine("  calc add 5 3");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Tip: all file operations currently use root path 0:\\");
            Console.WriteLine("Commands marked with [TODO] are not implemented yet.");
            Console.ResetColor();
         }   

        private static void PrintCommand(string command, string description){
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  " + command);

            if (command.Length < 25){
                Console.Write(new string(' ', 25 - command.Length));}
            else{
                Console.Write("  ");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("- " + description);
            Console.ResetColor();
        }
    }
}