using System;
using Abp.BankManagement.Dtos;
using Abp.BankManagement.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.BankManagement.Entities;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Abp.BankManagement.Services
{
    public class TransactionTypeAppService : ApplicationService, ITransactionTypeService
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository;

        public TransactionTypeAppService(ITransactionTypeRepository transactionTypeSRepository)
        {
            _transactionTypeRepository = transactionTypeSRepository;
        }

        public async Task<bool> CreateAsync(TransactionTypeDto transactionTypeDto)
        {
            var entity = ObjectMapper.Map<TransactionTypeDto, TransactionType>(transactionTypeDto);

            await _transactionTypeRepository.InsertAsync(entity);
            return true;

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _transactionTypeRepository.DeleteAsync(id);
            return true;
        }
        public async Task<bool> UpdateAsync(TransactionTypeDto transactionTypeDto)
        {
            var entity = await _transactionTypeRepository.FindAsync(x => x.Id == transactionTypeDto.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("TransactionType not found.");
            }

            ObjectMapper.Map(transactionTypeDto, entity);

            await _transactionTypeRepository.UpdateAsync(entity);

            return true;
        }
        public async Task<TransactionTypeDto> GetAsync(Guid id)
        {
            var entity = await _transactionTypeRepository.GetAsync(id);
            return ObjectMapper.Map<TransactionType, TransactionTypeDto>(entity);
        }

        public async Task<List<TransactionTypeDto>> GetListAsync()
        {
            var entities = await _transactionTypeRepository.GetListAsync();
            return ObjectMapper.Map<List<TransactionType>, List<TransactionTypeDto>>(entities);
        }



    }
}
