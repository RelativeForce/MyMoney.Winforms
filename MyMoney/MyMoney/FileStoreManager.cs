using System;

namespace MyMoney
{

    public class FileStoreManager
    {
        public static string STORAGE_FILE = CURRENT_DIRECTORY + "\\" + "dataStore.txt";

        private static string CURRENT_DIRECTORY = AppDomain.CurrentDomain.BaseDirectory;

        private const string DEFAULT_FILE_NAME = "Finance.mdf";

        public string Read()
        {
            // If the storage file does not exist in the local storage.
            if (!System.IO.File.Exists(STORAGE_FILE))
            {
                // Feedback and create the file.
                Console.Out.WriteLine("Storage Created");
                System.IO.File.Create(STORAGE_FILE);

                return "";
            }

            // Feedback and read the contents of the storage file.
            Console.WriteLine("Storage Located");
            return System.IO.File.ReadAllText(STORAGE_FILE);
        }

        public void Clear()
        {
            // The file is wiped because the contents are invalid.
            Console.Out.WriteLine("Clearing Storage File.");
            System.IO.File.WriteAllText(STORAGE_FILE, "");
        }

        public void Save(string path) {

            Console.Out.WriteLine("Saving " + path);
            System.IO.File.WriteAllText(STORAGE_FILE, path);
        }
    }
}
