using System.Collections.Generic;
using System.Linq;

namespace MyMoney.Core.Table
{
    public class Row
    {
        private readonly Dictionary<string, string> _rawRow;

        public Row()
        {
            _rawRow = new Dictionary<string, string>();
        }

        public void AddColoumn(string coloumn, string value)
        {
            _rawRow.Add(coloumn, value);
        }

        public string[] GetColoumns()
        {
            return _rawRow.Keys.ToArray();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object o)
        {

            if (!(o is Row)) return false;

            Row row = o as Row;

            if (!CheckColoumTitles(row)) return false;

            if (!CheckColoumValues(row)) return false;

            return true;

        }

        public void UpdateColoumn(string updatedCol, string newValue)
        {

            _rawRow[updatedCol] = newValue;

        }

        public string GetValue(string coloumn)
        {
            return _rawRow[coloumn];
        }

        public override string ToString()
        {

            string output = "";

            foreach (string coloumn in _rawRow.Keys)
            {
                output += coloumn + ": " + _rawRow[coloumn] + " ";
            }

            return output;

        }

        private bool CheckColoumValues(Row row)
        {

            foreach (string coloumn in _rawRow.Keys)
            {
                if (!_rawRow[coloumn].Equals(row._rawRow[coloumn]))
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckColoumTitles(Row row)
        {

            // This holds all the rows that should be in the table.
            List<string> coloumnChecklist = _rawRow.Keys.ToList();

            // Iterate through all the row titles in the row
            foreach (string column in row._rawRow.Keys)
            {
                /*
                 * If the current column is in the column check list then remove 
                 * it from the check list. This signifies that the column is 
                 * accounted for and if another identical title appears if 
                 * should be incorrect. Otherwise the current title is invalid
                 * meaning that the whole row is invalid.
                */
                if (coloumnChecklist.Contains(column))
                {
                    coloumnChecklist.Remove(column);
                }
                else
                {
                    return false;
                }
            }

            /*
             * If there are still elements in the column check list then there
             * are row titles missing. This meeans the row is invalid.
             */
            if (coloumnChecklist.Count != 0)
            {
                return false;
            }

            return true;

        }

    }
}
