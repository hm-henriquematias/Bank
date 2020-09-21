using Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(customer => customer.Id);

            builder.Property(customer => customer.Name)
                .IsRequired();

            builder.Property(customer => customer.RegistrationNumber)
                .IsRequired();

            builder.Property(customer => customer.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(customer => customer.ModifiedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

        }
    }
}
