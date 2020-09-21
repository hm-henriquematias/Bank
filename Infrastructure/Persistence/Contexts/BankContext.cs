using Bank.Business.Domain.Entities;
using Bank.Business.Domain.Enums;
using Bank.Business.Domain.ValueObjects;
using Bank.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Persistence.Contexts
{
    public class BankContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<FarePlan> FarePlans { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        public BankContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerAccountConfiguration());
            modelBuilder.ApplyConfiguration(new AccountTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FarePlanConfiguration());
            modelBuilder.ApplyConfiguration(new TransferConfiguration());

            modelBuilder.Entity<AccountType>().HasData(
                new AccountType()
                {
                    AccountTypeId = (int) AccountTypeEnum.Corrente,
                    AccountTypeDescription = "corrente"
                },
                new AccountType()
                {
                    AccountTypeId = (int) AccountTypeEnum.Poupanca,
                    AccountTypeDescription = "poupanca"
                }
            );

            modelBuilder.Entity<FarePlan>().HasData(
                new FarePlan()
                {
                    FarePlanId = (int) FarePlanEnum.ServicosEssenciais,
                    FarePlanDescription = "servicos essenciais",
                    FreeTransfersQuantity = 2,
                },
                new FarePlan()
                {
                    FarePlanId = (int) FarePlanEnum.Basico,
                    FarePlanDescription = "basico",
                    FreeTransfersQuantity = 4,
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
