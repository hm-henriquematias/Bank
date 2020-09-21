using Bank.Domain.Contracts;
using Bank.Domain.Entities;
using Bank.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Infrastructure.Repositories
{
    public class TransferRepository : BaseRepository<Transfer>, ITransferRepository
    {
        public TransferRepository(BankContext bankContext) : base(bankContext)
        {
        }

        public Transfer Find(CustomerAccount from, CustomerAccount to)
        {
            return BankContext.Transfers.FirstOrDefault(transfer => transfer.From == from && transfer.To == to);
        }

        public IEnumerable<Transfer> FindAll(CustomerAccount customerAccount)
        {
            return BankContext.Transfers.Where(transfer => transfer.From == customerAccount || transfer.To == customerAccount);
        }

        public IEnumerable<Transfer> FindAllDestinationAccount(CustomerAccount to)
        {
            return BankContext.Transfers.Where(transfer => transfer.To == to);
        }

        public IEnumerable<Transfer> FindAllOriginAccount(CustomerAccount from)
        {
            return BankContext.Transfers.Where(transfer => transfer.From == from);
        }

        public Transfer FindDestinationAccount(CustomerAccount to)
        {
            return BankContext.Transfers.FirstOrDefault(transfer => transfer.To == to);
        }

        public Transfer FindOriginAccount(CustomerAccount from)
        {
            return BankContext.Transfers.FirstOrDefault(transfer => transfer.From == from);
        }

        public int CountAmountTransferInCurrentMonth(CustomerAccount account)
        {
            return BankContext.CustomerAccounts.Count(
                 transfer =>
                    transfer.BankBranch == account.BankBranch &&
                    transfer.BankAccount == account.BankAccount &&
                    transfer.CreatedDate.Month == DateTime.Now.Month
                );
        }
    }
}
