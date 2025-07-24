using System;
using Abp.BankManagement.Entities;
using Volo.Abp.Domain.Repositories;

namespace Abp.BankManagement.Repositories
{
    public interface IAccountTypeRepository : IRepository<AccountType,Guid>
    {
    }
}
