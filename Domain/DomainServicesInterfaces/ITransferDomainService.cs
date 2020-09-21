using Bank.Domain.Entities;
using Bank.Domain.ValueObjects;

namespace Bank.Domain.DomainServicesInterfaces
{
    public interface ITransferDomainService
    {
        decimal GetTransferTax(int amountTransfersFromCurrentMonth, FarePlan planOriginAccount);
        void ValidateTransfer(Transfer transfer);
    }
}
