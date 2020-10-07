using Bank.Business.Application.Commands.Responses;
using CQRSHelper._Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Bank.Business.Application.Queries.Queries
{
    public class AccountTransferQuery : IRequest<AccountResponse>
    {
        [Required]
        public int Branch { get; set; }

        [Required]
        public int Account { get; set; }
    }
}
