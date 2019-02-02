using System;
using System.IO;

namespace MyMoney
{

    public class FileStoreManager
    {
        public static string STORAGE_FILE = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\" + "MyMoneyDataStore.txt");

        private const string DEFAULT_FILE_NAME = "Finance.mdf";

        public string Read()
        {
            // If the storage file does not exist in the local storage.
            if (!File.Exists(STORAGE_FILE))
            {
                // Feedback and create the file.
                Console.Out.WriteLine("Storage Created");
                File.Create(STORAGE_FILE);

                return string.Empty;
            }

            // Feedback and read the contents of the storage file.
            Console.WriteLine("Storage Located");
            return File.ReadAllText(STORAGE_FILE);
        }

        public void Clear()
        {
            // The file is wiped because the contents are invalid.
            Console.Out.WriteLine("Clearing Storage File.");
            File.WriteAllText(STORAGE_FILE, "");
        }

        public void Save(string path) {

            Console.Out.WriteLine("Saving " + path);
            File.WriteAllText(STORAGE_FILE, path);
        }
    }
}
