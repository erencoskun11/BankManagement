using System;
using Abp.BankManagement.Constant;
using Abp.BankManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Abp.BankManagement.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts", BankManagementDatabaseConstants.SchemaName);
            builder.ConfigureByConvention();

            builder.Property(x => x.AccountName).HasMaxLength(50);
            builder.Property(x => x.IBAN).HasMaxLength(26);
            builder.Property(x => x.OpenedAt).HasDefaultValue(DateTime.UtcNow);


            // 📌 Customer ilişkisi
            builder.HasOne(x => x.Customer)
                .WithMany(c => c.Accounts)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // 📌 Benzersiz alanlar
            builder.HasIndex(x => x.AccountNumber).IsUnique();
            builder.HasIndex(x => x.IBAN).IsUnique();
        }
    }
}

