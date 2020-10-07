using Bank.Business.Application.Commands.Handlers;
using Bank.Business.Application.Contracts;
using Bank.Business.Application.Queries.Handlers;
using Bank.Infrastructure.Persistence.Contexts;
using Bank.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Bank.Infrastructure.Bootstrap.DI
{
    public class Loader
    {
        public static void Register(IServiceCollection services, IConfiguration configuration, Action<Type> setMediator)
        {
            RegisterDbContexts(services, configuration);
            RegisterRepositories(services);
            RegisterMediators(setMediator);
        }

        private static void RegisterDbContexts(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<BankContext>(options => options.UseInMemoryDatabase("BankTest"));
            services.AddDbContext<BankContext>(options => options.UseSqlServer(configuration.GetConnectionString("BankDB")));
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
            setMediator(typeof(TransferHandler));
            setMediator(typeof(TransferQueryHandler));
        }
    }
}
