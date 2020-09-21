using Bank.Business.Domain.Entities;

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
            return Account.BankBranch != null && Account.BankBranch != 0;
        }

        public bool IsValidBankAccount()
        {
            return Account.BankAccount != null && Account.BankAccount != 0;
        }
    }
}
