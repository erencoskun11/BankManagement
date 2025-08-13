using Abp.BankManagement.Constant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.DistributedEvents;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Abp.BankManagement.Configurations
{
    public static class EventInboxConfiguration
    {
        public static void ConfigureEventOutbox([NotNull] this ModelBuilder builder)
        {
            builder.Entity<IncomingEventRecord>(b =>
            {
                b.ToTable("EventInbox", BankManagementDatabaseConstants.SchemaName);
                b.ConfigureByConvention();
                b.HasIndex(x => x.CreationTime);
            });
        }
    }
}
