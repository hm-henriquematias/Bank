using AutoMapper;
using Bank.Business.Application.Queries.Handlers;
using Bank.Business.Application.Queries.Queries;
using Bank.Business.Domain.Contracts;
using Bank.Business.Domain.Entities;
using Bank.Infrastructure.Bootstrap.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Application.Test.Queries
{
    [TestClass]
    public class TransferQueryHandlerTest
    {
        private readonly ITransferRepository _transferRepository;
        private readonly ICustomerAccountRepository _customerAccountRepository;
        private readonly IMapper _mapper;

        private readonly TransferQueryHandler _sut;

        public TransferQueryHandlerTest()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<BankMappingProfile>();
            });

            _transferRepository = Substitute.For<ITransferRepository>();
            _customerAccountRepository = Substitute.For<ICustomerAccountRepository>();
            _mapper = config.CreateMapper();

            _sut = new TransferQueryHandler(_transferRepository, _customerAccountRepository, _mapper);
        }

        [TestMethod]
        public async Task FindAccountTransfers_ShouldBeSuccess()
        {
            // Arrange
            var bankBranch = 1;
            var bankAccount = 1;

            var request = new AccountTransferQuery(bankBranch, bankAccount);

            _customerAccountRepository.Find(Arg.Any<int>(), Arg.Any<int>()).Returns(new CustomerAccount());
            _transferRepository.FindAllOriginAccount(Arg.Any<CustomerAccount>()).Returns(new List<Transfer>());

            // Action
            var transfer = await _sut.Handle(request, new CancellationToken()).ConfigureAwait(false);

            // Assert
            Assert.IsTrue(transfer.IsSuccess);
        }
    }
}
