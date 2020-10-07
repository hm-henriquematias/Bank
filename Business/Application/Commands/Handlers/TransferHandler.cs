using Bank.Business.Application.Commands.Requests;
using Bank.Business.Application.Commands.Responses;
using Bank.Business.Application.Contracts;
using Bank.Business.Application.ValidationScope;
using Bank.Business.Application.ValidationScope.Transfer;
using Bank.Business.Domain.Entities;
using Bank.Business.Domain.Mappers;
using Bank.Business.Domain.Utils;
using Bank.Business.Domain.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Business.Application.Commands.Handlers
{
    public class TransferHandler : IRequestHandler<TransferRequest, TransferResponse>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IFarePlanRepository _farePlanRepository;
        private readonly ICustomerAccountRepository _accountRepository;

        public TransferHandler(ITransferRepository transferRepository, IFarePlanRepository farePlanRepository, ICustomerAccountRepository accountRepository)
        {
            _transferRepository = transferRepository;
            _farePlanRepository = farePlanRepository;
            _accountRepository = accountRepository;
        }

        public async Task<TransferResponse> Handle(TransferRequest request, CancellationToken cancellationToken)
        {
            TransferResponse response = new TransferResponse() { Status = false };

            Transfer transfer = new Transfer()
            {
                Value = request.Value,
                From = Mapper.Map(request.From, new CustomerAccount()),
                To = Mapper.Map(request.To, new CustomerAccount()),
            };

            IValidationScope<Transfer> validationScope = new TransferValidationScope(_accountRepository)
            {
                Entity = transfer
            };

            await validationScope.Validate();

            if (validationScope.Validation.IsValid)
            {
                await MakeTransfer(transfer);
                response.Status = true;
                response.Message = "Transferencia Realizada";
            }
            else
            {
                response.Message = validationScope.Validation.Message;
            }

            return response;
        }

        private async Task MakeTransfer(Transfer transfer)
        {
            transfer.Tax = await GetTransferTax(transfer.From);
            await _transferRepository.Add(transfer);
            transfer.MakeTransfer();
            await UpdateAccountsAfterTransfer(transfer);
        }

        private async Task<decimal> GetTransferTax(CustomerAccount originAccount)
        {
            FarePlan farePlanOriginAccount = await _farePlanRepository.Find(originAccount.FarePlanId);
            int amountTransfersFromCurrentMonth = await _transferRepository.CountAmountTransferInCurrentMonth(originAccount);
            return (amountTransfersFromCurrentMonth < farePlanOriginAccount.FreeTransfersQuantity) ? 0 : Tax.TransferTax;
        }

        private async Task UpdateAccountsAfterTransfer(Transfer transfer)
        {
            await _accountRepository.Update(transfer.From);
            await _accountRepository.Update(transfer.To);
        }
    }
}
