using MyMoney.Core.Data;
using System;

namespace MyMoney.Core.Repositories
{
    public interface IBudgetRepository : IRepository<Budget>
    {
        Budget Find(DateTime date);
    }
}
