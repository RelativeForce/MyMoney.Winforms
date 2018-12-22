using MyMoney.Core.Model;
using MyMoney.Core.Repositories;
using System;

namespace MyMoney.Core
{
    public interface IDatabase : IDisposable
    {
        ITransactionRepository Transactions { get; }
        IBudgetRepository Budgets { get; }

        bool SaveChanges();
    }
}
