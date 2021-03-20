using Bank.Business.Domain.Contracts;
using Bank.Infrastructure.Persistence.Contexts;
using System.Threading.Tasks;

namespace Bank.Infrastructure.Persistence.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly BankContext _context;

        public ICustomerRepository Customer { get; }
        public ICustomerAccountRepository CustomerAccount { get; }
        public IFarePlanRepository FarePlan { get; }
        public ITransferRepository Transfer { get; }

        public UnitOfWork(ICustomerRepository customerRepository,
                          ICustomerAccountRepository customerAccountRepository,
                          IFarePlanRepository farePlanRepository,
                          ITransferRepository transferRepository)
        {
            Customer = customerRepository;
            CustomerAccount = customerAccountRepository;
            FarePlan = farePlanRepository;
            Transfer = transferRepository;
        }

        public async Task<bool> Commit()
            => await _context.SaveChangesAsync().ConfigureAwait(false) > 0;

        public Task Rollback()
            => Task.CompletedTask;
    }
}
