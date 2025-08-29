using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using Abp.BankManagement.EntityFrameworkCore;

public class BankManagementDbContextFactory : IDesignTimeDbContextFactory<BankManagementDbContext>
{
    public BankManagementDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<BankManagementDbContext>();
        var conn = config.GetConnectionString("Default")
                   ?? "Server=(localdb)\\MSSQLLocalDB;Database=BankManagementDb_alt;Trusted_Connection=True;";

        optionsBuilder.UseSqlServer(conn,
            sql => sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));

        return new BankManagementDbContext(optionsBuilder.Options);
    }
}
