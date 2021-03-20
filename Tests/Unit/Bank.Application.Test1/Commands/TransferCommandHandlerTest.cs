using Bank.Business.Application.Commands.Handlers;
using Bank.Business.Application.Commands.Requests;
using Bank.Business.Application.Dto;
using Bank.Business.Domain.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Bank.Test.Unit.Application.Test.Commands
{
    public class TransferCommandHandlerTest
    {
        //DbContextOptions<BankContext> options;
        //var builder = new DbContextOptionsBuilder<BankContext>();
        //builder.UseInMemoryDatabase("BankTest");
        //    options = builder.Options;
        //    BankContext bankContext = new BankContext(options);
        //bankContext.Database.EnsureDeleted();
        //    bankContext.Database.EnsureCreated();
        //    return new CustomerAccountRepository(bankContext);
        private readonly IMediator _mediator;
        private readonly TransferCommandHandler _transferCommandHandler;
        //private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TransferCommandHandlerTest(TransferCommandHandler transferCommandHandler)
        {
            _transferCommandHandler = transferCommandHandler;
        }

        [Fact]
        public async Task TransferBetweenAccounts_ShouldBeSuccess()
        {
            var originAccount = new CustomerAccountDto()
            {
                CustomerId = 1,
                BankBranch = 1,
                BankAccount = 1,
                AccountTypeId = 1,
                FarePlanId = 2,
                IsActiveAccount = true,
                Balance = 20,
            };

            var destinationAccount = new CustomerAccountDto()
            {
                CustomerId = 2,
                BankBranch = 1,
                BankAccount = 2,
                AccountTypeId = 1,
                FarePlanId = 2,
                IsActiveAccount = true,
                Balance = 20,
            };

            var transferDto = new TransferDto()
            {
                Value = 5,
                From = originAccount,
                To = destinationAccount,
            };

            var transfer = await _transferCommandHandler.Handle(new TransferCommandRequest(transferDto), new CancellationToken()).ConfigureAwait(false);

            Assert.True(transfer.IsSuccess);
        }
    }
}
