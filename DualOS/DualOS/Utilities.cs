using System;

namespace DualOS
{
    public static class Utilities
    {
        public static void PrintDualOSLogo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("              *****            ");
            Console.WriteLine("         *************         ");
            Console.WriteLine("      *******************      ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("    *********     *********    ");
            Console.WriteLine("   *******           *******   ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  ******      ***      ******  ");
            Console.WriteLine(" ******     *******     ****** ");
            Console.WriteLine(" ******    *********    ****** ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" ******     *******     ****** ");
            Console.WriteLine("  ******      ***      ******  ");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("   *******           *******   ");
            Console.WriteLine("    *********     *********    ");
            Console.WriteLine("      *******************      ");
            Console.WriteLine("         *************         ");
            Console.WriteLine("              *****            ");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("   Dual");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("OS");

            Console.ResetColor();
        }
    }
}