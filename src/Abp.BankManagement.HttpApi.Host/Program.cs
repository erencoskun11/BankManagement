using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.BankManagement.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Libs;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;

namespace Abp.BankManagement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting HttpApi.Host...");

                var builder = WebApplication.CreateBuilder(args);

                builder.Host
                    .UseSerilog()
                    .UseAutofac();

                var configuration = builder.Configuration;

                // EF Core DbContext ve varsayılan repository'ler
                builder.Services.AddAbpDbContext<BankManagementDbContext>(options =>
                {
                    options.AddDefaultRepositories(includeAllEntities: true);
                });

                // ABP uygulama modülünü yükle
                builder.Services.AddApplication<BankManagementHttpApiHostModule>();

                // ABP MVC ayarları (libs kontrolünü kapatıyoruz)
                builder.Services.Configure<AbpMvcLibsOptions>(options =>
                {
                    options.CheckLibs = false;
                });

                // Swagger
                builder.Services.AddSwaggerGen();

                // MVC ve Antiforgery ayarları
                builder.Services.AddControllersWithViews(options =>
                {
                    options.Filters.Add(new IgnoreAntiforgeryTokenAttribute());
                });
                builder.Services.AddAntiforgery(options => options.SuppressXFrameOptionsHeader = true);

                // ABP konvansiyonel controller'ları override etme (opsiyonel)
                builder.Services.Configure<AbpAspNetCoreMvcOptions>(options =>
                {
                    options.ConventionalControllers.Create(typeof(BankManagementApplicationModule).Assembly, opts =>
                    {
                        opts.TypePredicate = type => false;
                    });
                });

                // CORS ayarları
                var corsOrigins = configuration["App:CorsOrigins"]
                    ?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(o => o.Trim())
                    .ToArray() ?? Array.Empty<string>();

                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("Default", policy =>
                    {
                        policy.WithOrigins(corsOrigins)
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
                });

              

                // KeyPrefix ayarı (isteğe bağlı)
                builder.Services.Configure<AbpDistributedCacheOptions>(options =>
                {
                    options.KeyPrefix = "BankManagement:";
                });

                var app = builder.Build();

                await app.InitializeApplicationAsync();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankManagement API V1");
                    c.RoutePrefix = string.Empty;
                });

                app.UseRouting();
                app.UseCors("Default");
                app.MapControllers();

                await app.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "HttpApi.Host terminated unexpectedly!");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
