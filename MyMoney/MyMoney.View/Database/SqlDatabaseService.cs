using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MyMoney.Core.Database;
using MyMoney.Core.Table;

namespace MyMoney.Database
{

    public class SqlDatabaseService : IDatabaseService
    {

        private string _filePath;

        private SQLiteConnection _dbConnection;

        public SqlDatabaseService()
        {
            _filePath = "";
        }

        public void Connect(string filePath)
        {

            try
            {
                // Attempt to open the connection to the database file.
                _dbConnection = new SQLiteConnection(@"Data Source=" + filePath + ";Version=3;");
                _dbConnection.Open();

                this._filePath = filePath;
                Console.Out.WriteLine("Connection Establised");

            }
            catch (Exception ex)
            {
                // If this fails exit the program.
                Console.Out.WriteLine("Database Failure");
                Console.WriteLine(ex);
                Environment.Exit(1);

            }
        }

        public void Execute(Command command)
        {
            // Declare the command to be executed.
            SQLiteCommand sqLiteCommand = null;

            try
            {
                // Process the command
                sqLiteCommand = new SQLiteCommand(command.Text, _dbConnection);

                // If the commmand is successful then display the number of rows effected.
                Console.WriteLine("Rows effected: " + sqLiteCommand.ExecuteNonQuery());
            }
            catch (Exception)
            {
                // Notify user of SQL failure.
                Console.WriteLine("SQL NON QUERY ERROR:\n" + command.Text);
            }
            finally
            {
                // If the command was created then dispose of it.
                if (sqLiteCommand != null)
                {
                    sqLiteCommand.Dispose();
                }
            }

        }

        public int GetValueInt(Command command, string coloumnName)
        {

            // Holds the value that will be obtained from the operation.
            int value = 0;

            // The sql comand and results reader.
            SQLiteCommand sqLiteCommand = null;
            SQLiteDataReader reader = null;

            try
            {

                sqLiteCommand = new SQLiteCommand(command.Text, _dbConnection);
                reader = sqLiteCommand.ExecuteReader();

                // Read the results and take the most recent value.
                while (reader.Read()) value = int.Parse(reader[coloumnName].ToString());

            }
            catch (Exception ex)
            {
                // Display an error message.
                Console.WriteLine("SQL query ERROR:\n" + command.Text);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Dispose(sqLiteCommand, reader);
            }

            return value;
        }

        public void PopulateTable(Command command, ITableController table)
        {

            table.Clear();

            SQLiteCommand sqLiteCommand = null;
            SQLiteDataReader reader = null;

            try
            {

                sqLiteCommand = new SQLiteCommand(command.Text, _dbConnection);
                reader = sqLiteCommand.ExecuteReader();

                while (reader.Read())
                {
                    Row newRow = new Row();

                    for (int columnIndex = 0; columnIndex < reader.FieldCount; columnIndex++)
                    {

                        string coloumn = reader.GetName(columnIndex);

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
                            Console.Out.WriteLine(e.ToString());

                            value = "";
                        }

                        // Add the column - value paor to the new row.
                        newRow.AddColoumn(coloumn, value);

                    }
                    // Add the row to the internal table.
                    table.AddRow(newRow);

                }

            }
            catch (Exception e)
            {
                // Display an error message.
                Console.WriteLine("SQL query ERROR:\n" + command.Text);
                Console.WriteLine(e.Message);
            }
            finally
            {
                Dispose(sqLiteCommand, reader);
            }

        }

        public bool CheckColoumnTitles(Command command, IReadOnlyList<string> columns)
        {

            SQLiteCommand sqLiteCommand = new SQLiteCommand(command.Text, _dbConnection);
            SQLiteDataReader reader = sqLiteCommand.ExecuteReader();

            List<string> remainingColumns = new List<string>(columns);

            for (int i = 0; i < reader.FieldCount; i++)
            {
                string coloumn = reader.GetName(i);

                if (remainingColumns.Contains(coloumn))
                {
                    remainingColumns.Remove(coloumn);
                }
                else
                {
                    return false;
                }
            }

            if (remainingColumns.Count != 0)
            {
                return false;
            }

            return true;

        }

        public void TryCreateDatabaseFile()
        {
            if (!System.IO.File.Exists(_filePath)) SQLiteConnection.CreateFile(_filePath);
        }

        public void Disconnect()
        {
            _dbConnection?.Close();
            Console.Out.WriteLine("Connection Treminated");
        }

        private void Dispose(SQLiteCommand command, SQLiteDataReader reader)
        {
            command?.Dispose();

            reader?.Dispose();
        }
    }
}
