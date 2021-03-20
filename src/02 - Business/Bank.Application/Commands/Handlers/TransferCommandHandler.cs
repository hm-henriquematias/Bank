using AutoMapper;
using Bank.Business.Application.Commands.Requests;
using Bank.Business.Domain.Contracts;
using Bank.Business.Domain.Entities;
using MediatR;
using OperationResult;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Business.Application.Commands.Handlers
{
    public class TransferCommandHandler : IRequestHandler<TransferCommandRequest, Result<string>>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public TransferCommandHandler(IMapper mapper, IMediator mediator, IUnitOfWork unitOfWork)
            => (_mapper, _mediator, _unitOfWork) = (mapper, mediator, unitOfWork);

        public async Task<Result<string>> Handle(TransferCommandRequest request, CancellationToken cancellationToken)
        {
            var transfer = new Transfer()
            {
                Value = request.TransferDto.Value,
                From = _mapper.Map<CustomerAccount>(request.TransferDto.From),
                To = _mapper.Map<CustomerAccount>(request.TransferDto.To),
                Tax = request.TransferTax,
            };

            Result<string> result;
            if (!transfer.IsOriginAccountEqualsDestinationAccount())
            {
                result = await ValidateOriginAccountBalance(transfer).ConfigureAwait(false)
                    ? await MakeTransfer(transfer).ConfigureAwait(false)
                    : Result.Error<string>(new Exception("Conta de origem nao possui saldo suficiente para operacao"));
            }
            else
            {
                result = Result.Error<string>(new Exception("Contas nao podem ser iguais"));
            }

            return result;
        }

        private async Task<bool> ValidateOriginAccountBalance(Transfer transfer)
        {
            var originAccount = await _unitOfWork.CustomerAccount.Find(transfer.From.BankBranch, transfer.From.BankAccount).ConfigureAwait(false);
            return originAccount.Balance >= transfer.GetTotalValueOfTransferToDecreaseForOriginAccount();
        }

        private async Task<Result<string>> MakeTransfer(Transfer transfer)
        {
            await _unitOfWork.Transfer.Add(transfer).ConfigureAwait(false);

            transfer.MakeTransfer();

            await _unitOfWork.CustomerAccount.Update(transfer.From).ConfigureAwait(false);
            await _unitOfWork.CustomerAccount.Update(transfer.To).ConfigureAwait(false);

            return await _unitOfWork.Commit().ConfigureAwait(false)
                ? Result.Success("Transferencia Realizada")
                : Result.Error<string>(new Exception("Transferencia nao Realizada"));
        }
    }
}
