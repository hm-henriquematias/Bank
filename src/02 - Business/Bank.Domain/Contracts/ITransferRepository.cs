using Bank.Business.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Business.Domain.Contracts
{
    public interface ITransferRepository : IBaseRepository<Transfer>
    {
        Task<Transfer> Find(CustomerAccount from, CustomerAccount to);
        Task<IEnumerable<Transfer>> FindAll(CustomerAccount customerAccount);
        Task<IEnumerable<Transfer>> FindAllOriginAccount(CustomerAccount from);
        Task<Transfer> FindOriginAccount(CustomerAccount from);
        Task<Transfer> FindDestinationAccount(CustomerAccount to);
        Task<IEnumerable<Transfer>> FindAllDestinationAccount(CustomerAccount to);
        Task<int> CountAmountTransferInCurrentMonth(CustomerAccount account);
    }
}
