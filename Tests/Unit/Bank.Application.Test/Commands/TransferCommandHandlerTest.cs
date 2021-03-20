using AutoMapper;
using Bank.Business.Application.Commands.Handlers;
using Bank.Business.Application.Commands.Requests;
using Bank.Business.Application.Dto;
using Bank.Business.Domain.Contracts;
using Bank.Business.Domain.Entities;
using Bank.Infrastructure.Bootstrap.Mapper;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Application.Test.Commands
{
    [TestClass]
    public class TransferCommandHandlerTest
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private readonly TransferCommandHandler _sut;

        public TransferCommandHandlerTest()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<BankMappingProfile>();
            });
            _mapper = config.CreateMapper();

            _mediator = Substitute.For<IMediator>();
            _unitOfWork = Substitute.For<IUnitOfWork>();

            _sut = new TransferCommandHandler(_mapper, _mediator, _unitOfWork);
        }

        private CustomerAccountDto GetOriginAccount()
            => GetCustomerAccountDto(1);

        private CustomerAccountDto GetDestinationAccount()
            => GetCustomerAccountDto(2);

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

        private TransferDto BuildTransferDto(int transferValue, CustomerAccountDto originAccountDto, CustomerAccountDto destinationAccountDto)
            => new TransferDto()
            {
                Value = transferValue,
                From = originAccountDto,
                To = destinationAccountDto,
            };

        [TestMethod]
        public async Task TransferBetweenAccounts_ShouldBeSuccess()
        {
            // Arrange
            var originAccountDto = GetOriginAccount();
            var transferDto = BuildTransferDto(5, originAccountDto, GetDestinationAccount());

            var request = new TransferCommandRequest(transferDto, 0);

            _unitOfWork.CustomerAccount.Find(Arg.Any<int>(), Arg.Any<int>()).Returns(_mapper.Map<CustomerAccount>(originAccountDto));
            _unitOfWork.Commit().Returns(true);

            // Action
            var transfer = await _sut.Handle(request, new CancellationToken()).ConfigureAwait(false);

            // Assert
            Assert.IsTrue(transfer.IsSuccess);
        }

        [TestMethod]
        public async Task TransferBetweenAccounts_ErrorsOccursInCommit_ShouldBeFalse()
        {
            // Arrange
            var originAccountDto = GetOriginAccount();
            var transferDto = BuildTransferDto(5, originAccountDto, GetDestinationAccount());

            var request = new TransferCommandRequest(transferDto, 0);

            _unitOfWork.CustomerAccount.Find(Arg.Any<int>(), Arg.Any<int>()).Returns(_mapper.Map<CustomerAccount>(originAccountDto));
            _unitOfWork.Commit().Returns(false);

            // Action
            var transfer = await _sut.Handle(request, new CancellationToken()).ConfigureAwait(false);

            // Assert
            Assert.IsFalse(transfer.IsSuccess);
        }

        [TestMethod]
        public async Task TransferBetweenAccounts_TheTwoAccountsAreEquals_ShouldBeFalse()
        {
            // Arrange
            var originAccountDto = GetOriginAccount();
            var transferDto = BuildTransferDto(5, originAccountDto, originAccountDto);

            var request = new TransferCommandRequest(transferDto, 0);

            // Action
            var transfer = await _sut.Handle(request, new CancellationToken()).ConfigureAwait(false);

            // Assert
            Assert.IsFalse(transfer.IsSuccess);
        }

        [TestMethod]
        public async Task TransferBetweenAccounts_TheBalanceIsInsufficient_ShouldBeFalse()
        {
            // Arrange
            var originAccountDto = GetOriginAccount();
            var transferDto = BuildTransferDto(40, originAccountDto, GetDestinationAccount());

            var request = new TransferCommandRequest(transferDto, 0);

            _unitOfWork.CustomerAccount.Find(Arg.Any<int>(), Arg.Any<int>()).Returns(_mapper.Map<CustomerAccount>(originAccountDto));

            // Action
            var transfer = await _sut.Handle(request, new CancellationToken()).ConfigureAwait(false);

            // Assert
            Assert.IsFalse(transfer.IsSuccess);
        }
    }
}
