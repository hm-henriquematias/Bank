using Bank.Business.Domain.Contracts;
using Bank.Business.Domain.Entities;
using Bank.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Infrastructure.Persistence.Repositories
{
    public class TransferRepository : BaseRepository<Transfer>, ITransferRepository
    {
        public TransferRepository(BankContext bankContext) : base(bankContext)
        {
        }

        public async Task<Transfer> Find(CustomerAccount from, CustomerAccount to)
        {
            return await BankContext.Transfers.FirstOrDefaultAsync(transfer => transfer.From == from && transfer.To == to);
        }

        public async Task<IEnumerable<Transfer>> FindAll(CustomerAccount customerAccount)
        {
            List<Transfer> transfers = new List<Transfer>();

            IEnumerable<Transfer> transfersList = await BankContext.Transfers.ToListAsync();

            foreach (Transfer transfer in transfersList)
            {
                if ((transfer.From.BankBranch == customerAccount.BankBranch && transfer.From.BankAccount == customerAccount.BankAccount) ||
                    (transfer.To.BankBranch == customerAccount.BankBranch && transfer.To.BankAccount == customerAccount.BankAccount))
                    transfers.Add(transfer);
            }

            return transfers;
        }

        public async Task<IEnumerable<Transfer>> FindAllDestinationAccount(CustomerAccount to)
        {
            List<Transfer> transfers = new List<Transfer>();

            IEnumerable<Transfer> transfersList = await BankContext.Transfers.ToListAsync();

            foreach (Transfer transfer in transfersList)
            {
                if (transfer.To.BankBranch == to.BankBranch && transfer.To.BankAccount == to.BankAccount)
                    transfers.Add(transfer);
            }

            return transfers;
        }

        public async Task<IEnumerable<Transfer>> FindAllOriginAccount(CustomerAccount from)
        {
            List<Transfer> transfers = new List<Transfer>();

            IEnumerable<Transfer> transfersList = await BankContext.Transfers.ToListAsync();

            foreach (Transfer transfer in transfersList)
            {
                if (transfer.From.BankBranch == from.BankBranch && transfer.From.BankAccount == from.BankAccount)
                    transfers.Add(transfer);
            }

            return transfers;
        }

        public async Task<Transfer> FindDestinationAccount(CustomerAccount to)
        {
            return await BankContext.Transfers.FirstOrDefaultAsync(transfer => transfer.To == to);
        }

        public async Task<Transfer> FindOriginAccount(CustomerAccount from)
        {
            return await BankContext.Transfers.FirstOrDefaultAsync(transfer => transfer.From == from);
        }

        public async Task<int> CountAmountTransferInCurrentMonth(CustomerAccount account)
        {
            List<Transfer> transfers = new List<Transfer>();

            IEnumerable<Transfer> transfersList = await BankContext.Transfers.ToListAsync();

            foreach (Transfer transfer in transfersList)
            {
                if (transfer.From.BankBranch == account.BankBranch && transfer.From.BankAccount == account.BankAccount && transfer.CreatedDate.Month == DateTime.Now.Month)
                    transfers.Add(transfer);
            }

            return transfers.Count();
        }
    }
}
