using Bank.Domain.Entities;
using System.Collections.Generic;

namespace Bank.Domain.Contracts
{
    public interface ICustomerAccountRepository : IBaseRepository<CustomerAccount>
    {
        CustomerAccount Find(int BankBranch, int BankAccount);
        IEnumerable<CustomerAccount> FindAllCustomerAccounts(int CustomerId);
        IEnumerable<CustomerAccount> FindAllAccounts(int BankBranch);
    }
}
