using Abp.BankManagement.Constant;
using Abp.BankManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Abp.BankManagement.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards", BankManagementDatabaseConstants.SchemaName);
            builder.ConfigureByConvention();

            builder.Property(x => x.CardNumber)
                .HasMaxLength(16);

            builder.Property(x => x.CCV)
                .HasMaxLength(3);

            builder.Property(x => x.IsActive).HasDefaultValue(true);

            // 📌 Account ilişkisi
            builder.HasOne(c => c.Account)
                .WithMany(a => a.Cards)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // 📌 CardType ilişkisi
            builder.HasOne(c => c.CardType)
                .WithMany(ct => ct.Cards)
                .HasForeignKey(c => c.CardTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.CardNumber).IsUnique();
        }
    }
}

