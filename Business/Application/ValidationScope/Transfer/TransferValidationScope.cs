using Bank.Business.Application.Contracts;
using Bank.Business.Domain.Validators;
using System.Threading.Tasks;
using FluentValidation.Results;
using System.Linq;
using CQRSHelper._Common;

namespace Bank.Business.Application.ValidationScope.Transfer
{
    public class TransferValidationScope : IValidationScope<Domain.Entities.Transfer>
    {
        public ValidationScope Validation { get; set; }
        public Domain.Entities.Transfer Entity { get; set; }

        private readonly ICustomerAccountRepository _accountRepository;

        public TransferValidationScope(ICustomerAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task Validate()
        {
            Validation = new ValidationScope() { IsValid = false };

            ValidationResult commandValidation = ValidateCommand();

            if (Entity.IsOriginAccountEqualsDestinationAccount())
            {
                Validation.Message = "Operação inválida";
            }
            else
            {
                if (commandValidation.IsValid)
                {
                    Response databaseValidation = await ValidateInPersistenceLayer();

                    if (databaseValidation.Status)
                    {
                        Entity.From = await _accountRepository.Find(Entity.From.BankBranch, Entity.From.BankAccount);
                        Entity.To = await _accountRepository.Find(Entity.To.BankBranch, Entity.To.BankAccount);

                        ValidationResult domainValidation = ValidateInDomain();

                        if (domainValidation.IsValid)
                        {
                            Validation.IsValid = true;
                        }
                        else
                        {
                            Validation.Message = domainValidation.Errors.First().ErrorMessage;
                        }
                    }
                    else
                    {
                        Validation.Message = databaseValidation.Message;
                    }
                }
                else
                {
                    Validation.Message = commandValidation.Errors.First().ErrorMessage;
                }
            }
        }

        public ValidationResult ValidateCommand()
        {
            return new TransferCommandValidator().Validate(Entity);
        }

        public async Task<Response> ValidateInPersistenceLayer()
        {
            var transferResponse = new Response() { Status = true };

            bool existsOriginAccount = await _accountRepository.IsExistsAccount(Entity.From);
            bool existsDestinationAccount = await _accountRepository.IsExistsAccount(Entity.To);

            if (!existsOriginAccount)
            {
                transferResponse.Status = false;
                transferResponse.Message = "Conta de origem não existe";
            }
            if (!existsDestinationAccount)
            {
                transferResponse.Status = false;
                transferResponse.Message = "Conta de destino não existe";
            }

            return transferResponse;
        }

        public ValidationResult ValidateInDomain()
        {
            return new TransferValidator().Validate(Entity);
        }
    }
}
