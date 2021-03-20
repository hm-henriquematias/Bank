using System.Threading.Tasks;

namespace Bank.Business.Domain.Contracts
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customer { get; }
        ICustomerAccountRepository CustomerAccount { get; }
        IFarePlanRepository FarePlan { get; }
        ITransferRepository Transfer { get; }

        Task<bool> Commit();
        Task Rollback();
    }
}
