using Bank.Business.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Infrastructure.Persistence.Configurations
{
    public class CustomerAccountConfiguration : IEntityTypeConfiguration<CustomerAccount>
    {
        public void Configure(EntityTypeBuilder<CustomerAccount> builder)
        {
            builder.HasKey(customerAccount => customerAccount.Id);

            builder.Property(customerAccount => customerAccount.CustomerId)
                .IsRequired();

            builder.Property(customerAccount => customerAccount.BankBranch)
                .IsRequired();
            
            builder.Property(customerAccount => customerAccount.BankAccount)
                .IsRequired();
            
            builder.Property(customerAccount => customerAccount.AccountTypeId)
                .IsRequired();
            
            builder.Property(customerAccount => customerAccount.FarePlanId)
                .IsRequired();

            builder.Property(customerAccount => customerAccount.IsActiveAccount)
                .IsRequired();

            builder.Property(customerAccount => customerAccount.Balance)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(customerAccount => customerAccount.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(customerAccount => customerAccount.ModifiedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
