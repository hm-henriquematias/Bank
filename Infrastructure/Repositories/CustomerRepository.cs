using Bank.Domain.Contracts;
using Bank.Domain.Entities;
using Bank.Infrastructure.Contexts;
using System.Linq;

namespace Bank.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BankContext bankContext) : base(bankContext)
        {
        }

        public Customer Find(string RegistrationNumber)
        {
            return BankContext.Customers.FirstOrDefault(customer => customer.RegistrationNumber == RegistrationNumber);
        }
    }
}
