using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileSystem.Lesson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetDirInfo();

            #region Tabular

            List<Customer> users = new List<Customer>();
            users.Add(new Customer { Name = "John", Age = 30 });
            users.Add(new Customer { Name = "Alice", Age = 25 });

            List<Account> accounts = new List<Account>();
            accounts.Add(new Account { AccountId = 1, Saldo = 1000.50m });
            accounts.Add(new Account { AccountId = 2, Saldo = 500.75m });

            // WriteAsTabular(@"D:\logs\", "TabularFile", users);

            #endregion
            string path =Directory.GetCurrentDirectory();
            string ClientsFilename = "ClientieAccounts.csv";
            

            WriteAsTabular(path,ClientsFilename,users,accounts);
        }
        static void SpecialPath(string RootPath, String MyDir) // Path  = percorso LOCAL  -> REMOTE path -> SERVER  
        {
            string myPath = $"{RootPath}{Path.DirectorySeparatorChar}{MyDir}";

            Console.WriteLine(myPath);
            // C:\users\bruno\ -> Windows 
            // Home/Bruno/miofile/ -> UNIX + MacOS
        }
        static void SpecialDirectory()
        {
            string SpecialDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Console.WriteLine(SpecialDir);
        }
        static void SplitPath()
        {
            string myDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine(myDirectory);
            string[] splitedPath = myDirectory.Split(Path.DirectorySeparatorChar);

            foreach (var item in splitedPath)
            {
                // int counter = 1;
                Console.Write("\\");
                Console.WriteLine(item);

            }
            JoinPath(splitedPath);
        }
        static void JoinPath(string[] _path)
        {

            var path = Path.Combine(_path);
            Console.Write("JOINED STRINGS: ");
            Console.WriteLine(path);
        }
        static void GetFileExtention()
        {
            var fExt = Path.GetExtension("vendita.json");
            Console.WriteLine(fExt);
        }
        static void GetDirInfo()
        {
            string path = Directory.GetCurrentDirectory(); // -> trova il Path 
            DirectoryInfo dInfos = new DirectoryInfo(path);


            foreach (var item in dInfos.GetFiles())
            {
                Console.WriteLine($"Name -  {item.Name}");
                Console.WriteLine($"Parent Directory -  {item.Directory.Parent}");
                Console.WriteLine($" Directory FullName -  {item.Directory.FullName}");
                Console.WriteLine($" Directory CreationTime -  {item.Directory.CreationTime}");
                Console.WriteLine($" Directory LastAccessTime -  {item.Directory.LastAccessTime}");
                Console.WriteLine($" Directory Root -  {item.Directory.Root}");


            }
        }
        static void SearchInDirectory()
        {
            var files = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.dll", SearchOption.AllDirectories);


            foreach (var file in files)
                Console.WriteLine(file);
        }
        static void FindOrCreate(String path)
        {
            DirectoryInfo info = new DirectoryInfo(path);

            if (info.Exists)
            {
                Console.WriteLine(info.FullName);
                Console.WriteLine(info.Name);
                Console.WriteLine(info.Parent);
            }
            else
            {
                info.Create();
                Console.WriteLine(info.FullName);
                Console.WriteLine(info.Name);
                Console.WriteLine(info.Parent);
            }
        }
        static void CreateFile(string FileName)
        {
            File.Create(FileName);
        }
        static void WriteOnFile(string path, string FileName)
        {
            List<string> mytext = new List<string>()
            {
                "Heello by Bruno",
                "Heello by marco",
                "Heello by Maria"
            };

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.WriteAllLines(Path.Combine(path, FileName), mytext); // Overwrite all content 
            File.AppendAllLines(Path.Combine(path, FileName), mytext); // Append content text 


        }
        static void ReadOnFile(string path, string FileName)
        {
            var texd = File.ReadAllText(Path.Combine(path, FileName));
            Console.WriteLine(texd);
        }
        static void SimpleFileMove(string SrcPath, string destPath, string Filename)
        {
            string Src = Path.Combine(SrcPath, Filename);
            string dest = Path.Combine(destPath, Filename);
            File.Move(Src, dest);
        }
        static void SimpleFileCopy(string SrcPath, string destPath, string Filename)
        {
            string Src = Path.Combine(SrcPath, Filename);
            string dest = Path.Combine(destPath, Filename);
            File.Copy(Src, dest, true);
        }
        static void SimpleFileDelete(string SrcPath, string Filename)
        {
            File.Delete(Path.Combine(SrcPath, Filename));
        }
        static void WriteAsTabular(string path, string Filename, List<Customer> data, List<Account> accounts)
        {
            StringBuilder sb = new StringBuilder();

            string FilePath = Path.Combine(path, Filename);

            if (!File.Exists(FilePath))
            {
                string header = string.Format("Name,Age,AccountId,Saldo");
                sb.AppendLine(header);
            }

            for (int i = 0; i < data.Count; i++)
            {
                var usr = data[i];
                var acc = (i < accounts.Count) ? accounts[i] : null;

                // Aggiungi informazioni sull'account solo se è presente
                if (usr != null)
                {
                    sb.AppendLine(string.Format($"{usr.Name},{usr.Age},{(acc != null ? acc.AccountId.ToString() : "")},{(acc != null ? acc.Saldo.ToString() : "")}"));
                }
                else
                {
                    // Nel caso in cui il cliente non sia presente, inserisci valori di default o vuoti
                    sb.AppendLine(string.Format(",,,"));
                }
            }

            File.AppendAllText(FilePath, sb.ToString());
        }
    }

    public class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }

    }
    public class Account
    {
        public int AccountId { get; set; }
        public decimal Saldo { get; set; }

    }
}