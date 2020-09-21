using Bank.Domain.Entities;

namespace Bank.Infrastructure.ServicesInterfaces
{
    public interface ITransferService
    {
        void Transfer(Transfer transfer);
    }
}
