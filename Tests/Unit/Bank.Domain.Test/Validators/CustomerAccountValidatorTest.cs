using Bank.Business.Domain.Entities;
using Bank.Business.Domain.Validators;
using Xunit;

namespace Bank.Test.Unit.Domain.Test.Validators
{
    public class CustomerAccountValidatorTest
    {
        [Fact]
        public void ValidateBankBranch_BankBranchIsNull()
        {
            CustomerAccountValidator validator = new CustomerAccountValidator()
            {
                Account = new CustomerAccount()
                {
                }
            };

            Assert.False(validator.IsValidBankBranch());
        }

        [Fact]
        public void ValidateBankBranch_BankBranchIsValid()
        {
            CustomerAccountValidator validator = new CustomerAccountValidator()
            {
                Account = new CustomerAccount()
                {
                    BankBranch = 1,
                }
            };

            Assert.True(validator.IsValidBankBranch());
        }

        [Fact]
        public void ValidateBankBranch_BankAccountIsNull()
        {
            CustomerAccountValidator validator = new CustomerAccountValidator()
            {
                Account = new CustomerAccount()
                {
                }
            };

            Assert.False(validator.IsValidBankAccount());
        }

        [Fact]
        public void ValidateBankAccount_BankAccountIsValid()
        {
            CustomerAccountValidator validator = new CustomerAccountValidator()
            {
                Account = new CustomerAccount()
                {
                    BankAccount = 1,
                }
            };

            Assert.True(validator.IsValidBankAccount());
        }

        [Fact]
        public void ValidateBankBranch_InactiveAccount()
        {
            CustomerAccountValidator validator = new CustomerAccountValidator()
            {
                Account = new CustomerAccount()
                {
                    IsActiveAccount = false,
                }
            };

            validator.ValidateActiveAccount();

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void ValidateBankBranch_ActiveAccount()
        {
            CustomerAccountValidator validator = new CustomerAccountValidator()
            {
                Account = new CustomerAccount()
                {
                    IsActiveAccount = true,
                }
            };

            validator.ValidateActiveAccount();

            Assert.True(validator.IsValid);
        }
    }
}
