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
                    AccountTypeId = (int)AccountTypeEnum.Corrente,
                    AccountTypeDescription = "corrente"
                },
                new AccountType()
                {
                    AccountTypeId = (int)AccountTypeEnum.Poupanca,
                    AccountTypeDescription = "poupanca"
                }
            );

            modelBuilder.Entity<FarePlan>().HasData(
                new FarePlan()
                {
                    FarePlanId = (int)FarePlanEnum.ServicosEssenciais,
                    FarePlanDescription = "servicos essenciais",
                    FreeTransfersQuantity = 2,
                },
                new FarePlan()
                {
                    FarePlanId = (int)FarePlanEnum.Basico,
                    FarePlanDescription = "basico",
                    FreeTransfersQuantity = 4,
                }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    Id = 1,
                    Name = "A",
                    RegistrationNumber = "123",
                },
                new Customer()
                {
                    Id = 2,
                    Name = "B",
                    RegistrationNumber = "456",
                }
            );

            modelBuilder.Entity<CustomerAccount>().HasData(
                new CustomerAccount()
                {
                    Id = 1,
                    CustomerId = 1,
                    BankBranch = 1,
                    BankAccount = 1,
                    AccountTypeId = (int)AccountTypeEnum.Corrente,
                    FarePlanId = (int)FarePlanEnum.ServicosEssenciais,
                    IsActiveAccount = true,
                    Balance = 100,
                },
                new CustomerAccount()
                {
                    Id = 2,
                    CustomerId = 2,
                    BankBranch = 1,
                    BankAccount = 2,
                    AccountTypeId = (int)AccountTypeEnum.Corrente,
                    FarePlanId = (int)FarePlanEnum.ServicosEssenciais,
                    IsActiveAccount = true,
                    Balance = 100,
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
