
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Abp.BankManagement.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CacheRefreshAttribute : ActionFilterAttribute
    {
        private readonly Type _keyContainerType;
        private readonly string[] _memberNames;

        // Usage: [CacheRefresh(typeof(AccountCacheKeys), "ListKey", "Last10Key")]
        public CacheRefreshAttribute(Type keyContainerType, params string[] memberNames)
        {
            _keyContainerType = keyContainerType ?? throw new ArgumentNullException(nameof(keyContainerType));
            _memberNames = memberNames ?? Array.Empty<string>();
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            if (resultContext.Exception != null) return;

            var cache = context.HttpContext.RequestServices.GetService<IDistributedCache>();
            var logger = context.HttpContext.RequestServices.GetService<ILogger<CacheRefreshAttribute>>();
            if (cache == null)
            {
                logger?.LogWarning("IDistributedCache not available; cache refresh skipped.");
                return;
            }

            foreach (var name in _memberNames)
            {
                try
                {
                    string? key = null;
                    var field = _keyContainerType.GetField(name, BindingFlags.Public | BindingFlags.Static);
                    if (field != null) key = field.GetValue(null) as string;
                    else
                    {
                        var prop = _keyContainerType.GetProperty(name, BindingFlags.Public | BindingFlags.Static);
                        if (prop != null) key = prop.GetValue(null) as string;
                    }

                    if (string.IsNullOrEmpty(key))
                    {
                        logger?.LogWarning("Cache key not found: {MemberName} on {Type}", name, _keyContainerType.FullName);
                        continue;
                    }

                    await cache.RemoveAsync(key);
                    logger?.LogDebug("Removed cache key: {CacheKey}", key);
                }
                catch (Exception ex)
                {
                    logger?.LogWarning(ex, "Failed to remove cache key for member {MemberName}", name);
                }
            }
        }
    }
}
