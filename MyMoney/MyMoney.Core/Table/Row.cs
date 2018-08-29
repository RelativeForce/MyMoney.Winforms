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

        public void UpdateColoumn(string updatedCol, string newValue)
        {

            _rawRow[updatedCol] = newValue;

        }

        public string GetValue(string coloumn)
        {
            return _rawRow[coloumn];
        }

    }
}
