using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MyMoney.Controllers.TableControllers;
using MyMoney.Model.Table;

namespace MyMoney.Controllers
{

    public class SQLController : ISQLController
    {

        private string filePath;

        private SQLiteConnection DBConnection;

        public SQLController()
        {
            filePath = "";
        }

        public void Connect(string filePath)
        {

            try
            {
                // Attempt to open the connection to the database file.
                DBConnection = new SQLiteConnection(@"Data Source=" + filePath + ";Version=3;");
                DBConnection.Open();

                this.filePath = filePath;
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

        public void Execute(string sql)
        {
            // Declare the command to be executed.
            SQLiteCommand command = null;

            try
            {
                // Process the command
                command = new SQLiteCommand(sql, DBConnection);

                // If the commmand is successful then display the number of rows effected.
                Console.WriteLine("Rows effected: " + command.ExecuteNonQuery());
            }
            catch (Exception)
            {
                // Notify user of SQL failure.
                Console.WriteLine("SQL NON QUERY ERROR:\n" + sql);
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

        public int GetValueInt(string sql, string coloumnName)
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
                while (reader.Read()) value = int.Parse(reader[coloumnName].ToString());

            }
            catch (Exception ex)
            {
                // Display an error message.
                Console.WriteLine("SQL query ERROR:\n" + sql);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Dispose(command, reader);
            }

            return value;
        }

        public void PopulateTable(string sql, ITableController table)
        {

            table.Clear();

            SQLiteCommand command = null;
            SQLiteDataReader reader = null;

            try
            {

                command = new SQLiteCommand(sql, DBConnection);
                reader = command.ExecuteReader();

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
                        newRow.addColoumn(coloumn, value);

                    }
                    // Add the row to the internal table.
                    table.Add(newRow);

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
                Dispose(command, reader);
            }

        }

        public bool CheckColoumnTitles(string SQL, IReadOnlyList<string> columns)
        {

            SQLiteCommand command= new SQLiteCommand(SQL, DBConnection);
            SQLiteDataReader reader = command.ExecuteReader();

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

        public void TryCreateDBFile()
        {
            if (!System.IO.File.Exists(filePath)) SQLiteConnection.CreateFile(filePath);
        }

        public void Disconnect()
        {
            if (DBConnection != null) DBConnection.Close();
            Console.Out.WriteLine("Connection Treminated");
        }

        private void Dispose(SQLiteCommand command, SQLiteDataReader reader)
        {
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
}
