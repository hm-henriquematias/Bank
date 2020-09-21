using Bank.Business.Application.Contracts;
using Bank.Infrastructure.Persistence.Contracts;
using Bank.Business.Domain.Entities;
using Bank.Business.Domain.Exceptions;

namespace Bank.Infrastructure.Persistence.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferDomainService _transferDomainService;
        private readonly ITransferRepository _transferRepository;
        private readonly IFarePlanRepository _farePlanRepository;
        private readonly IAccountService _accountService;

        public TransferService(ITransferDomainService transferService, 
            ITransferRepository transferRepository,
            IFarePlanRepository farePlanRepository,
            IAccountService accountService)
        {
            _transferDomainService = transferService;
            _transferRepository = transferRepository;
            _farePlanRepository = farePlanRepository;
            _accountService = accountService;
        }

        public void Transfer(Transfer transfer)
        {
            if (!_accountService.IsExistAccount(transfer.From))
                throw new InvalidCustomerAccountException("Conta de origem não existe");
            if (!_accountService.IsExistAccount(transfer.To))
                throw new InvalidCustomerAccountException("Conta de destino não existe");

            _transferDomainService.ValidateTransfer(transfer);

            transfer.Tax = GetTransferTaxToOriginAccount(transfer.From);

            _transferRepository.Add(transfer);

            transfer.MakeTransfer();

            UpdateAccountsAfterTransfer(transfer);
        }

        public decimal GetTransferTaxToOriginAccount(CustomerAccount originAccount)
        {
            FarePlan farePlanOriginAccount = _farePlanRepository.Find(originAccount.FarePlanId);
            int amountTransfersFromCurrentMonth = _transferRepository.CountAmountTransferInCurrentMonth(originAccount); 
            return _transferDomainService.GetTransferTax(amountTransfersFromCurrentMonth, farePlanOriginAccount);
        }

        public void UpdateAccountsAfterTransfer(Transfer transfer)
        {
            _accountService.Update(transfer.From);
            _accountService.Update(transfer.To);
        }
    }
}
