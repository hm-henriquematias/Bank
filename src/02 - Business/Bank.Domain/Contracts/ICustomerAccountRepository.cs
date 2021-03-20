using Bank.Business.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Business.Domain.Contracts
{
    public interface ICustomerAccountRepository : IBaseRepository<CustomerAccount>
    {
        Task<CustomerAccount> Find(int BankBranch, int BankAccount);
        Task<IEnumerable<CustomerAccount>> FindAllCustomerAccounts(int CustomerId);
        Task<IEnumerable<CustomerAccount>> FindAllAccounts(int BankBranch);
        Task<bool> IsExistsAccount(CustomerAccount customerAccount);
        Task<CustomerAccount> Find(object branch, object account);
    }
}
