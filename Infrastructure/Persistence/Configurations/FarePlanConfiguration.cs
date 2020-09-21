using Bank.Business.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Infrastructure.Persistence.Configurations
{
    public class FarePlanConfiguration : IEntityTypeConfiguration<FarePlan>
    {
        public void Configure(EntityTypeBuilder<FarePlan> builder)
        {
            builder.HasKey(customer => customer.FarePlanId);

            builder.Property(customer => customer.FarePlanDescription)
                .IsRequired();

            builder.Property(customer => customer.FreeTransfersQuantity)
                .IsRequired();
        }
    }
}
