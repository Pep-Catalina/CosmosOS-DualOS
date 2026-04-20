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
                    break;

                case "clearvoid":
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
                    ShowDisks();
                    break;

                case "peek":
                    PeekCurrentDirectory();
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
                    break;
            }
        }

        private void HandleShutdown(string[] parts){
            if (parts.Length < 2){
                Console.WriteLine("Usage: shutdown off | shutdown reboot");
                return;
            }

            string option = parts[1].ToLower();

            switch (option){
                case "off":
                    Sys.Power.Shutdown();
                    break;

                case "reboot":
                    Sys.Power.Reboot();
                    break;

                default:
                    Console.WriteLine("Invalid option. Use: shutdown off | shutdown reboot");
                    break;
            }
        }

        private void ShowDisks(){
            try{
                var disks = VFSManager.GetDisks();

                if (disks == null || disks.Count == 0){
                    Console.WriteLine("No disks found.");
                    return;
                }

                foreach (var disk in disks){
                    try
                    {
                        string diskInfo = "Disk: 0:\\";
                        if (disk.Partitions != null && disk.Partitions.Count > 0){
                            foreach (var partition in disk.Partitions)
                            {
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
                    foreach (string dir in directories)
                    {
                        Console.WriteLine("  [DIR] " + Path.GetFileName(dir));
                    }
                }

                Console.WriteLine("Files:");
                if (files.Length == 0){
                    Console.WriteLine("  (none)");
                }
                else{
                    foreach (string file in files)
                    {
                        Console.WriteLine("  " + Path.GetFileName(file));
                    }
                }
            }
            catch (Exception ex){
                Console.WriteLine("Error listing directory: " + ex.Message);
            }
        }

        private void CreateDirectory(string[] parts){
            if (parts.Length < 2){
                Console.WriteLine("Usage: forge <directory_name>");
                return;
            }
            try{
                string dirName = parts[1];
                string path = currentPath + dirName;
                path = NormalizePath(path);

                if (Directory.Exists(path)){
                    Console.WriteLine("Directory already exists.");
                    return;
                }

                Directory.CreateDirectory(path);
                Console.WriteLine("Directory created: " + path);
            }
            catch (Exception ex){
                Console.WriteLine("Error creating directory: " + ex.Message);
            }
        }

        private void DeleteDirectory(string[] parts){
            if (parts.Length < 2){
                Console.WriteLine("Usage: wipe <directory_name>");
                return;
            }
            try{
                string dirName = parts[1];
                string path = currentPath + dirName;
                path = NormalizePath(path);

                if (!Directory.Exists(path)){
                    Console.WriteLine("Directory does not exist.");
                    return;
                }

                Directory.Delete(path, true);
                Console.WriteLine("Directory deleted: " + path);
            }
            catch (Exception ex){
                Console.WriteLine("Error deleting directory: " + ex.Message);
            }
        }

        private void WriteFile(string[] parts, string fullInput){
            if (parts.Length < 3){
                Console.WriteLine("Usage: write <file_name> <text>");
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
                    contentStart++;
                }

                string content = contentStart < fullInput.Length ? fullInput.Substring(contentStart) : "";

                File.WriteAllText(path, content);
                Console.WriteLine("File written: " + path);
            }
            catch (Exception ex){
                Console.WriteLine("Error writing file: " + ex.Message);
            }
        }

        private void ReadFile(string[] parts){
            if (parts.Length < 2){
                Console.WriteLine("Usage: read <file_name>");
                return;
            }
            try{
                string fileName = parts[1];
                string path = currentPath + fileName;
                path = NormalizePath(path).TrimEnd('\\');

                if (!File.Exists(path)){
                    Console.WriteLine("File does not exist.");
                    return;
                }

                string content = File.ReadAllText(path);
                Console.WriteLine("Content of " + path + ":");
                Console.WriteLine(content);
            }
            catch (Exception ex){
                Console.WriteLine("Error reading file: " + ex.Message);
            }
        }

        private void ChangeDirectory(string[] parts){
            if (parts.Length < 2){
                Console.WriteLine("Usage: jump <path>");
                return;
            }
            try{
                string targetPath = parts[1];
                string newPath;

                if (targetPath.Contains(@":")){
                    newPath = targetPath;
                }
                else{
                    newPath = currentPath + targetPath;
                }

                newPath = newPath.Replace('/', '\\');

                string[] rawParts = newPath.Split('\\', StringSplitOptions.RemoveEmptyEntries);
                System.Collections.Generic.List<string> cleanParts = new System.Collections.Generic.List<string>();

                foreach (string part in rawParts){
                    if (part == "."){
                        continue;
                    }
                    else if (part == "..")
                    {
                        if (cleanParts.Count > 1)
                        {
                            cleanParts.RemoveAt(cleanParts.Count - 1);
                        }
                    }
                    else
                    {
                        cleanParts.Add(part);
                    }
                }

                if (cleanParts.Count == 0){
                    newPath = @"0:\";
                }
                else{
                    newPath = cleanParts[0] + @"\";
                    for (int i = 1; i < cleanParts.Count; i++)
                    {
                        newPath += cleanParts[i] + @"\";
                    }
                }

                newPath = NormalizePath(newPath);

                if (!Directory.Exists(newPath)){
                    Console.WriteLine("Directory does not exist: " + newPath);
                    return;
                }

                currentPath = newPath;
                Console.WriteLine("Current directory: " + currentPath);
            }
            catch (Exception ex){
                Console.WriteLine("Error changing directory: " + ex.Message);
            }
        }
    }
}