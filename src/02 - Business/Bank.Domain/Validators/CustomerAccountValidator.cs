using Bank.Business.Domain.Entities;
using FluentValidation;

namespace Bank.Business.Domain.Validators
{
    public class CustomerAccountValidator : AbstractValidator<CustomerAccount>
    {
        public CustomerAccountValidator()
        {
            RuleFor(account => account.BankBranch).NotNull().NotEmpty().WithMessage("Agencia invalida");
            RuleFor(account => account.BankAccount).NotNull().NotEmpty().WithMessage("Conta invalida");
        }
    }
}
