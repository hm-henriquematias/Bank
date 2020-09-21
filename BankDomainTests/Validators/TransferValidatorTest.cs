using Bank.Domain.Entities;
using Bank.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BankDomainTests.Validators
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
