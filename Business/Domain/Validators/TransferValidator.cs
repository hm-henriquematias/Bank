using Bank.Business.Domain.Entities;
using Bank.Business.Domain.Enums;

namespace Bank.Business.Domain.Validators
{
    public class TransferValidator : EntityValidator
    {
        public Transfer TransferEntity { get; set; }

        public override void Validate()
        {
            if (!IsBalanceAvailableToTransfer())
                AddValidateMessage("Saldo indisponivel para conta de origem");
            ValidateAccounts(TransferenceDirectionEnum.Origem, TransferEntity.From);
            ValidateAccounts(TransferenceDirectionEnum.Destino, TransferEntity.To);
        }

        public void ValidateAccounts(TransferenceDirectionEnum transferenceDirection, CustomerAccount customerAccount)
        {
            CustomerAccountValidator AccountValidator = new CustomerAccountValidator
            {
                Account = customerAccount
            };

            AccountValidator.Validate();

            if (!AccountValidator.IsValid)
                AddValidateMessage($"Erro ao validar conta: {transferenceDirection.ToString()} - {AccountValidator.GetValidateMessage()}");
        }

        public bool IsBalanceAvailableToTransfer()
        {
            return TransferEntity.Value <= TransferEntity.From.Balance;
        }
    }
}
