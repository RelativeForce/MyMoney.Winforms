using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMoney.Core.Table
{
    public class Row
    {
        private Dictionary<string, string> rawRow = new Dictionary<string, string>();

        public void AddColoumn(string coloumn, string value)
        {
            rawRow.Add(coloumn, value);
        }

        public string[] GetColoumns()
        {
            return rawRow.Keys.ToArray();
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

            rawRow[updatedCol] = newValue;

        }

        public string GetValue(string coloumn)
        {
            return rawRow[coloumn];
        }

        public override string ToString()
        {

            string output = "";

            foreach (string coloumn in rawRow.Keys)
            {
                output += coloumn + ": " + rawRow[coloumn] + " ";
            }

            return output;

        }

        private Boolean CheckColoumValues(Row row)
        {

            foreach (string coloumn in rawRow.Keys)
            {
                if (!rawRow[coloumn].Equals(row.rawRow[coloumn]))
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckColoumTitles(Row row)
        {

            // This holds all the rows that should be in the table.
            List<string> coloumnChecklist = rawRow.Keys.ToList();

            // Iterate through all the row titles in the row
            foreach (string column in row.rawRow.Keys)
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
