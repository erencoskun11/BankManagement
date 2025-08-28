using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace Abp.BankManagement.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CacheableAttribute : ActionFilterAttribute
    {
        private readonly Type _keyContainer;
        private readonly string _memberName;
        private readonly TimeSpan _ttl;

        public CacheableAttribute(Type keyContainer, string memberName, int seconds = 600)
        {
            _keyContainer = keyContainer ?? throw new ArgumentNullException(nameof(keyContainer));
            _memberName = memberName ?? throw new ArgumentNullException(nameof(memberName));
            _ttl = TimeSpan.FromSeconds(seconds);
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<CacheableAttribute>>();
            var cache = context.HttpContext.RequestServices.GetService<IDistributedCache>();
            if (cache == null) { await next(); return; }

            // Resolve key (field/property or template with {arg})
            var key = ResolveKey(context);
            if (string.IsNullOrEmpty(key)) { await next(); return; }

            try
            {
                var cached = await cache.GetStringAsync(key);
                if (!string.IsNullOrEmpty(cached))
                {
                    // Return cached JSON directly
                    context.Result = new ContentResult
                    {
                        Content = cached,
                        ContentType = "application/json",
                        StatusCode = 200
                    };
                    logger?.LogDebug("Cache hit: {Key}", key);
                    return;
                }
            }
            catch (Exception ex)
            {
                logger?.LogWarning(ex, "Cache read failed for {Key}", key);
            }

            // Execute action
            var executed = await next();

            if (executed.Exception != null) return;

            // Extract object result (simple cases)
            object? value = null;
            if (executed.Result is ObjectResult orObj)
                value = orObj.Value;
            else if (executed.Result is JsonResult jr)
                value = jr.Value;

            if (value == null) return;

            try
            {
                var json = JsonSerializer.Serialize(value, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                await cache.SetStringAsync(key, json, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _ttl });
                logger?.LogDebug("Cached {Key}", key);
            }
            catch (Exception ex)
            {
                logger?.LogWarning(ex, "Cache write failed for {Key}", key);
            }
        }

        private string? ResolveKey(ActionExecutingContext context)
        {
            // 1. Try static field
            var field = _keyContainer.GetField(_memberName, BindingFlags.Public | BindingFlags.Static);
            if (field != null)
            {
                var v = field.GetValue(null) as string;
                return ApplyPlaceholders(v, context);
            }

            // 2. Try static property
            var prop = _keyContainer.GetProperty(_memberName, BindingFlags.Public | BindingFlags.Static);
            if (prop != null)
            {
                var v = prop.GetValue(null) as string;
                return ApplyPlaceholders(v, context);
            }

            return null;
        }

        private static string? ApplyPlaceholders(string? template, ActionExecutingContext ctx)
        {
            if (string.IsNullOrEmpty(template)) return template;
            foreach (var kv in ctx.ActionArguments)
            {
                var ph = "{" + kv.Key + "}";
                if (template.Contains(ph, StringComparison.OrdinalIgnoreCase) && kv.Value != null)
                    template = template.Replace(ph, kv.Value.ToString()!, StringComparison.OrdinalIgnoreCase);
            }
            return template;
        }
    }
}
