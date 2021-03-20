using System;

namespace Bank.Business.Domain.Entities
{
    public class Transfer
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public CustomerAccount From { get; set; }
        public CustomerAccount To { get; set; }
        public decimal Tax { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public void MakeTransfer()
        {
            From.DecreaseValueInBalance(GetTotalValueOfTransferToDecreaseForOriginAccount());
            To.IncreaseValueInBalance(Value);
        }

        public decimal GetTotalValueOfTransferToDecreaseForOriginAccount()
            => Tax + Value;

        public bool IsOriginAccountEqualsDestinationAccount()
            => From.BankBranch == To.BankBranch && From.BankAccount == To.BankAccount;
    }
}
