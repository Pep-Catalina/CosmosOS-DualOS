using System;
using System.IO;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Sys = Cosmos.System;

namespace DualOS{
    public class Kernel : Sys.Kernel{
        private CosmosVFS fs;
        private string currentPath = @"0:\";

        private string NormalizePath(string path){
            if (string.IsNullOrEmpty(path)) return @"0:\";
            path = path.Replace('/', '\\');
            if (!path.EndsWith(@"\")) path += @"\";
            return path;
        }

        protected override void BeforeRun(){
            fs = new CosmosVFS();
            VFSManager.RegisterVFS(fs);

            Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.ESStandardLayout());
            Utilities.PrintDualOSLogo();

            StartupSound();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("DualOS started successfully!");
            Console.ResetColor();
        }

        protected override void Run(){
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(currentPath + "> ");
            Console.ResetColor();

            string input = Console.ReadLine();

            if (input == null || input.Trim() == ""){
                return;
            }

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0].ToLower();

            switch (command){
                case "guide":
                    Consola.ShowHelp();
                    SuccessSound();
                    break;

                case "clearvoid":
                    Console.Clear();
                    SuccessSound();
                    break;

                case "origin":
                    Console.WriteLine("DualOS v1.0");
                    SuccessSound();
                    break;

                case "shutdown":
                    HandleShutdown(parts);
                    break;

                case "calc":
                    Calculadora.Execute(parts);
                    SuccessSound();
                    break;

                case "disks":
                    ShowDisks();
                    SuccessSound();
                    break;

                case "peek":
                    PeekCurrentDirectory();
                    SuccessSound();
                    break;

                case "forge":
                    CreateDirectory(parts);
                    break;

                case "wipe":
                    DeleteDirectory(parts);
                    break;

                case "write":
                    WriteFile(parts, input);
                    break;

                case "read":
                    ReadFile(parts);
                    break;

                case "jump":
                    ChangeDirectory(parts);
                    break;

                default:
                    Console.WriteLine("Unknown command. Type 'guide' for help.");
                    ErrorSound();
                    break;
            }
        }

        private void StartupSound(){
            try{
                Console.Beep(523, 120);
                Console.Beep(659, 140);
                Console.Beep(784, 180);
                Console.Beep(1046, 260);
            }
            catch{
            }
        }

        private void SuccessSound(){
            try{
                Console.Beep(740, 80);
                Console.Beep(988, 130);
            }
            catch{
            }
        }

        private void ErrorSound(){
            try{
                Console.Beep(440, 110);
                Console.Beep(311, 180);
            }
            catch{
            }
        }

        private void HandleShutdown(string[] parts){
            if (parts.Length < 2){
                Console.WriteLine("Usage: shutdown off | shutdown reboot");
                ErrorSound();
                return;
            }

            string option = parts[1].ToLower();

            switch (option){
                case "off":
                    SuccessSound();
                    Sys.Power.Shutdown();
                    break;

                case "reboot":
                    SuccessSound();
                    Sys.Power.Reboot();
                    break;

                default:
                    Console.WriteLine("Invalid option. Use: shutdown off | shutdown reboot");
                    ErrorSound();
                    break;
            }
        }

        private void ShowDisks(){
            try{
                var disks = VFSManager.GetDisks();

                if (disks == null || disks.Count == 0){
                    Console.WriteLine("No disks found.");
                    ErrorSound();
                    return;
                }

                foreach (var disk in disks){
                    try{
                        string diskInfo = "Disk: 0:\\";
                        if (disk.Partitions != null && disk.Partitions.Count > 0){
                            foreach (var partition in disk.Partitions){
                                diskInfo += " Partition: " + partition.RootPath;
                            }
                        }
                        Console.WriteLine(diskInfo);
                    }
                    catch{
                        Console.WriteLine("Disk: Available");
                    }
                }
            }
            catch (Exception ex){
                Console.WriteLine("Error showing disks: " + ex.Message);
                ErrorSound();
            }
        }

        private void PeekCurrentDirectory(){
            try{
                string[] directories = Directory.GetDirectories(currentPath);
                string[] files = Directory.GetFiles(currentPath);

                Console.WriteLine("Current path: " + currentPath);
                Console.WriteLine("Directories:");

                if (directories.Length == 0){
                    Console.WriteLine("  (none)");
                }
                else{
                    foreach (string dir in directories){
                        Console.WriteLine("  [DIR] " + Path.GetFileName(dir));
                    }
                }

                Console.WriteLine("Files:");
                if (files.Length == 0){
                    Console.WriteLine("  (none)");
                }
                else{
                    foreach (string file in files){
                        Console.WriteLine("  " + Path.GetFileName(file));
                    }
                }
            }
            catch (Exception ex){
                Console.WriteLine("Error listing directory: " + ex.Message);
                ErrorSound();
            }
        }

        private void CreateDirectory(string[] parts){
            if (parts.Length < 2){
                Console.WriteLine("Usage: forge <directory_name>");
                ErrorSound();
                return;
            }
            try{
                string dirName = parts[1];
                string path = currentPath + dirName;
                path = NormalizePath(path);

                if (Directory.Exists(path)){
                    Console.WriteLine("Directory already exists.");
                    ErrorSound();
                    return;
                }

                Directory.CreateDirectory(path);
                Console.WriteLine("Directory created: " + path);
                SuccessSound();
            }
            catch (Exception ex){
                Console.WriteLine("Error creating directory: " + ex.Message);
                ErrorSound();
            }
        }

        private void DeleteDirectory(string[] parts){
            if (parts.Length < 2){
                Console.WriteLine("Usage: wipe <directory_name>");
                ErrorSound();
                return;
            }
            try{
                string dirName = parts[1];
                string path = currentPath + dirName;
                path = NormalizePath(path);

                if (!Directory.Exists(path)){
                    Console.WriteLine("Directory does not exist.");
                    ErrorSound();
                    return;
                }

                Directory.Delete(path, true);
                Console.WriteLine("Directory deleted: " + path);
                SuccessSound();
            }
            catch (Exception ex){
                Console.WriteLine("Error deleting directory: " + ex.Message);
                ErrorSound();
            }
        }

        private void WriteFile(string[] parts, string fullInput){
            if (parts.Length < 3){
                Console.WriteLine("Usage: write <file_name> <text>");
                ErrorSound();
                return;
            }

            try{
                string fileName = parts[1];
                string path = currentPath + fileName;
                path = NormalizePath(path).TrimEnd('\\');

                int writeIndex = fullInput.IndexOf("write");
                int firstSpaceAfterWrite = fullInput.IndexOf(' ', writeIndex);
                int fileNameStart = fullInput.IndexOf(fileName, firstSpaceAfterWrite);
                int contentStart = fileNameStart + fileName.Length;

                if (contentStart < fullInput.Length && fullInput[contentStart] == ' '){
                    content
