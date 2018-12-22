using MyMoney.Core.Model;
using MyMoney.Core.Repositories;
using MyMoney.Infrastructure.EntityFramework;
using System;

namespace MyMoney.Infrastructure.Repositories
{
    public class BudgetRepository : Repository<Budget>, IBudgetRepository
    {
        internal BudgetRepository(DatabaseContext model) : base(model, model.Budgets)
        {

        }

        public new Budget Add(Budget budget) {
            CheckDisposed();

            if (budget == null) return null;

            budget.Month = Budget.FilterDate(budget.Month);

            if (Find(b => b.Month.Equals(budget.Month)) != null) {
                return null;
            }
            
            return base.Add(budget);
        }

        public Budget Find(DateTime date) {

            if (date == null) return null;

            return Find(b => b.Month.Equals(Budget.FilterDate(date)));
        }
    }
}
