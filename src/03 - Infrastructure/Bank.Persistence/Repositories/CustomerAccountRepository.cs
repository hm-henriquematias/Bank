using Bank.Business.Domain.Contracts;
using Bank.Business.Domain.Entities;
using Bank.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Infrastructure.Persistence.Repositories
{
    public class CustomerAccountRepository : BaseRepository<CustomerAccount>, ICustomerAccountRepository
    {
        public CustomerAccountRepository(BankContext bankContext) : base(bankContext)
        {
        }

        public async Task<CustomerAccount> Find(int BankBranch, int BankAccount)
        {
            return await BankContext.CustomerAccounts.FirstOrDefaultAsync(
                customerAccount =>
                    customerAccount.BankBranch == BankBranch &&
                    customerAccount.BankAccount == BankAccount
            );
        }

        public Task<CustomerAccount> Find(object branch, object account)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<CustomerAccount>> FindAllAccounts(int BankBranch)
        {
            return await BankContext.CustomerAccounts.Where(customerAccount => customerAccount.BankBranch == BankBranch).ToListAsync();
        }

        public async Task<IEnumerable<CustomerAccount>> FindAllCustomerAccounts(int CustomerId)
        {
            return await BankContext.CustomerAccounts.Where(customerAccount => customerAccount.CustomerId == CustomerId).ToListAsync();
        }

        public async Task<bool> IsExistsAccount(CustomerAccount customerAccount)
        {
            var account = await Find(customerAccount.BankBranch, customerAccount.BankAccount);
            return account != null;
        }
    }
}
