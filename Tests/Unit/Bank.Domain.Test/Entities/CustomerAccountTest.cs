using Bank.Business.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bank.Domain.Test.Entities
{
    [TestClass]
    public class CustomerAccountTest
    {
        private CustomerAccount GetCustomerAccount()
            => new CustomerAccount() { Balance = 1000 };

        [TestMethod]
        public void IncreaseValueInBalance_ShouldBeEqual()
        {
            // Arrange
            var customerAccount = GetCustomerAccount();

            // Action
            customerAccount.IncreaseValueInBalance(10m);

            // Assert
            Assert.AreEqual(1010m, customerAccount.Balance);
        }

        [TestMethod]
        public void DecreaseValueInBalance_ShouldBeEqual()
        {
            // Arrange
            var customerAccount = GetCustomerAccount();

            // Action
            customerAccount.DecreaseValueInBalance(10m);

            // Assert
            Assert.AreEqual(990m, customerAccount.Balance);
        }
    }
}