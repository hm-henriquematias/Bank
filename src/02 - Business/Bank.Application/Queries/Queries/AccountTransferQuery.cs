using Bank.Business.Application.Dto;
using MediatR;
using OperationResult;
using System.Collections.Generic;

namespace Bank.Business.Application.Queries.Queries
{
    public class AccountTransferQuery : IRequest<Result<IEnumerable<CustomerAccountDto>>>
    {
        public int BankBranch { get; set; }
        public int BankAccount { get; set; }

        public AccountTransferQuery(int bankBranch, int bankAccount)
            => (BankBranch, BankAccount) = (bankBranch, bankAccount);
    }
}
