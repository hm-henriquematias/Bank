using Bank.Business.Domain.Entities;
using System.Threading.Tasks;

namespace Bank.Business.Domain.Contracts
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer> Find(string RegistrationNumber);
    }
}
