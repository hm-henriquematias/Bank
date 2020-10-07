using Bank.Business.Domain.Entities;
using System.Threading.Tasks;

namespace Bank.Business.Application.Contracts
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer> Find(string RegistrationNumber);
    }
}
