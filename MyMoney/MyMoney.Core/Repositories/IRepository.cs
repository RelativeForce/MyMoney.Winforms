using System;
using System.Collections.Generic;

namespace MyMoney.Core.Repositories
{
    public interface IRepository<T> : IDisposable
    {

        T Add(T newItem);

        bool Delete(T item);

        IEnumerable<T> Where(Func<T, bool> predicate);

        T Find(Func<T, bool> predicate);

        IEnumerable<T> All();

    }
}
