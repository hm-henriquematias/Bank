using CQRSHelper._Common;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace Bank.Business.Application.ValidationScope
{
    public interface IValidationScope<TEntity> where TEntity: class
    {
        public ValidationScope Validation { get; set; }
        public TEntity Entity { get; set; }

        Task Validate();
        ValidationResult ValidateCommand();
        Task<Response>   ValidateInPersistenceLayer();
        ValidationResult ValidateInDomain();
    }
}
