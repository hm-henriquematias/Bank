using Bank.Domain.Dto;
using Bank.Domain.Entities;
using Bank.Domain.Mappers;
using Bank.Infrastructure.ServicesInterfaces;

namespace BankWeb.Services
{
    public class TransferApplicationService
    {
        private readonly ITransferService _transferService;

        public TransferApplicationService(ITransferService transferService)
        {
            _transferService = transferService;
        }

        public void Transfer(TransferDto transferDto, CustomerAccountDto originAccountDto, CustomerAccountDto destinationAccountDto)
        {
            Transfer transfer = DtoToEntity(transferDto, originAccountDto, destinationAccountDto);
            _transferService.Transfer(transfer);
        }

        public Transfer DtoToEntity(TransferDto transferDto, CustomerAccountDto originAccountDto, CustomerAccountDto destinationAccountDto)
        {
            Transfer transfer = Mapper.Map(transferDto, new Transfer());
            transfer.From = Mapper.Map(originAccountDto, new CustomerAccount());
            transfer.To = Mapper.Map(destinationAccountDto, new CustomerAccount());
            
            return transfer;
        }
    }
}
