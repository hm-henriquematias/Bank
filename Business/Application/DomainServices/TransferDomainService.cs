using Bank.Business.Application.Contracts;
using Bank.Business.Domain.Entities;
using Bank.Business.Domain.Exceptions;
using Bank.Business.Domain.Utils;
using Bank.Business.Domain.Validators;
using Bank.Business.Domain.ValueObjects;

namespace Bank.Domain.DomainServices
{
    public class TransferDomainService : ITransferDomainService
    {
        public decimal GetTransferTax(int amountTransfersFromCurrentMonth, FarePlan planOriginAccount)
        {
            return (HasFreeTransfersAvailableInOriginAccount(amountTransfersFromCurrentMonth, planOriginAccount)) ? 0 : Tax.TransferTax;
        }

        public bool HasFreeTransfersAvailableInOriginAccount(int amountTransfersFromCurrentMonth, FarePlan planOriginAccount)
        {
            return amountTransfersFromCurrentMonth < planOriginAccount.FreeTransfersQuantity;
        }

        public void ValidateTransfer(Transfer transfer)
        {
            TransferValidator validator = new TransferValidator()
            {
                TransferEntity = transfer
            };

            validator.Validate();

            if (!validator.IsValid)
                throw new InvalidTransferException(validator.GetValidateMessage());
        }
    }
}
