using System;
using Abp.BankManagement.Constant;
using Abp.BankManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Abp.BankManagement.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions", BankManagementDatabaseConstants.SchemaName);
            builder.ConfigureByConvention();

            builder.Property(x => x.Amount)
                .HasPrecision(18, 2);

            builder.Property(x => x.Description)
                .HasMaxLength(100);

            builder.Property(x => x.TransactionDate)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Property(x => x.TenantId)
                .IsRequired(false);

            builder.HasOne(t => t.Account)
                   .WithMany(a => a.Transactions)
                   .HasForeignKey(t => t.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Card)
                   .WithMany(c => c.Transactions)
                   .HasForeignKey(t => t.CardId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.TransactionType)
                   .WithMany(tt => tt.Transactions)
                   .HasForeignKey(t => t.TransactionTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
