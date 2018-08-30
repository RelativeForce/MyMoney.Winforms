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

        public override bool Equals(object o)
        {
            if (!(o is Row)) return false;
            var row = (Row) o;

            return IsSameAsThis(row);
        }

        private bool IsSameAsThis(Row row)
        {
            return HasSameColoumNames(row) && HasSameColoumValues(row);
        }

        private bool HasSameColoumValues(Row row)
        {
            foreach (var coloumn in _rawRow.Keys)
            {
                if (!_rawRow[coloumn].Equals(row._rawRow[coloumn]))
                {
                    return false;
                }
            }
            return true;
        }

        private bool HasSameColoumNames(Row row)
        {

            var coloumnChecklist = _rawRow.Keys.ToList();

            foreach (var column in row._rawRow.Keys)
            {
 
                if (coloumnChecklist.Contains(column))
                {
                     coloumnChecklist.Remove(column);
                }
                else
                {
                     return false;
                }
            }

          return coloumnChecklist.Count == 0;
        }
   }
}
