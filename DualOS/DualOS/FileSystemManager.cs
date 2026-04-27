using System;
using System.IO;
using System.Text;
using Cosmos.System.FileSystem.VFS;

namespace DualOS
{
    public class FileSystemManager
    {
        public string CurrentPath = @"0:\";

        public string ShowDisks()
        {
            StringBuilder sb = new StringBuilder();

            var disks = VFSManager.GetDisks();

            if (disks == null || disks.Count == 0)
            {
                return "No disks found.";
            }

            foreach (var d in disks)
            {
                sb.AppendLine("Disk: " + d.ToString());
            }

            return sb.ToString();
        }

        public string Peek()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Current path: " + CurrentPath);

            string[] directories = Directory.GetDirectories(CurrentPath);
            string[] files = Directory.GetFiles(CurrentPath);

            sb.AppendLine("Directories:");
            if (directories.Length == 0)
            {
                sb.AppendLine("  (none)");
            }
            else
            {
                foreach (var dir in directories)
                {
                    sb.AppendLine("  [DIR] " + Path.GetFileName(dir));
                }
            }

            sb.AppendLine("Files:");
            if (files.Length == 0)
            {
                sb.AppendLine("  (none)");
            }
            else
            {
                foreach (var file in files)
                {
                    sb.AppendLine("  " + Path.GetFileName(file));
                }
            }

            return sb.ToString();
        }

        public string CreateDirectory(string name)
        {
            string path = Path.Combine(CurrentPath, name);
            Directory.CreateDirectory(path);
            return "Directory created: " + path;
        }

        public string DeleteDirectory(string name)
        {
            string path = Path.Combine(CurrentPath, name);

            if (!Directory.Exists(path))
            {
                return "Directory does not exist: " + path;
            }

            Directory.Delete(path);
            return "Directory deleted: " + path;
        }

        public string WriteFile(string fileName, string content)
        {
            string path = Path.Combine(CurrentPath, fileName);
            File.WriteAllText(path, content);
            return "File written: " + path;
        }

        public string ReadFile(string fileName)
        {
            string path = Path.Combine(CurrentPath, fileName);

            if (!File.Exists(path))
            {
                return "File does not exist: " + path;
            }

            return File.ReadAllText(path);
        }

        public string ChangeDirectory(string target)
        {
            string newPath;

            if (target.Contains(@":\"))
            {
                newPath = target;
            }
            else
            {
                newPath = Path.Combine(CurrentPath, target);
            }

            newPath = NormalizePath(newPath);

            if (!Directory.Exists(newPath))
            {
                return "Directory does not exist: " + newPath;
            }

            CurrentPath = newPath;
            return "Current directory: " + CurrentPath;
        }

        private string NormalizePath(string path)
        {
            path = path.Replace('/', '\\');

            string[] rawParts = path.Split('\\', StringSplitOptions.RemoveEmptyEntries);
            System.Collections.Generic.List<string> cleanParts = new System.Collections.Generic.List<string>();

            foreach (string part in rawParts)
            {
                if (part == ".")
                {
                    continue;
                }

                if (part == "..")
                {
                    if (cleanParts.Count > 1)
                    {
                        cleanParts.RemoveAt(cleanParts.Count - 1);
                    }

                    continue;
                }

                cleanParts.Add(part);
            }

            if (cleanParts.Count == 0)
            {
                return @"0:\";
            }

            string result = cleanParts[0] + @"\";

            for (int i = 1; i < cleanParts.Count; i++)
            {
                result += cleanParts[i] + @"\";
            }

            return result;
        }
    }
}
