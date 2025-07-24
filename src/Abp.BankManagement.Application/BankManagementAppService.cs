using Abp.BankManagement.Localization;
using Volo.Abp.Application.Services;

namespace Abp.BankManagement;

/* Inherit your application services from this class.
 */
public abstract class BankManagementAppService : ApplicationService
{
    protected BankManagementAppService()
    {
        LocalizationResource = typeof(BankManagementResource);
    }
}
