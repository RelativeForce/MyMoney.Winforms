using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoney.Model.Database
{
    public class BudgetModel
    {
        public const string MONTH_COLOUMN = "Month";

        public const string AMOUNT_COLOUMN = "Amount";

        public const string TABLE_NAME = "Budget";

        public static string[] Coloumns()
        {
            return new string[] { MONTH_COLOUMN, AMOUNT_COLOUMN };
        }

    }
}
