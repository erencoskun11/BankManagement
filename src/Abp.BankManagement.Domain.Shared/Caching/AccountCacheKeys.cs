using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.BankManagement.Caching
{
    public class AccountCacheKeys
    {
        public const string Prefix = "BankManagement:Account:";
        public static string ListKey => Prefix + "All";
        public static string ItemKey(Guid id) => Prefix + $"Item:{id}";
        public static string ListByCustomer(Guid customerId) => Prefix + $"Customer:{customerId}:All";
        public static string Last10Key => Prefix + "Last10";
    }
}
