using Bank.Domain.Entities;
using System.Collections.Generic;

namespace Bank.Domain.Contracts
{
    public interface ITransferRepository : IBaseRepository<Transfer>
    {
        Transfer Find(CustomerAccount from, CustomerAccount to);
        IEnumerable<Transfer> FindAll(CustomerAccount customerAccount);
        IEnumerable<Transfer> FindAllOriginAccount(CustomerAccount from);
        Transfer FindOriginAccount(CustomerAccount from);
        Transfer FindDestinationAccount(CustomerAccount to);
        IEnumerable<Transfer> FindAllDestinationAccount(CustomerAccount to);
        int CountAmountTransferInCurrentMonth(CustomerAccount account);
    }
}
