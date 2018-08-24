namespace MyMoney.Model.Database
{
    public class BudgetModel
    {
        public const string MONTH_COLOUMN = "Month";

        public const string AMOUNT_COLOUMN = "Amount";

        public const string TABLE_NAME = "Budget";

        public static string[] Coloumns()
        {
            return new[] { MONTH_COLOUMN, AMOUNT_COLOUMN };
        }

    }
}
