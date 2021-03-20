namespace Bank.Business.Application.Dto
{
    public class CustomerAccountDto
    {
        public int CustomerId { get; set; }
        public int BankBranch { get; set; }
        public int BankAccount { get; set; }
        public int AccountTypeId { get; set; }
        public int FarePlanId { get; set; }
        public bool IsActiveAccount { get; set; }
        public decimal Balance { get; set; }
    }
}
