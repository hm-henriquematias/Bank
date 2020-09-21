using Bank.Business.Domain.Entities;
using Bank.Business.Domain.Validators;
using Xunit;

namespace Bank.Test.Unit.Domain.Test.Validators
{
    public class TransferValidatorTest
    {
        [Theory]
        [InlineData(200, true)]
        [InlineData(10, false)]
        public void IsBalanceAvailableToTransfer(decimal balance, bool expected)
        {
            Transfer transfer = new Transfer()
            {
                Value = 100,
                From = new CustomerAccount()
                {
                    BankBranch = 1,
                    BankAccount = 1,
                    Balance = balance
                },
                To = new CustomerAccount()
                {
                    BankBranch = 2,
                    BankAccount = 2,
                    Balance = 0
                }
            };

            TransferValidator validator = new TransferValidator()
            {
                TransferEntity = transfer
            };

            Assert.Equal(expected, validator.IsBalanceAvailableToTransfer());
        }
    }
}
