using System;

namespace Bank.Business.Domain.Entities
{
    public class CustomerAccount
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int BankBranch { get; set; }
        public int BankAccount { get; set; }
        public int AccountTypeId { get; set; }
        public int FarePlanId { get; set; }
        public bool IsActiveAccount { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
