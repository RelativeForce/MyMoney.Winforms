using MyMoney.Core.Model;
using System;

namespace MyMoney.Core.Repositories
{
    public interface IBudgetRepository : IRepository<Budget>
    {
        Budget Find(DateTime date);
    }
}
