using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Abp.BankManagement.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.EntityFrameworkCore.DistributedEvents;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp.Security.Claims;
using Volo.Abp.AspNetCore.MultiTenancy;
using Abp.BankManagement.MultiTenancy;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Abp.BankManagement
{
    [DependsOn(
        typeof(BankManagementHttpApiModule),
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMultiTenancyModule),
        typeof(BankManagementApplicationModule),
        typeof(BankManagementEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
        typeof(AbpAccountWebOpenIddictModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpEventBusRabbitMqModule)
    )]
    public class BankManagementHttpApiHostModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            // OpenIddict doðrulama
            PreConfigure<OpenIddictBuilder>(builder =>
            {
                builder.AddValidation(options =>
                {
                    options.AddAudiences("BankManagement");
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });
            });

            // Outbox kullanýmý
            Configure<AbpDistributedEventBusOptions>(options =>
            {
                options.Outboxes.Configure(o => o.UseDbContext<BankManagementDbContext>());
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var env = context.Services.GetHostingEnvironment();

            ConfigureAuthentication(context);
            ConfigureBundles();
            ConfigureUrls(configuration);
            ConfigureVirtualFileSystem(env, context);
            ConfigureConventionalControllers();
            ConfigureCors(context, configuration);
            ConfigureSwagger(configuration, context);
            ConfigureRabbitMq(configuration, context);   // Güncellenen metot
            DisableAntiforgery(context);
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context)
        {
            context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
            context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(opts => opts.IsDynamicClaimsEnabled = true);
        }

        private void ConfigureBundles() => Configure<AbpBundlingOptions>(opts =>
        {
            opts.StyleBundles.Configure(LeptonXLiteThemeBundles.Styles.Global,
                bundle => bundle.AddFiles("/global-styles.css"));
        });

        private void ConfigureUrls(IConfiguration configuration) => Configure<AppUrlOptions>(opts =>
        {
            opts.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            opts.RedirectAllowedUrls.AddRange(
                configuration["App:RedirectAllowedUrls"]?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>());
            opts.Applications["Angular"].RootUrl = configuration["App:ClientUrl"];
            opts.Applications["Angular"].Urls["PasswordReset"] = "account/reset-password";
        });

        private void ConfigureVirtualFileSystem(IHostEnvironment env, ServiceConfigurationContext context)
        {
            if (env.IsDevelopment() && Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != "true")
            {
                var root = env.ContentRootPath;
                var baseDir = Path.GetFullPath(Path.Combine(root, ".."));
                Configure<AbpVirtualFileSystemOptions>(opts =>
                {
                    opts.FileSets.ReplaceEmbeddedByPhysical<BankManagementDomainSharedModule>(
                        Path.Combine(baseDir, "Abp.BankManagement.Domain.Shared"));
                    opts.FileSets.ReplaceEmbeddedByPhysical<BankManagementDomainModule>(
                        Path.Combine(baseDir, "Abp.BankManagement.Domain"));
                    opts.FileSets.ReplaceEmbeddedByPhysical<BankManagementApplicationContractsModule>(
                        Path.Combine(baseDir, "Abp.BankManagement.Application.Contracts"));
                    opts.FileSets.ReplaceEmbeddedByPhysical<BankManagementApplicationModule>(
                        Path.Combine(baseDir, "Abp.BankManagement.Application"));
                });
            }
        }

        private void ConfigureConventionalControllers() => Configure<AbpAspNetCoreMvcOptions>(opts =>
        {
            opts.ConventionalControllers.Create(typeof(BankManagementApplicationModule).Assembly);
        });

        private static void ConfigureSwagger(IConfiguration configuration, ServiceConfigurationContext context)
        {
            context.Services.AddAbpSwaggerGenWithOAuth(
                configuration["AuthServer:Authority"],
                new Dictionary<string, string> { { "BankManagement", "BankManagement API" } },
                opts =>
                {
                    opts.SwaggerDoc("v1", new OpenApiInfo { Title = "BankManagement API", Version = "v1" });
                    opts.DocInclusionPredicate((_, desc) => !desc.RelativePath!.StartsWith("api/abp/"));
                    opts.CustomSchemaIds(type => type.FullName);
                });
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration) => context.Services.AddCors(opts =>
        {
            opts.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(configuration["App:CorsOrigins"]!
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(o => o.TrimEnd('/'))
                        .ToArray())
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        private void ConfigureRabbitMq(IConfiguration configuration, ServiceConfigurationContext context)
        {
            // Burada JSON içindeki "RabbitMQ" kökünü ve altýndaki EventBus bölümünü okuyalým
            var rabbitSection = configuration.GetSection("RabbitMQ:EventBus");
            context.Services.Configure<AbpRabbitMqEventBusOptions>(opts =>
            {
                opts.ClientName = rabbitSection["ClientName"];
                opts.ExchangeName = rabbitSection["ExchangeName"];
                opts.ConnectionName = "Default"; // appsettings içindeki Connections:Default
            });
        }

        private void DisableAntiforgery(ServiceConfigurationContext context) => context.Services.Configure<MvcOptions>(opts =>
        {
            var filter = opts.Filters.OfType<AutoValidateAntiforgeryTokenAttribute>().FirstOrDefault();
            if (filter != null) opts.Filters.Remove(filter);
        });

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseAbpRequestLocalization();
            if (!env.IsDevelopment()) app.UseErrorPage();

            app.UseCorrelationId();
            app.MapAbpStaticAssets();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAbpOpenIddictValidation();
            if (MultiTenancyConsts.IsEnabled) app.UseMultiTenancy();
            app.UseUnitOfWork();
            app.UseDynamicClaims();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankManagement API");
                c.OAuthClientId(context.ServiceProvider.GetRequiredService<IConfiguration>()["AuthServer:SwaggerClientId"]);
                c.OAuthScopes("BankManagement");
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
