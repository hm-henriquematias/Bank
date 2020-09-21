using Bank.Domain.Entities;

namespace Bank.Domain.Contracts
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Customer Find(string RegistrationNumber);
    }
}
