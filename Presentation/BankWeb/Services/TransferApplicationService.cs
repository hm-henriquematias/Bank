using Bank.Business.Application.Dto;
using Bank.Business.Domain.Entities;
using Bank.Business.Domain.Mappers;
using Bank.Infrastructure.Persistence.Contracts;

namespace Bank.Presentation.BankWeb.Services
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
