using MyMoney.Core.Data;
using System.Data.Entity;

namespace MyMoney.Infrastructure.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<Transaction> Transactions { get; set; }

        DbSet<Budget> Budgets { get; set; }

        int SaveChanges();
    }
}
