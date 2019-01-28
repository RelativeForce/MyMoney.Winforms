using MyMoney.Core.Data;
using System.Data.Entity;

namespace MyMoney.Infrastructure.EntityFramework
{
    internal class DatabaseContext : DbContext
    {
        public DatabaseContext(string connectionString) : base(connectionString) { }

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<Budget> Budgets { get; set; }
    }
}
