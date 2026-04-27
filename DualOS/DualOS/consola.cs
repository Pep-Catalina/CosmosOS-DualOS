using System;
using System.Text;

namespace DualOS
{
    public static class Consola
    {
        public static string GetHelpText()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("DualOS Help");
            sb.AppendLine("==============================================");

            sb.AppendLine();
            sb.AppendLine("File System Commands");
            sb.AppendLine("  disks                    - Show available disks");
            sb.AppendLine("  peek                     - List directories and files");
            sb.AppendLine("  jump <path>              - Change directory");
            sb.AppendLine("  forge <name>             - Create directory");
            sb.AppendLine("  wipe <name>              - Delete directory");
            sb.AppendLine("  write <file> <text>      - Write file");
            sb.AppendLine("  read <file>              - Read file");

            sb.AppendLine();
            sb.AppendLine("System Commands");
            sb.AppendLine("  guide                    - Show help");
            sb.AppendLine("  origin                   - Show version");
            sb.AppendLine("  clear / clearvoid        - Clear graphical shell");
            sb.AppendLine("  shutdown off             - Shutdown system");
            sb.AppendLine("  shutdown reboot          - Restart system");

            sb.AppendLine();
            sb.AppendLine("Calculator");
            sb.AppendLine("  calc add a b             - Add numbers");
            sb.AppendLine("  calc sub a b             - Subtract");
            sb.AppendLine("  calc mul a b             - Multiply");
            sb.AppendLine("  calc div a b             - Divide");
            sb.AppendLine("  calc mod a b             - Modulo");
            sb.AppendLine("  calc sqrt a              - Square root");

            sb.AppendLine();
            sb.AppendLine("History");
            sb.AppendLine("  history                  - Show last 5 commands");
            sb.AppendLine("  !n                       - Execute command from history");

            sb.AppendLine();
            sb.AppendLine("Examples");
            sb.AppendLine("  peek");
            sb.AppendLine("  forge docs");
            sb.AppendLine("  jump docs");
            sb.AppendLine("  write note.txt Hello DualOS");
            sb.AppendLine("  read note.txt");
            sb.AppendLine("  history");
            sb.AppendLine("  !0");

            return sb.ToString();
        }

        public static void ShowHelp()
        {
            Console.WriteLine(GetHelpText());
        }
    }
}
