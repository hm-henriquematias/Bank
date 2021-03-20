using Bank.Business.Application.Queries.Queries;
using Bank.Business.Domain.Contracts;
using Bank.Business.Domain.Utils;
using MediatR;
using OperationResult;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Business.Application.Queries.Handlers
{
    public class FarePlanTaxQueryHandler : IRequestHandler<FarePlanTransferTaxQuery, Result<decimal>>
    {
        private readonly IFarePlanRepository _farePlanRepository;

        public FarePlanTaxQueryHandler(IFarePlanRepository farePlanRepository)
            => _farePlanRepository = farePlanRepository;

        public async Task<Result<decimal>> Handle(FarePlanTransferTaxQuery request, CancellationToken cancellationToken)
        {
            var farePlanOriginAccount = await _farePlanRepository.Find(request.CustomerAccountDto.FarePlanId);
            return (request.AmountTransferMonthly < farePlanOriginAccount.FreeTransfersQuantity) ? 0m : Tax.TransferTax;
        }
    }
}
