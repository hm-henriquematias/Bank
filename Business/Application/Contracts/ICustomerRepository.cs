using Bank.Business.Domain.Entities;

namespace Bank.Business.Application.Contracts
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Customer Find(string RegistrationNumber);
    }
}
