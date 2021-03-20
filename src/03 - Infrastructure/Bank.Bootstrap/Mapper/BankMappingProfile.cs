using AutoMapper;
using Bank.Business.Application.Dto;
using Bank.Business.Domain.Entities;

namespace Bank.Infrastructure.Bootstrap.Mapper
{
    public class BankMappingProfile : Profile
    {
        public BankMappingProfile()
        {
            CreateMap<CustomerAccount, CustomerAccountDto>().ReverseMap();
            CreateMap<Transfer, TransferDto>().ReverseMap();
        }
    }
}
