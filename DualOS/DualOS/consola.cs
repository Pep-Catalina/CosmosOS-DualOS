using System;
using System.Text;

namespace DualOS
{
    public static class consola
    {
        // Espera a que l'usuari premi ENTER
        private static void PressEnterToContinue()
        {
            Console.WriteLine();
            Console.Write("Press ENTER to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        // Mostra l'ajuda
        public static void ShowHelp()
        {
            Console.WriteLine("DualOS Help");
            Console.WriteLine("==============================================");

            Console.WriteLine();
            Console.WriteLine("File System Commands");
            Console.WriteLine("  disks                    - Show available disks");
            Console.WriteLine("  peek                     - List directories and files");
            Console.WriteLine("  jump <path>              - Change directory");
            Console.WriteLine("  forge <name>             - Create directory");
            Console.WriteLine("  wipe <name>              - Delete directory");
            Console.WriteLine("  write <file> <text>      - Write file");
            Console.WriteLine("  read <file>              - Read file");

            PressEnterToContinue();

            Console.WriteLine("Network Commands");
            Console.WriteLine("  netconfig <ip> <mask> <gateway> - Configure static IP");
            Console.WriteLine("  ip                       - Show current IP address");

            Console.WriteLine();
            Console.WriteLine("FTP Commands");
            Console.WriteLine("  ftpstart                 - Start FTP server (use FileZilla)");
            Console.WriteLine("  ftpstop                  - Stop FTP server");
            Console.WriteLine("  ftpstatus                - Show FTP server status");

            PressEnterToContinue();

            Console.WriteLine("System Commands");
            Console.WriteLine("  guide                    - Show help");
            Console.WriteLine("  origin                   - Show version");
            Console.WriteLine("  clear / clearvoid        - Clear graphical shell");
            Console.WriteLine("  shutdown off             - Shutdown system");
            Console.WriteLine("  shutdown reboot          - Restart system");

            Console.WriteLine();
            Console.WriteLine("Calculator");
            Console.WriteLine("  calc add a b             - Add numbers");
            Console.WriteLine("  calc sub a b             - Subtract");
            Console.WriteLine("  calc mul a b             - Multiply");
            Console.WriteLine("  calc div a b             - Divide");
            Console.WriteLine("  calc mod a b             - Modulo");
            Console.WriteLine("  calc sqrt a              - Square root");

            PressEnterToContinue();

            Console.WriteLine("History");
            Console.WriteLine("  history                  - Show last 5 commands");
            Console.WriteLine("  !n                       - Execute command from history");

            Console.WriteLine();
            Console.WriteLine("Examples");
            Console.WriteLine("  peek");
            Console.WriteLine("  forge docs");
            Console.WriteLine("  jump docs");
            Console.WriteLine("  write note.txt Hello DualOS");
            Console.WriteLine("  read note.txt");
            Console.WriteLine("  netconfig 192.168.1.100 255.255.255.0 192.168.1.1");
            Console.WriteLine("  ip");
            Console.WriteLine("  ftpstart");
            Console.WriteLine("  history");
            Console.WriteLine("  !0");
        }
    }
}