using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoney.Model.Table
{
    /// <summary>
    /// The internal representation of an SQL table. This encapsulates 
    /// the behaviour of storing and retrieving <see cref="Row"/>s while 
    /// also storing the coloumn titles that are used to format those rows.
    /// </summary>
    public class Table
    { 

        /// <summary>
        /// The list of <see cref="Row"/>s that make up the table.
        /// </summary>
        protected List<Row> rawTable;

        /// <summary>
        /// The list of the coloumn titles that this table will consisit of.
        /// </summary>
        protected string[] coloumns;

        /// <summary>
        /// A exception that is specific to row and can onle be thrown by <see cref="Table"/>.
        /// </summary>
        private class RowException : Exception
        {
            /// <summary>
            /// Constructs a new <see cref="RowException"/>
            /// </summary>
            /// <param name="message">
            /// The message that will be outputted when this exception is thrown.
            /// </param>
            public RowException(string message)
                : base(message)
            {

            }

        }

        /// <summary>
        /// Constructs a new <see cref="Table"/>.
        /// </summary>
        /// <param name="coloumnTitles">
        /// A list of strings that contains the title of each 
        /// column of the table. No colloumn may have an empty 
        /// title and they must be unique.
        /// </param>
        public Table(string[] coloumnTitles)
        {

            checkTableColoumnTitles(coloumnTitles);

            // If no exception is thrown then initalise the table.
            this.coloumns = coloumnTitles;
            this.rawTable = new List<Row>();

        }

        /// <summary>
        /// Checks that in a specified list of coloumn titles there are no empty titles and all are unique.
        /// </summary>
        /// <param name="coloumnTitles">Coloumn titles</param>
        private void checkTableColoumnTitles(string[] coloumnTitles)
        {

            if (coloumnTitles == null)
            {
                throw new ArgumentNullException("No coloumn titles specified. 'coloumnTitles' is null.");
            }
            else if (coloumnTitles.Length == 0)
            {
                throw new ArgumentException("No coloumn titles specified.");
            }

            // Check if any titles are empty strings or they are not unique
            List<string> coloumnTitlesList = coloumnTitles.ToList<string>();

            // Loop thorugh all of the titles in the title lst and if any are 
            // duplicates or empty strings throw an argument exception.
            for (int index = coloumnTitles.Length - 1; index >= 0; index--)
            {

                // Holds the current title being evaluated
                string title = coloumnTitlesList[index];

                // Remove that title from the list
                coloumnTitlesList.RemoveAt(index);

                // Check if the title is empty or a duplicate title.
                if (title.Equals(""))
                {
                    throw new ArgumentException("Coloumn Titles must not be empty.");
                }
                else if (coloumnTitlesList.Contains(title))
                {
                    throw new ArgumentException("Coloumn Titles must be unique.");
                }
            }

        }

        /// <summary>
        /// Checks all the validity of all the specified rows.
        /// </summary>
        /// <param name="rows">Rows to be checked.</param>
        private void checkBaseRows(Row[] rows)
        {

            // Iterate thorught all the rows.
            foreach (Row row in rows)
            {

                if (row == null)
                {
                    throw new ArgumentNullException("Row cannot be null.");
                }
                // If a row is invalid throw an exception.
                else if (!check(row))
                {

                    throw new RowException("Invalid row");
                }

            }

        }

        /// <summary>
        /// Constructs a new <see cref="Table"/> using an inital set of <see cref="Row"/>s.
        /// </summary>
        /// <param name="coloumnTitles">Coloumn titles of the table.</param>
        /// <param name="rows">Rows in the table.</param>
        public Table(string[] coloumnTitles, Row[] rows)
        {

            checkTableColoumnTitles(coloumnTitles);

            // If no exception is thrown then initalise the table.
            this.coloumns = coloumnTitles;

            checkBaseRows(rows);

            this.rawTable = new List<Row>(rows);

        }

        /// <summary>
        /// Inserts a <see cref="Row"/> into the table at a specific index.
        /// </summary>
        /// <param name="index">Position of new row.</param>
        /// <param name="row">new row</param>
        public void insertRow(int index, Row row)
        {

            if (row == null)
            {
                throw new ArgumentNullException("Row is null.");
            }
            /*
             * If the row is not valid then throw an exception, 
             * otherwise add the row to the table.
            */
            if (!check(row))
            {
                throw new RowException("Invalid Row: " + row.ToString());
            }

            if (index < 0 || index > rawTable.Count)
            {
                throw new IndexOutOfRangeException();
            }

            rawTable.Insert(index, row);

        }

        /// <summary>
        /// Retrieves a <see cref="Row"/> based on a value of one of the rows coloumns.
        /// </summary>
        /// <param name="coloumn">The column that contains the specified value.</param>
        /// <param name="value">The value that determines if a row will be returned or not.</param>
        /// <returns></returns>
        public Row getRow(string coloumn, string value)
        {

            // Iterate through all the orws in the table.
            foreach (Row row in rawTable)
            {

                // If the current row has the column value pair that is desired then return it.
                if (row.getValue(coloumn).Equals(value))
                {

                    return row;

                }

            }

            // Otherwise return null.
            return null;


        }

        /// <summary>
        /// Inserts a valid row into the table.
        /// </summary>
        /// <param name="row">The <see cref="Row"/> to be added to the table.</param>
        public void addRow(Row row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("Row is null.");
            }
            /*
             * If the row is not valid then throw an exception, 
             * otherwise add the row to the table.
            */
            if (!check(row))
            {
                throw new RowException("Invalid Row: " + row.ToString());
            }

            rawTable.Add(row);

        }

        /// <summary>
        /// Removes a specified row from the table.
        /// </summary>
        /// <param name="row">The <see cref="Row"/> to be removed.</param>
        public void remove(Row row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("Row is null.");
            }
            rawTable.Remove(row);
        }

        /// <summary>
        /// Retrieves all the rows in the table as an array of <see cref="Row"/>s.
        /// </summary>
        /// <returns>An array of <see cref="Row"/>s.</returns>
        public Row[] getRows()
        {

            return rawTable.ToArray<Row>();

        }

        /// <summary>
        /// Retrieves the <see cref="Row"/> at a specified index in the 
        /// <see cref="Table"/>.
        /// </summary>
        /// <param name="index">
        /// <code>int</code> position of <see cref="Row"/> in 
        /// <see cref="Table"/>. Must be greater than or equal to zero.
        /// </param>
        /// <returns><see cref="Row"/> at the position of index.</returns>
        public Row getRow(int index)
        {
            if (index < 0 || index >= rawTable.Count)
            {
                throw new ArgumentOutOfRangeException("Index out of table bounds.");
            }

            return rawTable.ElementAt(index);
        }

        /// <summary>
        /// Retrieves the array of coloumn titles of the <see cref="Table"/>.
        /// </summary>
        /// <returns><code>string</code> titles</returns>
        public string[] getColoumns()
        {
            return coloumns;
        }

        /// <summary>
        /// Retrieves the string representation of the <see cref="Table"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            // Holds the output of the method.
            string output = "";

            // Lists all the rows in the table each on a new line
            foreach (Row row in rawTable)
            {
                output += row.ToString() + "\n";
            }

            return output;

        }

        /// <summary>
        /// Checks whether a specified <see cref="Row"/> is valid according to 
        /// the specification of this <see cref="Table"/>.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool check(Row row)
        {

            // This holds all the rows that should be in the table.
            List<string> coloumnChecklist = this.coloumns.ToList<string>();

            // Iterate through all the row titles in the row
            foreach (string coloumn in row.getColoumns())
            {
                /*
                 * If the current column is in the column check list then remove 
                 * it from the check list. This signifies that the column is 
                 * accounted for and if another identical title appears if 
                 * should be incorrect. Otherwise the current title is invalid
                 * meaning that the whole row is invalid.
                */
                if (coloumnChecklist.Contains(coloumn))
                {
                    coloumnChecklist.Remove(coloumn);
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
            if (coloumnChecklist.Count != 0)
            {
                return false;
            }

            return true;


        }

        /// <summary>
        /// Empties the tables of all rows.
        /// </summary>
        public void clear()
        {
            rawTable.Clear();
        }
    }
}
