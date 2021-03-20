using Bank.Business.Application.Dto;
using Bank.Business.Application.Queries.Handlers;
using Bank.Business.Application.Queries.Queries;
using Bank.Business.Domain.Contracts;
using Bank.Business.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Application.Test.Queries
{
    [TestClass]
    public class FarePlanTaxQueryHandlerTest
    {
        #region Fields
        private readonly IFarePlanRepository _farePlanRepository;
        private readonly FarePlanTaxQueryHandler _sut;
        #endregion

        #region ctor
        public FarePlanTaxQueryHandlerTest()
        {
            _farePlanRepository = Substitute.For<IFarePlanRepository>();
            _sut = new FarePlanTaxQueryHandler(_farePlanRepository);
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

        private FarePlan GetFarePlanEssentialsServiceToTest(int freeTransfersQuantity)
            => new FarePlan()
            {
                FreeTransfersQuantity = freeTransfersQuantity,
            };
        #endregion

        #region Tests
        [TestMethod]
        public async Task FindAccountByBranchAndAccountNumber_AvailableFreeTransfer_ShouldBeSuccess()
        {
            // Arrange
            var bankAccount = 1;
            var amountTransferMonthly = 0;
            var originAccountDto = GetCustomerAccountDto(bankAccount);

            var request = new FarePlanTransferTaxQuery(originAccountDto, amountTransferMonthly);

            _farePlanRepository.Find(Arg.Any<int>()).Returns(GetFarePlanEssentialsServiceToTest(1));

            // Action
            var transfer = await _sut.Handle(request, new CancellationToken()).ConfigureAwait(false);

            // Assert
            Assert.IsTrue(transfer.IsSuccess);
            Assert.AreEqual(0m, transfer.Value);
        }

        [TestMethod]
        public async Task FindAccountByBranchAndAccountNumber_NotAvailableFreeTransfer_ShouldBeSuccess()
        {
            // Arrange
            var bankAccount = 1;
            var amountTransferMonthly = 0;
            var originAccountDto = GetCustomerAccountDto(bankAccount);

            var request = new FarePlanTransferTaxQuery(originAccountDto, amountTransferMonthly);

            _farePlanRepository.Find(Arg.Any<int>()).Returns(GetFarePlanEssentialsServiceToTest(2));

            // Action
            var transfer = await _sut.Handle(request, new CancellationToken()).ConfigureAwait(false);

            // Assert
            Assert.IsTrue(transfer.IsSuccess);
            Assert.AreEqual(0m, transfer.Value);
        }
        #endregion
    }
}
