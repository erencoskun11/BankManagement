using Abp.BankManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Abp.BankManagement.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class BankManagementDbContext :
    AbpDbContext<BankManagementDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    public BankManagementDbContext(DbContextOptions<BankManagementDbContext> options)
        : base(options)
    {
    }

    // ABP Identity tabloları
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // ABP Tenant Management tabloları
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    // Application Entities
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<AccountType> AccountTypes { get; set; }
    public DbSet<CardType> CardTypes { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }

    public DbSet<IdentityUserDelegation> UserDelegations => throw new System.NotImplementedException();
    public DbSet<IdentitySession> Sessions => throw new System.NotImplementedException();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(BankManagementDbContext).Assembly);
        builder.ConfigureBankManagement();
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        // --------------------------------------------------
        // Aşağıda lookup entity’leri için GUID default ataması:
        builder.Entity<CardType>(b =>
        {
            b.Property(x => x.Id)
             .HasDefaultValueSql("NEWID()")
             .ValueGeneratedOnAdd();
        });

        builder.Entity<AccountType>(b =>
        {
            b.Property(x => x.Id)
             .HasDefaultValueSql("NEWID()")
             .ValueGeneratedOnAdd();
        });

        builder.Entity<TransactionType>(b =>
        {
            b.Property(x => x.Id)
             .HasDefaultValueSql("NEWID()")
             .ValueGeneratedOnAdd();
        });
        builder.Entity<TransactionType>(b =>
        {
            b.Property(x => x.Id)
             .ValueGeneratedOnAdd();
        });
        // --------------------------------------------------
    }
}
