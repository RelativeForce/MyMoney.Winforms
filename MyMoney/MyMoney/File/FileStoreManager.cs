using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoney.File
{

    public class FileStoreManager
    {

        public static string STORAGE_FILE = AppDomain.CurrentDomain.BaseDirectory + "\\" + "dataStore.txt";

        public static string DB_FILE_PATH = "undefined";

        private const string DEFAULT_FILE_NAME = "Finance.sqlite";

        private static string CURRENT_DIRECTORY = AppDomain.CurrentDomain.BaseDirectory;

        public bool Read()
        {
            // If there is a current database file open in program don't.
            if (!DB_FILE_PATH.Equals("undefined")) return false;

            // If the storage file does not exist in the local storage.
            if (!System.IO.File.Exists(STORAGE_FILE))
            {
                // Feedback and create the file.
                Console.Out.WriteLine("Storage Created");
                System.IO.File.Create(STORAGE_FILE);

                return false;
            }

            // Feedback and read the contents of the storage file.
            Console.Out.WriteLine("Storage Located");
            string storageFileText = System.IO.File.ReadAllText(STORAGE_FILE);

            // Check that the file specified in the storage exists.
            if (System.IO.File.Exists(storageFileText))
            {
                // Feedback and set the specified file as the file to be loaded.
                Console.Out.WriteLine("Stored File Loaded");
                DB_FILE_PATH = storageFileText;

                return true;
            }

            ClearFileStore();

            return false;



        }

        public void ClearFileStore() {

            // The file is wiped because the contents are invalid.
            Console.Out.WriteLine("Stored File Invalid, Wiping Storage File.");
            System.IO.File.WriteAllText(STORAGE_FILE, "");

        }

    }
}
