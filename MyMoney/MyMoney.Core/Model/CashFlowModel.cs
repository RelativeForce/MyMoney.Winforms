using System;

namespace MyMoney.Core.Model
{
    public class CashFlowModel
    {
        public const string DATE_COLOUMN = "Transaction_Date";

        public const string DESCRIPTION_COLOUMN = "Description";

        public const string TRANSACTION_ID_COLOUMN = "Transaction_ID";

        public const string AMOUNT_COLOUMN = "Amount";

        public const string TABLE_NAME = "CashFlow";

        public const int DESCRIPTION_LENGTH = 50;

        public static DateTime StartDate = DateTime.Now;

        public static string[] Coloumns()
        {
            return new[] {
                DESCRIPTION_COLOUMN,
                TRANSACTION_ID_COLOUMN,
                AMOUNT_COLOUMN,
                DATE_COLOUMN
            };
        }

    }
}
