using Abp.BankManagement.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Abp.BankManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BankManagementEntityFrameworkCoreModule),
    typeof(BankManagementApplicationContractsModule)
    )]
public class BankManagementDbMigratorModule : AbpModule
{
}
