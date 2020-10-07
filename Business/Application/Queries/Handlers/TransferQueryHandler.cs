using Bank.Business.Application.Commands.Responses;
using Bank.Business.Application.Contracts;
using Bank.Business.Application.Queries.Queries;
using Bank.Business.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using MediatR;

namespace Bank.Business.Application.Queries.Handlers
{
    public class TransferQueryHandler : IRequestHandler<AccountTransferQuery, AccountResponse>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly ICustomerAccountRepository _customerAccountRepository;

        public TransferQueryHandler(ICustomerAccountRepository customerAccountRepository, ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
            _customerAccountRepository = customerAccountRepository;
        }

        public async Task<AccountResponse> Handle(AccountTransferQuery accountQuery, CancellationToken cancellationToken)
        {
            AccountResponse accountResponse = new AccountResponse() { Status = false };
            CustomerAccount customerAccount = await _customerAccountRepository.Find(accountQuery.Branch, accountQuery.Account);

            if (customerAccount != null)
            {
                IEnumerable<Transfer> transfers = await _transferRepository.FindAllOriginAccount(customerAccount);

                accountResponse.Message = JsonSerializer.Serialize(transfers);
                accountResponse.Status = true;
            } 
            else
            {
                accountResponse.Error("Conta invalida");
            }

            return accountResponse;
        }
    }
}
