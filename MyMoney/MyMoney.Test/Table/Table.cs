using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMoney.Core.Table
{

    public class Table
    {

        private readonly List<Row> _rawTable;

        private readonly string[] _coloumns;

        private class RowException : Exception
        {

            public RowException(string message)
                : base(message)
            {

            }

        }

        public Table(string[] coloumnTitles)
        {

            CheckTableColoumnTitles(coloumnTitles);

            // If no exception is thrown then initalise the table.
            _coloumns = coloumnTitles;
            _rawTable = new List<Row>();

        }

        private void CheckTableColoumnTitles(string[] coloumnTitles)
        {

            if (coloumnTitles == null)
            {
                throw new ArgumentNullException("No coloumn titles specified. 'coloumnTitles' is null.");
            }

            if (coloumnTitles.Length == 0)
            {
                throw new ArgumentException("No coloumn titles specified.");
            }

            List<string> coloumnTitlesList = coloumnTitles.ToList();

            for (int index = coloumnTitles.Length - 1; index >= 0; index--)
            {

                string title = coloumnTitlesList[index];

                coloumnTitlesList.RemoveAt(index);

                if (title.Equals(""))
                {
                    throw new ArgumentException("Coloumn Titles must not be empty.");
                }

                if (coloumnTitlesList.Contains(title))
                {
                    throw new ArgumentException("Coloumn Titles must be unique.");
                }
            }

        }

        private void CheckBaseRows(Row[] rows)
        {

            // Iterate thorught all the rows.
            foreach (Row row in rows)
            {

                if (row == null)
                {
                    throw new ArgumentNullException("Row cannot be null.");
                }

                if (!Check(row))
                {
                    throw new RowException("Invalid row");
                }

            }

        }

        public Table(string[] coloumnTitles, Row[] rows)
        {

            CheckTableColoumnTitles(coloumnTitles);

            // If no exception is thrown then initalise the table.
            _coloumns = coloumnTitles;

            CheckBaseRows(rows);

            _rawTable = new List<Row>(rows);

        }

        public void InsertRow(int index, Row row)
        {

            if (row == null)
            {
                throw new ArgumentNullException("Row is null.");
            }
 
            if (!Check(row))
            {
                throw new RowException("Invalid Row: " + row);
            }

            if (index < 0 || index > _rawTable.Count)
            {
                throw new IndexOutOfRangeException();
            }

            _rawTable.Insert(index, row);

        }

        public Row GetRow(string coloumn, string value)
        {

            // Iterate through all the orws in the table.
            foreach (Row row in _rawTable)
            {

                // If the current row has the column value pair that is desired then return it.
                if (row.GetValue(coloumn).Equals(value))
                {
                    return row;
                }
            }

            // Otherwise return null.
            return null;

        }

        public void AddRow(Row row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("Row is null.");
            }

            if (!Check(row))
            {
                throw new RowException("Invalid Row: " + row);
            }

            _rawTable.Add(row);

        }

        public void Remove(Row row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("Row is null.");
            }
            _rawTable.Remove(row);
        }

        public Row[] GetRows()
        {

            return _rawTable.ToArray<Row>();

        }

        public Row GetRow(int index)
        {
            if (index < 0 || index >= _rawTable.Count)
            {
                throw new ArgumentOutOfRangeException("Index out of table bounds.");
            }

            return _rawTable.ElementAt(index);
        }

        public string[] GetColoumns()
        {
            return _coloumns;
        }


        public override string ToString()
        {

            // Holds the output of the method.
            string output = "";

            // Lists all the rows in the table each on a new line
            foreach (Row row in _rawTable)
            {
                output += row + "\n";
            }

            return output;

        }

        public bool Check(Row row)
        {

            // This holds all the rows that should be in the table.
            List<string> coloumnChecklist = _coloumns.ToList();

            // Iterate through all the row titles in the row
            foreach (string coloumn in row.GetColoumns())
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

        public void Clear()
        {
            _rawTable.Clear();
        }
    }
}
