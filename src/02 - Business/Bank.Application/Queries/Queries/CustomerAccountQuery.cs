using Bank.Business.Application.Dto;
using MediatR;
using OperationResult;

namespace Bank.Business.Application.Queries.Queries
{
    public class CustomerAccountQuery : IRequest<Result<CustomerAccountDto>>
    {
        public int BankBranch { get; set; }
        public int BankAccount { get; set; }

        public CustomerAccountQuery(int bankBranch, int bankAccount)
            => (BankBranch, BankAccount) = (bankBranch, bankAccount);
    }
}
