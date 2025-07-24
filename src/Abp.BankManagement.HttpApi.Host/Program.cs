using System;
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

                var connectionString = builder.Configuration.GetConnectionString("Default");

                builder.Services.AddAbpDbContext<BankManagementDbContext>(options =>
                {
                    options.AddDefaultRepositories(includeAllEntities: true);
                });

                builder.Services.AddApplication<BankManagementHttpApiHostModule>();

                // Buraya eklendi:
                builder.Services.Configure<AbpMvcLibsOptions>(options =>
                {
                    options.CheckLibs = false;
                });

                // Swagger servisini ekle
                builder.Services.AddSwaggerGen();

                // CSRF (Antiforgery) korumasını devre dışı bırak
                builder.Services.AddControllersWithViews(options =>
                {
                    options.Filters.Add(new IgnoreAntiforgeryTokenAttribute());
                });
                builder.Services.AddAntiforgery(options => options.SuppressXFrameOptionsHeader = true);

                // Dynamic API Controller'ları tamamen devre dışı bırak
                builder.Services.Configure<AbpAspNetCoreMvcOptions>(options =>
                {
                    options.ConventionalControllers.Create(typeof(BankManagementApplicationModule).Assembly, opts =>
                    {
                        opts.TypePredicate = type => false; // ApplicationService tabanlı controller oluşturulmaz
                    });
                });

                var app = builder.Build();

                await app.InitializeApplicationAsync();

                // Swagger middleware
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankManagement API V1");
                    c.RoutePrefix = string.Empty; // Swagger ana sayfada
                });

                app.UseRouting();

                // Authentication/Authorization devre dışı
                // app.UseAuthentication();
                // app.UseAuthorization();
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
