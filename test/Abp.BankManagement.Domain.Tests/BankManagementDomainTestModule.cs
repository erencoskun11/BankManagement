using Volo.Abp.Modularity;

namespace Abp.BankManagement;

[DependsOn(
    typeof(BankManagementDomainModule),
    typeof(BankManagementTestBaseModule)
)]
public class BankManagementDomainTestModule : AbpModule
{

}
