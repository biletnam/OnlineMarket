using System;
using System.Collections.Generic;

namespace OnlineMarket.DataAccessLayer.Interfaces
{
    public interface IRepository<T>
    {
        IList<T> GetAll();

        IList<T> Find(Func<T, bool> condition);

        void Add(T item);

        void Update(T item);

        void Remove(T item);
    }
}
