using System;

namespace MyMoney.Core.Data
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }

        public Transaction() {

        }

        public Transaction(DateTime date, string description, double amount) {
            Date = date;
            Description = description;
            Amount = amount;
        }
    }
}
