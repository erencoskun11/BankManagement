using System.Threading.Tasks;

namespace Abp.BankManagement.Data;

public interface IBankManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
