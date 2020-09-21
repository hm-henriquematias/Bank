using Bank.Domain.DomainServicesInterfaces;
using Bank.Domain.Entities;
using Bank.Domain.Exceptions;
using Bank.Domain.Utils;
using Bank.Domain.Validators;
using Bank.Domain.ValueObjects;
using System;

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
