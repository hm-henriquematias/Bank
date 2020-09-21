using Bank.Business.Application.Contracts;
using Bank.Business.Domain.Entities;
using Bank.Infrastructure.Persistence.Contexts;
using System.Linq;

namespace Bank.Infrastructure.Persistence.Repositories
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
