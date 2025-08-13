using Abp.BankManagement.EntityFrameworkCore;
using Abp.BankManagement.Etos.CustomerEtos;
using Abp.BankManagement.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.EntityFrameworkCore.DistributedEvents;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Abp.BankManagement
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(BankManagementDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(BankManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule),
        typeof(AbpEventBusModule),            // Distributed EventBus altyapısı
        typeof(AbpEventBusRabbitMqModule)     // RabbitMQ entegrasyonu
    )]
    public class BankManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // AutoMapper profilleri
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<BankManagementApplicationModule>();
            });

            // RabbitMQ EventBus ayarları
            Configure<AbpRabbitMqEventBusOptions>(options =>
            {
                options.ConnectionName = "Default";           // appsettings içindeki Connections:Default
                options.ClientName = "BankManagementClient";
                options.ExchangeName = "BankManagementExchange";
            });

            // (Opsiyonel) Outbox kullanıyorsan
            Configure<AbpDistributedEventBusOptions>(options =>
            {
                options.Outboxes.Configure(o =>
                {
                    o.UseDbContext<BankManagementDbContext>();
                });
            });
            // Event handler kaydı
            context.Services.AddTransient<IDistributedEventHandler<CustomerCreateEto>, CustomerCreateEventHandler>();
        }
    }
}
