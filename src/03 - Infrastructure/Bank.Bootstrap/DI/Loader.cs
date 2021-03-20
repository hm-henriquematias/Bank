using Bank.Business.Application.Commands.Handlers;
using Bank.Business.Application.Commands.Requests;
using Bank.Business.Application.Queries.Handlers;
using Bank.Business.Application.Queries.Queries;
using Bank.Business.Domain.Contracts;
using Bank.Infrastructure.Bootstrap.Mapper;
using Bank.Infrastructure.Persistence.Contexts;
using Bank.Infrastructure.Persistence.Repositories;
using Bank.Infrastructure.Persistence.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Bank.Infrastructure.Bootstrap.DI
{
    public class Loader
    {
        public static void Register(IServiceCollection services, IConfiguration configuration, Action<Type> setMediator)
        {
            RegisterAutoMapperProfiles(services);
            RegisterDbContexts(services, configuration);
            RegisterUnitOfWorkData(services);
            RegisterRepositories(services);
            RegisterMediators(setMediator);
        }

        private static void RegisterAutoMapperProfiles(IServiceCollection services)
            => services.AddAutoMapper(Assembly.GetAssembly(typeof(BankMappingProfile)));

        private static void RegisterDbContexts(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BankContext>(options => options.UseInMemoryDatabase("BankTest"));
            //services.AddDbContext<BankContext>(options => options.UseSqlServer(configuration.GetConnectionString("BankDB")));
        }

        private static void RegisterUnitOfWorkData(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerAccountRepository, CustomerAccountRepository>();
            services.AddScoped<IFarePlanRepository, FarePlanRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
        }

        private static void RegisterMediators(Action<Type> setMediator)
        {
            setMediator(typeof(TransferCommandHandler));
            setMediator(typeof(TransferCommandRequest));

            setMediator(typeof(CustomerAccountQueryHandler));
            setMediator(typeof(CustomerAccountQuery));
            setMediator(typeof(FarePlanTaxQueryHandler));
            setMediator(typeof(FarePlanTransferTaxQuery));
            setMediator(typeof(TransferQueryHandler));
            setMediator(typeof(AccountTransferQuery));
            setMediator(typeof(AmountAccountMonthlyTransferQuery));
        }
    }
}
