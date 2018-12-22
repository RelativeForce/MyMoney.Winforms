using System;


namespace MyMoney.Core.Model
{
    public class Budget
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime Month { get; set; }

        public Budget() {}

        public Budget(DateTime month, double amount) {

            Month = FilterDate(month);
            Amount = amount;
        }

        public static DateTime FilterDate(DateTime date)
        {
            var filteredDate = new DateTime(date.Year, date.Month, 1);

            return filteredDate;
        }
    }
}
