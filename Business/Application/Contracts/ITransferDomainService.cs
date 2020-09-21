using Bank.Business.Domain.ValueObjects;
using Bank.Business.Domain.Entities;

namespace Bank.Business.Application.Contracts
{
    public interface ITransferDomainService
    {
        decimal GetTransferTax(int amountTransfersFromCurrentMonth, FarePlan planOriginAccount);
        void ValidateTransfer(Transfer transfer);
    }
}
