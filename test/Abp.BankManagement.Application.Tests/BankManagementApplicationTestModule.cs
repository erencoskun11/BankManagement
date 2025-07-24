using Volo.Abp.Modularity;

namespace Abp.BankManagement;

[DependsOn(
    typeof(BankManagementApplicationModule),
    typeof(BankManagementDomainTestModule)
)]
public class BankManagementApplicationTestModule : AbpModule
{

}
