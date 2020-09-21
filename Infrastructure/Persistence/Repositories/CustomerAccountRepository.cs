using Bank.Business.Application.Contracts;
using Bank.Business.Domain.Entities;
using Bank.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Infrastructure.Persistence.Repositories
{
    public class CustomerAccountRepository : BaseRepository<CustomerAccount>, ICustomerAccountRepository
    {
        public CustomerAccountRepository(BankContext bankContext) : base(bankContext)
        {
        }

        public CustomerAccount Find(int BankBranch, int BankAccount)
        {
            return BankContext.CustomerAccounts.FirstOrDefault(
                customerAccount =>
                    customerAccount.BankBranch == BankBranch &&
                    customerAccount.BankAccount == BankAccount
            );
        }

        public IEnumerable<CustomerAccount> FindAllAccounts(int BankBranch)
        {
            return BankContext.CustomerAccounts.Where(customerAccount => customerAccount.BankBranch == BankBranch);
        }

        public IEnumerable<CustomerAccount> FindAllCustomerAccounts(int CustomerId)
        {
            return BankContext.CustomerAccounts.Where(customerAccount => customerAccount.CustomerId == CustomerId);
        }
    }
}
