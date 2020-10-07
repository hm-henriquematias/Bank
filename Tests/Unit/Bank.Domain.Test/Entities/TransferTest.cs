using Bank.Business.Domain.Entities;
using Xunit;

namespace Bank.Test.Unit.Domain.Test.Entities
{
    public class TransferTest
    {
        [Theory]
        [InlineData(0, 900)]
        [InlineData(10, 890)]
        [InlineData(24.90, 875.10)]
        public void MakeTransfer(decimal tax, decimal expectedBalanceToOriginAccount)
        {
            Transfer transfer = new Transfer()
            {
                Value = 100,
                Tax = tax,
                From = new CustomerAccount()
                {
                    Balance = 1000
                },
                To = new CustomerAccount()
                {
                    Balance = 1000
                },
            };

            transfer.MakeTransfer();

            Assert.Equal(expectedBalanceToOriginAccount, transfer.From.Balance);
            Assert.Equal(1100, transfer.To.Balance);
        }
    }
}
