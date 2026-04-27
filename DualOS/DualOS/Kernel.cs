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

            history.Add(input);
            ExecuteCommand(input);
        }

        private void ExecuteCommand(string input)
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0].ToLower();

            switch (command)
            {
                case "guide":
                    Consola.ShowHelp();
                    break;

                case "clear":
                    Console.Clear();
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
                    fileSystem.CreateDirectory(parts[1]);
                    break;

                case "wipe":
                    fileSystem.DeleteDirectory(parts[1]);
                    break;

                case "write":
                    string file = parts[1];
                    string content = input.Substring(input.IndexOf(file) + file.Length + 1);
                    fileSystem.WriteFile(file, content);
                    break;

                case "read":
                    fileSystem.ReadFile(parts[1]);
                    break;

                case "jump":
                    fileSystem.ChangeDirectory(parts[1]);
                    break;

                case "history":
                    history.Show();
                    break;

                default:
                    Console.WriteLine("Unknown command. Type 'guide' for help.");
                    break;
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