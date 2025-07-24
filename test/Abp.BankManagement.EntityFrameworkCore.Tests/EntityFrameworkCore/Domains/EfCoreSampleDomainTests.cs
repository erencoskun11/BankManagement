using Abp.BankManagement.Samples;
using Xunit;

namespace Abp.BankManagement.EntityFrameworkCore.Domains;

[Collection(BankManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<BankManagementEntityFrameworkCoreTestModule>
{

}
