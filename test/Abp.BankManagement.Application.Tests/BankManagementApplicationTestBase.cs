using Volo.Abp.Modularity;

namespace Abp.BankManagement;

public abstract class BankManagementApplicationTestBase<TStartupModule> : BankManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
