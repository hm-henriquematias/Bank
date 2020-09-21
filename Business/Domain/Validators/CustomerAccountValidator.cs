using Bank.Business.Application.Entities;

namespace Bank.Business.Domain.Validators
{
    public class CustomerAccountValidator : EntityValidator
    {
        public CustomerAccount Account { get; set; }

        public override void Validate()
        {
            if (!IsValidBankBranch())
                AddValidateMessage("Agencia inválida");
            if (!IsValidBankAccount())
                AddValidateMessage("Conta inválida");
        }

        public  void ValidateActiveAccount()
        {
            Validate();

            if (!Account.IsActiveAccount)
                AddValidateMessage("Conta inativa");
        }

        public bool IsValidBankBranch()
        {
            return StringValidator.ValidateString(Account.BankBranch.ToString());
        }

        public bool IsValidBankAccount()
        {
            return StringValidator.ValidateString(Account.BankAccount.ToString());
        }
    }
}
