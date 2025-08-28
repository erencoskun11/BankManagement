using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.BankManagement.Caching
{
    public static class CacheKeys<T>
    {
        private static string Base => $"bank:{typeof(T).FullName!.Replace('.', ':').ToLowerInvariant()}";
        public static string ListKey => $"{Base}:list";
        public static string Last10Key => $"{Base}:last10";
        public static string ItemKey(Guid id) => $"{Base}:item:{id}";
        public static string ItemKeyTemplate => $"{Base}:item:{{id}}"; // {id} placeholder desteğiyle kullanılabilir
                                                                       // Liste için key
       

    }
}
