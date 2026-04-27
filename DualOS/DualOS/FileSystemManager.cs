using System;
using System.IO;
using Cosmos.System.FileSystem.VFS;

namespace DualOS
{
    public class FileSystemManager
    {
        public string CurrentPath = @"0:\";

        public void ShowDisks()
        {
            var disks = VFSManager.GetDisks();

            foreach (var d in disks)
            {
                Console.WriteLine("Disk: " + d.Name);
            }
        }

        public void Peek()
        {
            Console.WriteLine("Current path: " + CurrentPath);

            foreach (var dir in Directory.GetDirectories(CurrentPath))
            {
                Console.WriteLine("[DIR] " + Path.GetFileName(dir));
            }

            foreach (var file in Directory.GetFiles(CurrentPath))
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }

        public void CreateDirectory(string name)
        {
            Directory.CreateDirectory(Path.Combine(CurrentPath, name));
        }

        public void DeleteDirectory(string name)
        {
            Directory.Delete(Path.Combine(CurrentPath, name));
        }

        public void WriteFile(string fileName, string content)
        {
            File.WriteAllText(Path.Combine(CurrentPath, fileName), content);
        }

        public void ReadFile(string fileName)
        {
            Console.WriteLine(File.ReadAllText(Path.Combine(CurrentPath, fileName)));
        }

        public void ChangeDirectory(string target)
        {
            string newPath = target.Contains(@":\")
                ? target
                : Path.Combine(CurrentPath, target);

            newPath = newPath.Replace('/', '\\');

            if (!newPath.EndsWith("\\")) newPath += "\\";

            if (!Directory.Exists(newPath))
            {
                Console.WriteLine("Directory does not exist.");
                return;
            }

            CurrentPath = newPath;
        }
    }
}