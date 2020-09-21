using Bank.Business.Domain.Enums;

namespace Bank.Business.Domain.ValueObjects
{
    public class AccountType
    {
        public int AccountTypeId { get; set; }
        public string AccountTypeDescription { get; set; }
        
        public bool IsCorrente()
        {
            return AccountTypeId.Equals((int) AccountTypeEnum.Corrente);
        }

        public bool IsPoupanca()
        {
            return AccountTypeId.Equals((int) AccountTypeEnum.Poupanca);
        }
    }
}
