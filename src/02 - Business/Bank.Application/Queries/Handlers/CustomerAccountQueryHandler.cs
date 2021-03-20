using AutoMapper;
using Bank.Business.Application.Dto;
using Bank.Business.Application.Queries.Queries;
using Bank.Business.Domain.Contracts;
using MediatR;
using OperationResult;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Business.Application.Queries.Handlers
{
    public class CustomerAccountQueryHandler : IRequestHandler<CustomerAccountQuery, Result<CustomerAccountDto>>
    {
        private readonly ICustomerAccountRepository _customerAccountRepository;
        private readonly IMapper _mapper;

        public CustomerAccountQueryHandler(ICustomerAccountRepository customerAccountRepository, IMapper mapper)
            => (_customerAccountRepository, _mapper) = (customerAccountRepository, mapper);

        public async Task<Result<CustomerAccountDto>> Handle(CustomerAccountQuery accountQuery, CancellationToken cancellationToken)
            => _mapper.Map<CustomerAccountDto>(await _customerAccountRepository.Find(accountQuery.BankBranch, accountQuery.BankAccount)
                                                                               .ConfigureAwait(false));
    }
}
