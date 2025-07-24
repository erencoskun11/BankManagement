using Abp.BankManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Abp.BankManagement.Permissions;

public class BankManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BankManagementPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BankManagementPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BankManagementResource>(name);
    }
}
