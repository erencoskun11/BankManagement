using Volo.Abp.Settings;

namespace Abp.BankManagement.Settings;

public class BankManagementSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BankManagementSettings.MySetting1));
    }
}
