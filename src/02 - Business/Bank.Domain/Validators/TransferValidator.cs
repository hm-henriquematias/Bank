using Bank.Business.Domain.Entities;
using FluentValidation;

namespace Bank.Business.Domain.Validators
{
    public class TransferValidator : AbstractValidator<Transfer>
    {
        public TransferValidator()
        {
            RuleFor(transfer => transfer.Value).GreaterThan(0).WithMessage("Valor deve ser maior que zero");

            RuleFor(transfer => transfer.From).SetValidator(new CustomerAccountValidator());
            RuleFor(transfer => transfer.To).SetValidator(new CustomerAccountValidator());

            RuleFor(transfer => transfer.From.IsActiveAccount).Equal(true).WithMessage("Conta de origem inativa");
            RuleFor(transfer => transfer.To.IsActiveAccount).Equal(true).WithMessage("Conta de destino inativa");

            RuleFor(transfer => transfer.From.Balance).GreaterThanOrEqualTo(transfer => transfer.Value).WithMessage("Saldo não disponivel para transferencia");
        }
    }
}
