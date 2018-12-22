using MyMoney.Infrastructure.EntityFramework;
using MyMoney.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyMoney.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        internal DbSet<T> _table;
        private DatabaseContext _model;

        internal Repository(DatabaseContext model, DbSet<T> table)
        {
            _model = model;
            _table = table;
        }

        public void Dispose()
        {
            _model = null;
            _table = null;
        }

        internal void CheckDisposed()
        {

            if (_model == null || _table == null)
            {
                throw new Exception("Database connection has been disposed!");
            }

        }

        public T Add(T newItem)
        {

            CheckDisposed();
            if (newItem == null) return null;

            try
            {
                _table.Add(newItem);

                _model.SaveChanges();

                return newItem;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        public bool Delete(T item)
        {
            CheckDisposed();
            if (item == null) return false;

            try
            {
                _table.Remove(item);
                _model.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            CheckDisposed();
            if (predicate == null) return null;

            try
            {
                return _table.Where(predicate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public T Find(Func<T, bool> predicate)
        {
            CheckDisposed();
            if (predicate == null) return null;

            try
            {
                return _table.FirstOrDefault(predicate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public IEnumerable<T> All()
        {
            CheckDisposed();
            try
            {
                return _table;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

    }
}
