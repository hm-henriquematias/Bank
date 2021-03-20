using Bank.Business.Domain.Entities;
using Bank.Business.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Bank.Business.Domain.Contracts
{
    public interface ITransferDomainService
    {
        Task<decimal> GetTransferTax(int amountTransfersFromCurrentMonth, FarePlan planOriginAccount);
        Task ValidateTransfer(Transfer transfer);
    }
}
