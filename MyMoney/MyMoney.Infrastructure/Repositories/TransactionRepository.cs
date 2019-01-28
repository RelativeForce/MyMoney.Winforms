using MyMoney.Infrastructure.EntityFramework;
using MyMoney.Core.Data;
using MyMoney.Core.Repositories;
using System;
using System.Collections.Generic;

namespace MyMoney.Infrastructure.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {

        internal TransactionRepository(DatabaseContext model) : base(model, model.Transactions) {

        }

        public IEnumerable<Transaction> Where(DateTime start, DateTime end) {
            return Where(t => DateTime.Compare(t.Date, start) >= 0 && DateTime.Compare(t.Date, end) <= 0);
        }
    }
}
