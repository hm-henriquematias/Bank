using Bank.Infrastructure.ServicesInterfaces;
using Bank.Domain.Entities;
using Bank.Domain.Contracts;

namespace Bank.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly ICustomerAccountRepository _customerAccountRepository;

        public AccountService(ICustomerAccountRepository customerAccountRepository)
        {
            _customerAccountRepository = customerAccountRepository;
        }

        public bool IsExistAccount(CustomerAccount account)
        {
            return !(_customerAccountRepository.Find(account.BankBranch, account.BankAccount) == null);
        }

        public void Update(CustomerAccount account)
        {
            _customerAccountRepository.Update(account);
        }
    }
}
