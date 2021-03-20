using AutoMapper;
using Bank.Business.Application.Dto;
using Bank.Business.Application.Queries.Queries;
using Bank.Business.Domain.Contracts;
using Bank.Business.Domain.Entities;
using MediatR;
using OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Business.Application.Queries.Handlers
{
    public class TransferQueryHandler :
        IRequestHandler<AccountTransferQuery, Result<IEnumerable<CustomerAccountDto>>>,
        IRequestHandler<AmountAccountMonthlyTransferQuery, Result<int>>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly ICustomerAccountRepository _customerAccountRepository;
        private readonly IMapper _mapper;

        public TransferQueryHandler(ITransferRepository transferRepository,
                                    ICustomerAccountRepository customerAccountRepository,
                                    IMapper mapper)
            => (_transferRepository, _customerAccountRepository, _mapper) = (transferRepository, customerAccountRepository, mapper);

        public async Task<Result<IEnumerable<CustomerAccountDto>>> Handle(AccountTransferQuery accountQuery, CancellationToken cancellationToken)
        {
            var customerAccount = await _customerAccountRepository.Find(accountQuery.BankBranch, accountQuery.BankAccount).ConfigureAwait(false);

            return customerAccount == null
                    ? Result.Error<IEnumerable<CustomerAccountDto>>(new Exception("Conta invalida"))
                    : Result.Success(await GetAllTransferToAccount(_mapper.Map<CustomerAccount>(customerAccount)).ConfigureAwait(false));
        }

        public Task<Result<int>> Handle(AmountAccountMonthlyTransferQuery request, CancellationToken cancellationToken)
        {
            return Result.Success<int>(1).AsTask;
        }

        private async Task<IEnumerable<CustomerAccountDto>> GetAllTransferToAccount(CustomerAccount customerAccount)
        {
            var accountTransfers = await _transferRepository.FindAllOriginAccount(customerAccount).ConfigureAwait(false);
            return accountTransfers.Select(accountTransfer => _mapper.Map<CustomerAccountDto>(accountTransfer));
        }
    }
}
