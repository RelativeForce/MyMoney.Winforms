using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SQLite;
using MyMoney.Database;

namespace MyMoney.Data_Storage
{
    /// <summary>
    /// Handles connection to the SQL database file. This class uses 
    /// the singleton pattern and the instance of this class can be accessed
    /// using <see cref="#getInstance"/>.
    /// </summary>
    public class SQLHandler
    {
        /// <summary>
        /// The singleton static instance of the <see cref="SQLHandler"/>.
        /// </summary>
        private static SQLHandler INSTANCE = new SQLHandler();

        /// <summary>
        /// The connection to the SQL database file.
        /// </summary>
        private SQLiteConnection DBConnection;

        /// <summary>
        /// Constructs the <see cref="SQLHandler"/>.
        /// </summary>
        private SQLHandler()
        {
            connect();
        }

        /// <summary>
        /// Attempt to open the connection to the database file.
        /// </summary>
        public void connect()
        {

            try
            {

                if (!DatabaseHandler.FILE_PATH.Equals("undefined"))
                {
                    System.IO.File.WriteAllText(DatabaseHandler.STORAGE_FILE, DatabaseHandler.FILE_PATH);

                    // Attempt to open the connection to the database file.
                    DBConnection = new SQLiteConnection(@"Data Source=" + DatabaseHandler.FILE_PATH + ";Version=3;");
                    DBConnection.Open();

                    // If this line is reached then the connection was successfully established.
                    Console.Out.WriteLine("Connection Establised");

                    DatabaseHandler.getInstance().load(DateTime.Today);

                }


            }
            catch (Exception ex)
            {
                // If this fails exit the program.
                Console.Out.WriteLine("Database Failure");
                Console.WriteLine(ex);
                System.Environment.Exit(1);

            }



        }

        /// <summary>
        /// Exectues a specified query that will not return a table of results.
        /// </summary>
        /// <param name="SQL">A specified query that does not return any results.</param>
        public void executeNONQuery(String SQL)
        {
            // Declare the command to be executed.
            SQLiteCommand command = null;

            try
            {
                // Process the command
                command = new SQLiteCommand(SQL, DBConnection);

                // If the commmand is successful then display the number of rows effected.
                Console.WriteLine("Rows effected: " + command.ExecuteNonQuery());
            }
            catch (Exception)
            {
                // Notify user of SQL failure.
                Console.WriteLine("SQL NON QUERY ERROR:\n" + SQL);
            }
            finally
            {
                // If the command was created then dispose of it.
                if (command != null)
                {
                    command.Dispose();
                }
            }

        }

        /// <summary>
        /// Using a specified SQL query with an agregate function retrieves an value from a specified table in the Database.
        /// </summary>
        /// <param name="sql">A query including an aggregate function.</param>
        /// <param name="variableName">
        /// The name given to the result of the operation specified in the other parameter.
        /// </param>
        /// <returns></returns>
        public int executeQuery(string sql, string variableName)
        {

            // Holds the value that will be obtained from the operation.
            int value = 0;

            // The sql comand and results reader.
            SQLiteCommand command = null;
            SQLiteDataReader reader = null;

            try
            {

                command = new SQLiteCommand(sql, DBConnection);
                reader = command.ExecuteReader();

                // Read the results and take the most recent value.
                while (reader.Read()) value = Int32.Parse(reader[variableName].ToString());

            }
            catch (Exception ex)
            {
                // Display an error message.
                Console.WriteLine("SQL query ERROR:\n" + sql);
                Console.WriteLine(ex.Message);
            }

            return value;
        }

        /// <summary>
        /// Reyrieves the results from an SQL query from the database as a <see cref="Table"/>.
        /// </summary>
        /// <param name="SQL">The query that will be performed on the database.</param>
        /// <returns><see cref="Table"/> containing the results of the specified query.</returns>
        public void executeQuery(string sql, out List<Row> rawTable)
        {

            rawTable = new List<Row>();

            // The sql comand and results reader.
            SQLiteCommand command = null;
            SQLiteDataReader reader = null;

            try
            {

                command = new SQLiteCommand(sql, DBConnection);
                reader = command.ExecuteReader();

                // Iterate while the reader has another row to read.
                while (reader.Read())
                {
                    // Holds the new row of the internal table.
                    Row newRow = new Row();

                    // Iterate for each column in the tables.
                    for (int columnIndex = 0; columnIndex < reader.FieldCount; columnIndex++)
                    {

                        string coloumn = reader.GetName(columnIndex);

                        // Holds the value returned from the database.
                        string value;

                        try
                        {
                            // Attempt to reader the value of the current coloumn.
                            value = reader[coloumn].ToString();
                        }
                        catch (Exception e)
                        {
                            //Display and error message and set the value to be an empty string.
                            Console.Out.WriteLine("SQL ERROR: Read table anonmily");
                            Console.Out.WriteLine(newRow.getValue(CashFlow.TRANSACTION_ID_COLOUMN));
                            Console.Out.WriteLine(e.ToString());

                            value = "";
                        }

                        // Add the column - value paor to the new row.
                        newRow.addColoumn(coloumn, value);

                    }
                    // Add the row to the internal table.
                    rawTable.Add(newRow);

                }

            }
            catch (Exception e)
            {
                // Display an error message.
                Console.WriteLine("SQL query ERROR:\n" + sql);
                Console.WriteLine(e.Message);
            }
            finally
            {
                // Dispose of the comand and reader if they are initalised.
                if (command != null)
                {
                    command.Dispose();
                }

                if (reader != null)
                {
                    reader.Dispose();
                }
            }

        }

        public bool checkColoumnTitles(SQLiteCommand command, string SQL, List<string> columns)
        {

            // Initalise the command and reader.
            command = new SQLiteCommand(SQL, DBConnection);
            SQLiteDataReader reader = command.ExecuteReader();


            // Iterate through all the coloumns in the table and 
            // check if they are in the specified list of coloumns.
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string coloumn = reader.GetName(i);

                /*
                 * If the current column is in the column check list then remove 
                 * it from the check list. This signifies that the column is 
                 * accounted for and if another identical title appears if 
                 * should be incorrect. Otherwise the current title is invalid
                 * meaning that the two lists of coloumn titles are different.
                */
                if (columns.Contains(coloumn))
                {
                    columns.Remove(coloumn);
                }
                else
                {
                    return false;
                }
            }

            /*
             * If there are still elements in the column check list then there
             * are row titles missing. This means the row is invalid.
             */
            if (columns.Count != 0)
            {
                return false;
            }

            return true;

        }

        /// <summary>
        /// Creates a SQLite database file using a specified file path.
        /// </summary>
        /// <param name="filePath">File path of the database file.</param>
        public void createDBFile(string filePath)
        {
            SQLiteConnection.CreateFile(filePath);
        }

        /// <summary>
        /// Retrieves the instance of this object.
        /// </summary>
        /// <returns><see cref="SQLHandler"/></returns>
        public static SQLHandler getInstance()
        {
            return INSTANCE;
        }

        /// <summary>
        /// Terminates the connection to the SQL database.
        /// </summary>
        public void terminate()
        {
            if (DBConnection != null) DBConnection.Close();
            Console.Out.WriteLine("Connection Treminated");
        }

        /// <summary>
        /// Updates the database file to be in line with the most recent schema.
        /// </summary>
        public void updateDB()
        {
            // TODO create a method to update a database file to the new schema.
        }
    }
}
