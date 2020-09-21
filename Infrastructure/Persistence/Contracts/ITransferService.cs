using Bank.Business.Domain.Entities;

namespace Bank.Infrastructure.Persistence.Contracts
{
    public interface ITransferService
    {
        void Transfer(Transfer transfer);
    }
}
