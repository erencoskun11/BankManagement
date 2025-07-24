using Abp.BankManagement.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Abp.BankManagement
{
    public static class BankManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureBankManagement(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new CardConfiguration());
            builder.ApplyConfiguration(new TransactionConfiguration());
            // Yeni configurationlar eklenirse buraya yazılır.
        }
    }
}
