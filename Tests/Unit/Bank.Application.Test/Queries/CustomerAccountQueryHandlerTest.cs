using AutoMapper;
using Bank.Business.Application.Dto;
using Bank.Business.Application.Queries.Handlers;
using Bank.Business.Application.Queries.Queries;
using Bank.Business.Domain.Contracts;
using Bank.Business.Domain.Entities;
using Bank.Infrastructure.Bootstrap.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Application.Test.Queries
{
    [TestClass]
    public class CustomerAccountQueryHandlerTest
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ICustomerAccountRepository _customerAccountRepository;

        private readonly CustomerAccountQueryHandler _sut;
        #endregion

        #region ctor
        public CustomerAccountQueryHandlerTest()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<BankMappingProfile>();
            });
            _mapper = config.CreateMapper();

            _customerAccountRepository = Substitute.For<ICustomerAccountRepository>();

            _sut = new CustomerAccountQueryHandler(_customerAccountRepository, _mapper);
        }
        #endregion

        #region PrivateMethods
        private CustomerAccountDto GetCustomerAccountDto(int bankAccountNumber)
            => new CustomerAccountDto()
            {
                CustomerId = 1,
                BankBranch = 1,
                BankAccount = bankAccountNumber,
                AccountTypeId = 1,
                FarePlanId = 2,
                IsActiveAccount = true,
                Balance = 20,
            };
        #endregion

        #region Tests                                                             
        [TestMethod]
        public async Task FindAccountByBranchAndAccountNumber_ShouldBeSuccess()
        {
            // Arrange
            var bankBranch = 1;
            var bankAccount = 1;
            var originAccountDto = GetCustomerAccountDto(bankAccount);

            var request = new CustomerAccountQuery(bankBranch, bankAccount);

            _customerAccountRepository.Find(Arg.Any<int>(), Arg.Any<int>()).Returns(_mapper.Map<CustomerAccount>(originAccountDto));

            // Action
            var transfer = await _sut.Handle(request, new CancellationToken()).ConfigureAwait(false);

            // Assert
            Assert.IsTrue(transfer.IsSuccess);
        }
        #endregion
    }
}
