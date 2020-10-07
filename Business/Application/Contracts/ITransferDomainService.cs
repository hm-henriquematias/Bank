using Bank.Business.Domain.ValueObjects;
using Bank.Business.Domain.Entities;
using System.Threading.Tasks;

namespace Bank.Business.Application.Contracts
{
    public interface ITransferDomainService
    {
        Task<decimal> GetTransferTax(int amountTransfersFromCurrentMonth, FarePlan planOriginAccount);
        Task ValidateTransfer(Transfer transfer);
    }
}
