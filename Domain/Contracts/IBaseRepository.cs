using System;
using System.Collections.Generic;

namespace Bank.Domain.Contracts
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity entity);
        TEntity Find(int id);
        IEnumerable<TEntity> FindAll();
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
