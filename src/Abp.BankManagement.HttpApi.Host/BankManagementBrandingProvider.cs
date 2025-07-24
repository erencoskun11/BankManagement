using Microsoft.Extensions.Localization;
using Abp.BankManagement.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Abp.BankManagement;

[Dependency(ReplaceServices = true)]
public class BankManagementBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<BankManagementResource> _localizer;

    public BankManagementBrandingProvider(IStringLocalizer<BankManagementResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
