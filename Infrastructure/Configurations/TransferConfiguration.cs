using Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bank.Infrastructure.Configurations
{
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.HasKey(customerAccount => customerAccount.Id);

            builder.Property(customerAccount => customerAccount.Value)
                .IsRequired();

            builder.Property(customerAccount => customerAccount.From)
                .IsRequired()
                .HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<CustomerAccount>(
                    v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                  )
                );

            builder.Property(customerAccount => customerAccount.To)
                .IsRequired()
                .HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<CustomerAccount>(
                    v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                  )
                ); ;

            builder.Property(customerAccount => customerAccount.Tax)
                .IsRequired();

            builder.Property(customerAccount => customerAccount.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(customerAccount => customerAccount.ModifiedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
