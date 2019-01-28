using MyMoney.Core.Data;
using System;
using System.Collections.Generic;

namespace MyMoney.Core.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        IEnumerable<Transaction> Where(DateTime start, DateTime end);
    }
}
