using Bank.Business.Domain.Entities;

namespace Bank.Infrastructure.Persistence.Contracts
{
    public interface IAccountService
    {
        bool IsExistAccount(CustomerAccount account);
        void Update(CustomerAccount account);
    }
}
