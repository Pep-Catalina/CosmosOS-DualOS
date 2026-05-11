using System;

namespace DualOS
{
    public static class Utilities
    {
        public static void PrintDualOSLogo()
        {
            // Neteja la consola abans de mostrar el logo
            Console.Clear();
            
            // Canvia el color del text a blau
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

            // Escriu la paraula "Dual" en blau
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("   Dual");
            
            // Escriu la paraula "OS" en verd
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("OS");

            Console.ResetColor();
        }
    }
}
