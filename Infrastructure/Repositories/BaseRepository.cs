using Bank.Domain.Contracts;
using Bank.Infrastructure.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly BankContext BankContext;

        public BaseRepository(BankContext bankContext)
        {
            BankContext = bankContext;
        }

        public void Add(TEntity entity)
        {
            BankContext.Set<TEntity>().Add(entity);
            BankContext.SaveChanges();
        }

        public TEntity Find(int id)
        {
            return BankContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> FindAll()
        {
            return BankContext.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            BankContext.Set<TEntity>().Remove(entity);
            BankContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            BankContext.Set<TEntity>().Update(entity);
            BankContext.SaveChanges();
        }

        public void Dispose()
        {
            BankContext.Dispose();
        }

    }
}
