using Bank.Business.Application.Dto;
using MediatR;
using OperationResult;

namespace Bank.Business.Application.Queries.Queries
{
    public class AmountAccountMonthlyTransferQuery : IRequest<Result<int>>
    {
        public CustomerAccountDto CustomerAccountDto { get; set; }

        public AmountAccountMonthlyTransferQuery(CustomerAccountDto customerAccountDto)
            => CustomerAccountDto = customerAccountDto;
    }
}
