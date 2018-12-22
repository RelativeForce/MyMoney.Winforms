using MyMoney.Infrastructure.Repositories;
using MyMoney.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMoney.Core;
using MyMoney.Core.Repositories;

namespace MyMoney.Infrastructure
{
    public class Database : IDatabase
    {

        private static string ConnectionString = null;
        public static bool IsDatabaseConnected { get => ConnectionString != null; }

        private DatabaseContext context;

        public ITransactionRepository Transactions { get; }
        public IBudgetRepository Budgets { get; }

        public Database()
        {
            if (ConnectionString == null) {
                throw new Exception($"Connection string not defined. Use {nameof(SetConnection)} to define the file path of the database file (.mdf).");
            }

            context = new DatabaseContext(ConnectionString);

            Transactions = new TransactionRepository(context);
            Budgets = new BudgetRepository(context);

        }

        public static void SetConnection(string filePath) {
            ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={filePath};Integrated Security=True;Connect Timeout=30";
        }

        public bool SaveChanges()
        {
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public void Dispose()
        {

            Transactions.Dispose();
            Budgets.Dispose();

            context.Dispose();
            context = null;
        }
    }
}
