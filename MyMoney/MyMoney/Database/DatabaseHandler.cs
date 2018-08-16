using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MyMoney.Data_Storage;

namespace MyMoney.Database
{
    /// <summary>
    /// Encapsulates the functionality of the Database. This includes creating all the tables.
    /// </summary>
    public class DatabaseHandler
    {

        private static DatabaseHandler INSTANCE = new DatabaseHandler();

        /// <summary>
        /// The file name given to the local sql database.
        /// </summary>
        private const string DEFAULT_FILE_NAME = "Finance.sqlite";

        public static string STORAGE_FILE = AppDomain.CurrentDomain.BaseDirectory + "\\" + "dataStore.txt";

        /// <summary>
        /// The file path given to the local sql database.
        /// </summary>
        public static string FILE_PATH = "undefined";

        /// <summary>
        /// The current directory that this app is located inside. The local sql 
        /// database should be in the same folder as the app.
        /// </summary>
        private static string CURRENT_DIRECTORY = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Create database file and all the tables.
        /// </summary>
        public void create()
        {
            // Create the database file.
            SQLHandler.getInstance().createDBFile(FILE_PATH);

            // Create the CashFlow Table
            CashFlow.getInstance().create();

            // Create the Budget Table
            Budget.getInstance().create();
        }

        public static DatabaseHandler getInstance() {
            return INSTANCE;
        }

        public void load(DateTime date) {

            clear();

            Console.WriteLine("Internal Storge Cleared");

            Budget.getInstance().loadFromSource(date);

            CashFlow.getInstance().loadFromSource(date);

        }

        public void clear() {

            Budget.getInstance().clear();

            CashFlow.getInstance().clear();
        }
    }

 }
