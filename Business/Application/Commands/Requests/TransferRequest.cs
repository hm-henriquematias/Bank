using Bank.Business.Application.Commands.Responses;
using Bank.Business.Application.Dto;
using MediatR;

namespace Bank.Business.Application.Commands.Requests
{
    public class TransferRequest : IRequest<TransferResponse>
    {
        public decimal Value { get; set; }
        public CustomerAccountDto From { get; set; }
        public CustomerAccountDto To { get; set; }
    }
}
