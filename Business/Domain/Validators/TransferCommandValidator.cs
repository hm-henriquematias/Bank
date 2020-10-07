using Bank.Business.Domain.Entities;
using FluentValidation;

namespace Bank.Business.Domain.Validators
{
    public class TransferCommandValidator : AbstractValidator<Transfer>
    {
        public TransferCommandValidator()
        {
            RuleFor(transfer => transfer.Value).GreaterThan(0).WithMessage("Valor deve ser maior que zero");
            RuleFor(transfer => transfer.From).SetValidator(new CustomerAccountCommandValidator());
            RuleFor(transfer => transfer.To).SetValidator(new CustomerAccountCommandValidator());
        }
    }
}
