using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Business.Application.Contracts
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Add(TEntity entity);
        Task<TEntity> Find(int id);
        Task<IEnumerable<TEntity>> FindAll();
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
    }
}
