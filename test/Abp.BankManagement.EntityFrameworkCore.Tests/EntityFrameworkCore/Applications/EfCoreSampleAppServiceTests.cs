using Abp.BankManagement.Samples;
using Xunit;

namespace Abp.BankManagement.EntityFrameworkCore.Applications;

[Collection(BankManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<BankManagementEntityFrameworkCoreTestModule>
{

}
