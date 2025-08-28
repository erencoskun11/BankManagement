using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        private const string HttpContextItemsKey = "CacheRefresh:Keys";

        public CacheRefreshAttribute(Type keyContainerType, params string[] memberNames)
        {
            _keyContainerType = keyContainerType ?? throw new ArgumentNullException(nameof(keyContainerType));
            _memberNames = memberNames ?? Array.Empty<string>();
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (resultContext.Exception != null) return;

            var keys = new List<string>();
            var logger = context.HttpContext.RequestServices.GetService<ILogger<CacheRefreshAttribute>>();

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

                    keys.Add(key);
                }
                catch (Exception ex)
                {
                    logger?.LogWarning(ex, "Failed to resolve cache key for member {MemberName}", name);
                }
            }

            if (keys.Any())
            {
                context.HttpContext.Items[HttpContextItemsKey] = keys;
            }
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<CacheRefreshAttribute>>();

            var status = context.HttpContext.Response?.StatusCode ?? 0;
            if (status < 200 || status >= 300)
            {
                logger?.LogDebug("Response status {StatusCode}, cache refresh skipped.", status);
                return;
            }

            if (!context.HttpContext.Items.TryGetValue(HttpContextItemsKey, out var obj) || obj == null)
            {
                logger?.LogDebug("No cache keys were registered for refresh.");
                return;
            }

            if (obj is not IEnumerable<string> keys)
            {
                logger?.LogWarning("Cache keys in HttpContext.Items have unexpected type: {Type}", obj.GetType().FullName);
                return;
            }

            var cache = context.HttpContext.RequestServices.GetService<IDistributedCache>();
            if (cache == null)
            {
                logger?.LogWarning("IDistributedCache not available; cache refresh skipped.");
                return;
            }

            foreach (var key in keys)
            {
                try
                {
                    cache.Remove(key);
                    logger?.LogDebug("Removed cache key: {CacheKey}", key);
                }
                catch (Exception ex)
                {
                    logger?.LogWarning(ex, "Failed to remove cache key {CacheKey}", key);
                }
            }

            context.HttpContext.Items.Remove(HttpContextItemsKey);
        }
    }
}
