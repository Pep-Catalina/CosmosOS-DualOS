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

        private string inputBuffer = "";

        protected override void BeforeRun()
        {
            fs = new CosmosVFS();
            VFSManager.RegisterVFS(fs);

            Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.ESStandardLayout());

            graphics.Initialize();
            graphics.DrawWelcomeScreen();

            Console.ReadKey(true);

            graphics.AddOutput("DualOS started successfully.");
            graphics.AddOutput("Type 'guide' to show available commands.");
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

                graphics.AddOutput(fileSystem.CurrentPath + "> " + commandInput);

                if (commandInput.StartsWith("!"))
                {
                    string cmd = history.GetCommand(commandInput);

                    if (cmd != null)
                    {
                        graphics.AddOutput("Executing: " + cmd);
                        string result = ExecuteCommand(cmd);
                        graphics.AddOutput(result);
                        history.Add(cmd);
                    }
                    else
                    {
                        graphics.AddOutput("Invalid history command.");
                    }
                }
                else
                {
                    string result = ExecuteCommand(commandInput);
                    graphics.AddOutput(result);
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
                        return Consola.GetHelpText();

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
                            return "Usage: forge <directory>";
                        }

                        return fileSystem.CreateDirectory(parts[1]);

                    case "wipe":
                        if (parts.Length < 2)
                        {
                            return "Usage: wipe <directory>";
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

                    default:
                        return "Unknown command. Type 'guide' for help.";
                }
            }
            catch (Exception ex)
            {
                return "Error executing command: " + ex.Message;
            }
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
