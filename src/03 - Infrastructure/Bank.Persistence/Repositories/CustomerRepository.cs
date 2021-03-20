using Bank.Business.Domain.Contracts;
using Bank.Business.Domain.Entities;
using Bank.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bank.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BankContext bankContext) : base(bankContext)
        {
        }

        public async Task<Customer> Find(string RegistrationNumber)
            => await BankContext.Customers.FirstOrDefaultAsync(customer => customer.RegistrationNumber == RegistrationNumber);
    }
}
