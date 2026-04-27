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

        protected override void BeforeRun()
        {
            fs = new CosmosVFS();
            VFSManager.RegisterVFS(fs);

            Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.ESStandardLayout());
            Utilities.PrintDualOSLogo();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("DualOS started successfully!");
            Console.ResetColor();
        }

        protected override void Run()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(fileSystem.CurrentPath + "> ");
            Console.ResetColor();

            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return;

            // Ejecutar historial
            if (input.StartsWith("!"))
            {
                string cmd = history.GetCommand(input);
                if (cmd != null)
                {
                    Console.WriteLine("Executing: " + cmd);
                    ExecuteCommand(cmd);
                    history.Add(cmd);
                }
                return;
            }

            ExecuteCommand(input);
            history.Add(input);
        }

        private void ExecuteCommand(string input)
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
                return;

            string command = parts[0].ToLower();

            try
            {
                switch (command)
                {
                    case "guide":
                        Consola.ShowHelp();
                        break;

                    case "clear":
                        Console.Clear();
                        break;

                    case "history":
                        history.Show();
                        break;

                    case "origin":
                        Console.WriteLine("DualOS v1.0");
                        break;

                    case "shutdown":
                        HandleShutdown(parts);
                        break;

                    case "calc":
                        Calculadora.Execute(parts);
                        break;

                    case "disks":
                        fileSystem.ShowDisks();
                        break;

                    case "peek":
                        fileSystem.Peek();
                        break;

                    case "forge":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Usage: forge <directory_name>");
                        }
                        else
                        {
                            fileSystem.CreateDirectory(parts[1]);
                        }
                        break;

                    case "wipe":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Usage: wipe <directory_name>");
                        }
                        else
                        {
                            fileSystem.DeleteDirectory(parts[1]);
                        }
                        break;

                    case "write":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Usage: write <filename> <content>");
                        }
                        else
                        {
                            string file = parts[1];
                            string content = input.Substring(input.IndexOf(file) + file.Length + 1);
                            fileSystem.WriteFile(file, content);
                        }
                        break;

                    case "read":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Usage: read <filename>");
                        }
                        else
                        {
                            fileSystem.ReadFile(parts[1]);
                        }
                        break;

                    case "jump":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Usage: jump <directory_path>");
                        }
                        else
                        {
                            fileSystem.ChangeDirectory(parts[1]);
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command. Type 'guide' for help.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing command: {ex.Message}");
            }
        }

        private void HandleShutdown(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Usage: shutdown off | shutdown reboot");
                return;
            }

            switch (parts[1].ToLower())
            {
                case "off":
                    Sys.Power.Shutdown();
                    break;

                case "reboot":
                    Sys.Power.Reboot();
                    break;
            }
        }
    }
}