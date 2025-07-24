using Localization.Resources.AbpUi;
using Abp.BankManagement.Localization;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Abp.BankManagement;

[DependsOn(
    typeof(BankManagementApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule)
    )]
public class BankManagementHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<BankManagementResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
