using Bank.Business.Application.Dto;
using MediatR;
using OperationResult;

namespace Bank.Business.Application.Queries.Queries
{
    public class FarePlanTransferTaxQuery : IRequest<Result<decimal>>
    {
        public CustomerAccountDto CustomerAccountDto { get; set; }
        public int AmountTransferMonthly { get; set; }

        public FarePlanTransferTaxQuery(CustomerAccountDto customerAccountDto, int amountTransferMonthly)
            => (CustomerAccountDto, AmountTransferMonthly) = (customerAccountDto, amountTransferMonthly);
    }
}
