using Bank.Business.Domain.Entities;
using System.Collections.Generic;

namespace Bank.Business.Application.Contracts
{
    public interface ICustomerAccountRepository : IBaseRepository<CustomerAccount>
    {
        CustomerAccount Find(int BankBranch, int BankAccount);
        IEnumerable<CustomerAccount> FindAllCustomerAccounts(int CustomerId);
        IEnumerable<CustomerAccount> FindAllAccounts(int BankBranch);
    }
}
