using Xunit;

namespace Abp.BankManagement.EntityFrameworkCore;

[CollectionDefinition(BankManagementTestConsts.CollectionDefinitionName)]
public class BankManagementEntityFrameworkCoreCollection : ICollectionFixture<BankManagementEntityFrameworkCoreFixture>
{

}
