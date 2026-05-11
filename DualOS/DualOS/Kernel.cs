using System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Sys = Cosmos.System;

namespace DualOS
{
    public class Kernel : Sys.Kernel
    {
        private CosmosVFS fs;
        private FileSystemManager fileSystem = new FileSystemManager();
        private CommandHistory history = new CommandHistory();
        private GraphicsManager graphics = new GraphicsManager();
        private NetworkManager network = new NetworkManager();
        private FtpManager ftp = new FtpManager();

        private string inputBuffer = "";

        protected override void BeforeRun()
        {
            fs = new CosmosVFS();
            VFSManager.RegisterVFS(fs);

            Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.ESStandardLayout());

            graphics.Initialize();
            graphics.DrawWelcomeScreen();

            Console.ReadKey(true);

            AddSafeOutput("DualOS started successfully.");
            AddSafeOutput("Type 'guide' to show available commands.");

            graphics.DrawShell(fileSystem.CurrentPath, inputBuffer);
        }

        protected override void Run()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                string commandInput = inputBuffer;
                inputBuffer = "";

                if (commandInput == null || commandInput.Trim() == "")
                {
                    graphics.DrawShell(fileSystem.CurrentPath, inputBuffer);
                    return;
                }

                AddSafeOutput(fileSystem.CurrentPath + "> " + commandInput);

                if (commandInput.StartsWith("!"))
                {
                    string cmd = history.GetCommand(commandInput);

                    if (cmd != null)
                    {
                        AddSafeOutput("Executing: " + cmd);

                        string result = ExecuteCommand(cmd);
                        AddSafeOutput(result);

                        history.Add(cmd);
                    }
                    else
                    {
                        AddSafeOutput("Invalid history command.");
                    }
                }
                else
                {
                    string result = ExecuteCommand(commandInput);
                    AddSafeOutput(result);

                    history.Add(commandInput);
                }

                graphics.DrawShell(fileSystem.CurrentPath, inputBuffer);
                return;
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                if (inputBuffer.Length > 0)
                {
                    inputBuffer = inputBuffer.Substring(0, inputBuffer.Length - 1);
                }

                graphics.DrawShell(fileSystem.CurrentPath, inputBuffer);
                return;
            }

            char c = key.KeyChar;

            if (c != '\0')
            {
                inputBuffer += c;
                graphics.DrawShell(fileSystem.CurrentPath, inputBuffer);
            }
        }

        private string ExecuteCommand(string input)
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                return "";
            }

            string command = parts[0].ToLower();

            try
      {
                switch (command)
                {
                    case "guide":
                        return Consola.GetHelpBlock1();

                    case "guide2":
                        return Consola.GetHelpBlock2();

                    case "guide3":
                        return Consola.GetHelpBlock3();
                    case "clear":
                    case "clearvoid":
                        graphics.ClearOutput();
                        return "Screen cleared.";

                    case "history":
                        return history.GetHistoryText();

                    case "origin":
                        return "DualOS v1.0 - Cosmos Graphic Subsystem";

                    case "shutdown":
                        return HandleShutdown(parts);

                    case "calc":
                        return Calculadora.ExecuteToString(parts);

                    case "disks":
                        return fileSystem.ShowDisks();

                    case "peek":
                        return fileSystem.Peek();

                    case "forge":
                        if (parts.Length < 2)
                        {
                            return "Usage: forge <name>";
                        }

                        return fileSystem.CreateDirectory(parts[1]);

                    case "wipe":
                        if (parts.Length < 2)
                        {
                            return "Usage: wipe <name>";
                        }

                        return fileSystem.DeleteDirectory(parts[1]);

                    case "write":
                        if (parts.Length < 3)
                        {
                            return "Usage: write <file> <text>";
                        }

                        string file = parts[1];
                        string content = input.Substring(input.IndexOf(file) + file.Length + 1);

                        return fileSystem.WriteFile(file, content);

                    case "read":
                        if (parts.Length < 2)
                        {
                            return "Usage: read <file>";
                        }

                        return fileSystem.ReadFile(parts[1]);

                    case "jump":
                        if (parts.Length < 2)
                        {
                            return "Usage: jump <path>";
                        }

                        return fileSystem.ChangeDirectory(parts[1]);

                    case "netconfig":
                        if (parts.Length < 4)
                        {
                            return "Usage: netconfig <ip> <mask> <gateway>";
                        }

                        return network.ConfigureStaticIp(parts[1], parts[2], parts[3]);

                    case "ip":
                        return network.GetCurrentIp();

                    case "ftpstart":
                        return ftp.StartFtp(fs);

                    case "ftpstop":
                        return ftp.StopFtp();

                    case "ftpstatus":
                        return ftp.GetFtpStatus();

                    default:
                        return "Unknown command." + Environment.NewLine +
                               "Type 'guide' for help.";
                }
            }
            catch (Exception ex)
            {
                return "Error executing command: " + ex.Message;
            }
        }

        private void AddSafeOutput(string text)
        {
            if (text == null || text.Trim() == "")
            {
                return;
            }

            graphics.AddOutput(SanitizeText(text));
        }

        private string SanitizeText(string text)
        {
            string clean = "";

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if ((c >= 32 && c <= 126) || c == '\n' || c == '\r')
                {
                    clean += c;
                }
                else
                {
                    clean += "?";
                }
            }

            return clean;
        }

        private string HandleShutdown(string[] parts)
        {
            if (parts.Length < 2)
            {
                return "Usage: shutdown off | shutdown reboot";
            }

            switch (parts[1].ToLower())
            {
                case "off":
                    Sys.Power.Shutdown();
                    return "Shutting down...";

                case "reboot":
                    Sys.Power.Reboot();
                    return "Rebooting...";

                default:
                    return "Invalid option. Use: shutdown off | shutdown reboot";
            }
        }
    }
}