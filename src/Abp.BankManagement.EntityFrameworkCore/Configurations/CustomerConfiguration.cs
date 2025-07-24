using Abp.BankManagement.Constant;
using Abp.BankManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Abp.BankManagement.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", BankManagementDatabaseConstants.SchemaName);
            builder.ConfigureByConvention();

            builder.Property(x => x.FullName)
                .HasMaxLength(100);

            builder.Property(x => x.NationalId)
                .HasMaxLength(11);

            builder.Property(x => x.BirthPlace)
                .HasMaxLength(50);

            builder.Property(x => x.RiskLimit)
                .HasPrecision(18, 2)
                .HasDefaultValue(10000);

            builder.Property(x => x.TenantId)
                .IsRequired(false);

            // 📌 Unique index for NationalId
            builder.HasIndex(x => x.NationalId).IsUnique();
        }
    }
}

