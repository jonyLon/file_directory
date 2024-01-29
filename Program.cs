using System.IO;
using System.Threading.Channels;

namespace file_directory
{

    class CmdLine
    {
        private bool flow = true;
        private Dictionary<string, string> commands = new Dictionary<string, string>()
        {
            {"help", "get commands list"},
            {"md", "create folder (provide name)"},
            {"rd", "remove folder (provide name)"},
            {"cd", "change current directory"},
            {"dir", "print content of current folder"},
            {"create", "create text file (provide name and content)"},
            {"type", "print content of file (provide name)"},
            {"copy", "copy file (provide current name and name for copy)" },
            {"del", "delete file (provide name)"},
            {"append", "add additional info to file (provide name and content)"},
            {"exit", "exit from cmd"}
        };        
        public CmdLine() {
            while (flow)
            {
                Console.WriteLine("Enter command (use 'help' to see command list):\n");
                string com = Console.ReadLine();
                string[] com_arr = com.Split(" ");
                if (!commands.ContainsKey(com_arr[0]))
                {
                    throw new ArgumentException($"'{com_arr[0]}' is not command");
                }
                switch (com_arr[0])
                {
                    case "help":
                        Console.WriteLine("List of commands:\n");
                        foreach (var item in commands)
                        {
                            Console.WriteLine($"{item.Key} - {item.Value}");
                        }
                        break;
                    case "md":
                        if (com_arr.Length == 1 || String.IsNullOrWhiteSpace(com_arr[1]))
                        {
                            throw new ArgumentException($"You should provide folder name after '{com_arr[0]}'");
                        }
                        Directory.CreateDirectory(com_arr[1]);
                        break;
                    case "rd":
                        if (com_arr.Length == 1 || String.IsNullOrWhiteSpace(com_arr[1]))
                        {
                            throw new ArgumentException($"You should provide folder name after '{com_arr[0]}'");
                        }
                        Directory.Delete(com_arr[1]);
                        break;
                    case "cd":
                        if (com_arr.Length == 1 || String.IsNullOrWhiteSpace(com_arr[1]))
                        {
                            throw new ArgumentException($"You should provide folder name after '{com_arr[0]}'");
                        }
                        Directory.SetCurrentDirectory(com_arr[1]);
                        Console.WriteLine($"Current directory is {Directory.GetCurrentDirectory()}");
                        break;

                    case "dir":
                        string[] entries = Directory.GetFileSystemEntries(Directory.GetCurrentDirectory());
                        Console.WriteLine($"Content of {Directory.GetCurrentDirectory()}:\n");
                        foreach (string entry in entries)
                        {
                            FileInfo fi = new FileInfo(entry);
                            string info = "<DIR>";
                            if (!fi.Attributes.HasFlag(FileAttributes.Directory))
                            {
                                info = fi.Length.ToString();
                            }
                            Console.WriteLine($"{fi.CreationTime,-22} {fi.Name,-50} {info,-15}");
                        }
                        break;
                    case "create":
                        if (com_arr.Length == 1 || String.IsNullOrWhiteSpace(com_arr[1]))
                        {
                            throw new ArgumentException($"You should provide file name after '{com_arr[0]}'");
                        }
                        if (com_arr.Length == 2 || String.IsNullOrWhiteSpace(com_arr[2]))
                        {
                            File.Create(com_arr[1]);
                        }
                        else
                        {
                            File.WriteAllText(com_arr[1], com_arr[2]);
                        }
                        break;
                    case "type":
                        if (com_arr.Length == 1 || String.IsNullOrWhiteSpace(com_arr[1]))
                        {
                            throw new ArgumentException($"You should provide file name after '{com_arr[0]}'");
                        }
                        Console.WriteLine(File.ReadAllText(com_arr[1]));
                        break;
                    case "copy":
                        if (com_arr.Length == 1 || String.IsNullOrWhiteSpace(com_arr[1]))
                        {
                            throw new ArgumentException($"You should provide file name after '{com_arr[0]}'");
                        }
                        if (com_arr.Length == 2 || String.IsNullOrWhiteSpace(com_arr[2]))
                        {
                            throw new ArgumentException($"You should provide file name for copy after '{com_arr[1]}'");
                        }
                        File.Copy(com_arr[1], com_arr[2]);
                        break;
                    case "del":
                        if (com_arr.Length == 1 || String.IsNullOrWhiteSpace(com_arr[1]))
                        {
                            throw new ArgumentException($"You should provide file name after '{com_arr[0]}'");
                        }
                        File.Delete(com_arr[1]);
                        break;
                    case "append":
                        if (com_arr.Length == 1 || String.IsNullOrWhiteSpace(com_arr[1]))
                        {
                            throw new ArgumentException($"You should provide file name after '{com_arr[0]}'");
                        }
                        if (com_arr.Length == 2 || String.IsNullOrWhiteSpace(com_arr[2]))
                        {
                            throw new ArgumentException($"You should provide content to append after '{com_arr[1]}'");
                        }
                        File.AppendAllText(com_arr[1], com_arr[2]);
                        break;
                    case "exit":
                        flow = false;
                        break;
                }
            }
     
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            CmdLine cmd = new CmdLine();
        }
    }
}