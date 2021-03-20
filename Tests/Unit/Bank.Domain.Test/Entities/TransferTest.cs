using Bank.Business.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bank.Domain.Test.Entities
{
    [TestClass]
    public class TransferTest
    {
        [DataTestMethod]
        [DataRow(0, 900, 1100)]
        [DataRow(10, 890, 1100)]
        [DataRow(24.90, 875.10, 1100)]
        public void MakeTransfer_ShouldBeEqual(double tax, double expectedBalanceToOriginAccount, double expectedBalanceToDestinationAccount)
        {
            // Arrange
            Transfer transfer = new Transfer()
            {
                Value = 100,
                Tax = Convert.ToDecimal(tax),
                From = new CustomerAccount()
                {
                    Balance = 1000
                },
                To = new CustomerAccount()
                {
                    Balance = 1000
                },
            };

            // Action
            transfer.MakeTransfer();

            // Assert
            Assert.AreEqual(Convert.ToDecimal(expectedBalanceToOriginAccount), transfer.From.Balance);
            Assert.AreEqual(Convert.ToDecimal(expectedBalanceToDestinationAccount), transfer.To.Balance);
        }

        [TestMethod]
        public void GetTotalValueOfTransferToDecreaseForOriginAccount_ShouldBeEqual()
        {
            // Assert
            var transfer = new Transfer() { Value = 100, Tax = 10, };

            var expected = 110;

            // Action
            var totalValue = transfer.GetTotalValueOfTransferToDecreaseForOriginAccount();

            // Assert
            Assert.AreEqual(expected, totalValue);
        }

        [TestMethod]
        public void GetTotalValueOfTransferToDecreaseForOriginAccount_ShouldBeTrue()
        {
            // Assert
            var transfer = new Transfer()
            {
                From = new CustomerAccount()
                {
                    BankBranch = 1,
                    BankAccount = 1,
                },
                To = new CustomerAccount()
                {
                    BankBranch = 1,
                    BankAccount = 1,
                },
            };

            // Action
            var isOriginAccountEqualsDestinationAccount = transfer.IsOriginAccountEqualsDestinationAccount();

            // Assert
            Assert.IsTrue(isOriginAccountEqualsDestinationAccount);
        }


        [TestMethod]
        public void GetTotalValueOfTransferToDecreaseForOriginAccount_ShouldBeFalse()
        {
            // Assert
            var transfer = new Transfer()
            {
                From = new CustomerAccount()
                {
                    BankBranch = 1,
                    BankAccount = 1,
                },
                To = new CustomerAccount()
                {
                    BankBranch = 1,
                    BankAccount = 2,
                },
            };

            // Action
            var isOriginAccountEqualsDestinationAccount = transfer.IsOriginAccountEqualsDestinationAccount();

            // Assert
            Assert.IsFalse(isOriginAccountEqualsDestinationAccount);
        }
    }
}