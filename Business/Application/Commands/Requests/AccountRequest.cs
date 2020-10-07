using Bank.Business.Application.Commands.Responses;
using Bank.Business.Application.Dto;
using MediatR;

namespace Bank.Business.Application.Commands.Requests
{
    public class AccountRequest : IRequest<AccountResponse>
    {
        public int Branch { get; set; }
        public int Account { get; set; }
    }
}
