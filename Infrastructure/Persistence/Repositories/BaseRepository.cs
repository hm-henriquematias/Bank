using Bank.Business.Application.Contracts;
using Bank.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly BankContext BankContext;

        public BaseRepository(BankContext bankContext)
        {
            BankContext = bankContext;
        }

        public async Task Add(TEntity entity)
        {
            BankContext.Set<TEntity>().Add(entity);
            await BankContext.SaveChangesAsync();
        }

        public async Task<TEntity> Find(int id)
        {
            return await BankContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> FindAll()
        {
            return await BankContext.Set<TEntity>().ToListAsync();
        }

        public async Task Remove(TEntity entity)
        {
            BankContext.Set<TEntity>().Remove(entity);
            await BankContext.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            BankContext.Set<TEntity>().Update(entity);
            await BankContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            BankContext.Dispose();
        }

    }
}
