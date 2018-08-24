using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMoney.Model.Table
{
    /// <summary>
    /// Encapsulates the behaviour of a row in the <see cref="Table"/>.
    /// </summary>
    public class Row
    {
        /// <summary>
        /// Holds the column titles as the keys and the data the that is assigned to it.
        /// </summary>
        private Dictionary<string, string> rawRow = new Dictionary<string, string>();

        /// <summary>
        /// Adds a column and assigned value to the current coloumn
        /// </summary>
        /// <param name="coloumn">
        /// The title of the coloumn.
        /// </param>
        /// <param name="value">
        /// The value of that coloumn.
        /// </param>
        public void addColoumn(string coloumn, string value)
        {
            // Adds the column and value to the row.
            rawRow.Add(coloumn, value);

        }

        /// <summary>
        /// Retrieves an array of the column titles in the row.
        /// </summary>
        /// <returns><code>string[]</code></returns>
        public string[] getColoumns()
        {
            return rawRow.Keys.ToArray();
        }

        /// <summary>
        /// Gets the hascode of this instance of a Row.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Checks equality between this and another object.
        /// </summary>
        /// <param name="o">The object that is to be checked.</param>
        /// <returns></returns>
        public override Boolean Equals(object o)
        {
            // If the paramter object is an instance of Row
            if (o is Row)
            {
                Row row = o as Row;

                // If the coloumn tites of both rows are the same.
                if (checkColoumTitles(row))
                {
                    // If the coloumn values of both rows are the same.
                    if (checkColoumValues(row))
                    {
                        return true;
                    }

                }
            }


            return false;
        }
        
        /// <summary>
        /// Updates the value assigned to a specifed coloumn with a new specifed value.
        /// </summary>
        /// <param name="updatedCol">
        /// The specified column that will be updated. This 
        /// column must be present in the row.
        /// </param>
        /// <param name="newValue">
        /// The new value for that column.
        /// </param>
        public void updateColoumn(string updatedCol, string newValue)
        {

            rawRow[updatedCol] = newValue;

        }

        /// <summary>
        /// Retrieves the value of a specified coloumn.
        /// </summary>
        /// <param name="coloumn"><code>string</code></param>
        /// <returns><code>string</code> assigned to the specified coloumn.</returns>
        public string getValue(string coloumn)
        {
            return rawRow[coloumn];
        }

        /// <summary>
        /// The string represntation of this instance of Row.
        /// </summary>
        /// <returns><code>string</code></returns>
        public override string ToString()
        {

            string output = "";
            // Iterate through all the coloumns of the Row
            foreach (string coloumn in rawRow.Keys)
            {
                // Add the coloumn and value to the output string.
                output += coloumn + ": " + rawRow[coloumn] + " ";

            }

            return output;

        }

        /// <summary>
        /// Check that this and a specified Row have the same column values.
        /// </summary>
        /// <param name="row">Row to be checked.</param>
        /// <returns><code>boolean</code></returns>
        private Boolean checkColoumValues(Row row)
        {
            // Iterate through each column in this row.
            foreach (string coloumn in rawRow.Keys)
            {
                // If the value assigned to the coloumn in both rows are the same.
                if (!rawRow[coloumn].Equals(row.rawRow[coloumn]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check that this and a specified row have the same column titles.
        /// </summary>
        /// <param name="row">Row to be checked.</param>
        /// <returns><code>boolean</code></returns>
        private Boolean checkColoumTitles(Row row)
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
