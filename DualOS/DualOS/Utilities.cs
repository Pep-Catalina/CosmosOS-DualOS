using System;
using Sys = Cosmos.System;

public static void PrintDualOSLogo(){
    Console.Clear();
    // Parte superior (azul)
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("              *****            ");
    Console.WriteLine("         *************         ");
    Console.WriteLine("      *******************      ");
    // Transicion azul -> cyan
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("    *********     *********    ");
    Console.WriteLine("   *******           *******   ");
    // Centro (simula brillo)
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("  ******      ***      ******  ");
    Console.WriteLine(" ******     *******     ****** ");
    Console.WriteLine(" ******    *********    ****** ");
    // Transicion hacia verde
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(" ******     *******     ****** ");
    Console.WriteLine("  ******      ***      ******  ");
    // Parte inferior (verde oscuro)
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine("   *******           *******   ");
    Console.WriteLine("    *********     *********    ");
    Console.WriteLine("      *******************      ");
    Console.WriteLine("         *************         ");
    Console.WriteLine("              *****            ");
    Console.WriteLine();
    // Texto DualOS con mezcla de colores
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("   Dual");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("OS");
    // Reset color
    Console.ResetColor();
}