using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMoney.Core.Table
{

    public class Table
    {

        private readonly List<Row> _rows;

        private readonly string[] _coloumns;

        public Table(string[] coloumnTitles)
        {

            CheckTableColoumnTitles(coloumnTitles);

            // If no exception is thrown then initalise the table.
            _coloumns = coloumnTitles;
            _rows = new List<Row>();

        }

        public void InsertRow(int index, Row row)
        {

            if (row == null)
            {
                throw new ArgumentNullException("Row is null.");
            }
 
            if (!Check(row))
            {
                throw new ArgumentException("Invalid Row: " + row);
            }

            if (index < 0 || index > _rows.Count)
            {
                throw new IndexOutOfRangeException();
            }

            _rows.Insert(index, row);

        }

        public Row GetRow(string coloumn, string value)
        {

            // Iterate through all the orws in the table.
            foreach (Row row in _rows)
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
                throw new ArgumentNullException("Row cannot be null.");
            }

            if (!Check(row))
            {
                throw new ArgumentException("Invalid Row");
            }

            _rows.Add(row);

        }

        public void Remove(Row row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("Row cannot be null.");
            }
            _rows.Remove(row);
        }

        public Row[] GetRows()
        {

            return _rows.ToArray<Row>();

        }

        public Row GetRow(int index)
        {
            if (index < 0 || index >= _rows.Count)
            {
                throw new ArgumentOutOfRangeException("Index out of table bounds.");
            }

            return _rows.ElementAt(index);
        }

        public string[] GetColoumns()
        {
            return _coloumns;
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
            _rows.Clear();
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

            var coloumnTitlesList = coloumnTitles.ToList();

            for (var index = coloumnTitles.Length - 1; index >= 0; index--)
            {

                var title = coloumnTitlesList[index];

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

            foreach (var row in rows)
            {

                if (row == null)
                {
                    throw new ArgumentNullException("Row cannot be null.");
                }

                if (!Check(row))
                {
                    throw new ArgumentException("Invalid row");
                }

            }

        }

    }
}
