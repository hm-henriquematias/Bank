using Bank.Business.Application.Dto;
using MediatR;
using OperationResult;

namespace Bank.Business.Application.Commands.Requests
{
    public class TransferCommandRequest : IRequest<Result<string>>
    {
        public TransferDto TransferDto { get; set; }
        public decimal TransferTax { get; set; }

        public TransferCommandRequest(TransferDto transferDto, decimal transferTax)
            => (TransferDto, TransferTax) = (transferDto, transferTax);
    }
}
