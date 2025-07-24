using Abp.BankManagement.Dtos.TransactionDtos;
using Abp.BankManagement.Managers;
using Abp.BankManagement.Models.Transactions;
using Abp.BankManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.BankManagement.Entities;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Abp.BankManagement.Services
{
    public class TransactionAppService : ApplicationService, ITransactionAppService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly TransactionManager _transactionManager;

        public TransactionAppService(ITransactionRepository transactionRepository,TransactionManager transactionManager)
        {
            _transactionRepository = transactionRepository;
            _transactionManager = transactionManager;
        }

        public async Task<bool> CreateAsync(CreateTransactionDto input)
        {
            var createModel = ObjectMapper.Map<CreateTransactionDto, TransactionCreateModel>(input);
            var transaction = _transactionManager.Create(createModel); 
            await _transactionRepository.InsertAsync(transaction);
            return true;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            await _transactionRepository.DeleteAsync(id);
            return true;
        }

       

        public async Task<TransactionDto> GetAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetAsync(id);
            return ObjectMapper.Map<Transaction, TransactionDto>(transaction);
        }

        public async Task<List<TransactionDto>> GetByAccountIdAsync(Guid accountId)
        {
            var transactions = await _transactionRepository.GetByAccountIdAsync(accountId);
            return ObjectMapper.Map<List<Transaction>, List<TransactionDto>>(transactions);
        }

        public async Task<List<TransactionDto>> GetByCardIdAsync(Guid cardId)
        {
            var transactions = await _transactionRepository.GetByCardIdAsync(cardId);
            return ObjectMapper.Map<List<Transaction>, List<TransactionDto>>(transactions);
        }

        public async Task<List<TransactionDto>> GetTransactionByDateRangeAsync(Guid accountId, DateTime start, DateTime end)
        {
            var transactions = await _transactionRepository.GetTransactionByDateRangeAsync(accountId, start, end);
            return ObjectMapper.Map<List<Transaction>, List<TransactionDto>>(transactions);
        }

        public async Task<List<TransactionDto>> GetByTypeIdAsync(Guid transactionTypeId)
        {
            var transactions = await _transactionRepository.GetByTypeIdAsync(transactionTypeId);
            return ObjectMapper.Map<List<Transaction>, List<TransactionDto>>(transactions);
        }
        


        public async Task<List<TransactionDto>> GetListAsync()
        {
            var transaction = await _transactionRepository.GetListAsync();
            return ObjectMapper.Map<List<Transaction>, List<TransactionDto>>(transaction);
           
        }

        

        public async Task<bool> UpdateAsync(Guid id,UpdateTransactionDto input)
        {
            var transaction = await _transactionRepository.GetAsync(id);
            if(transaction == null)
            {
                throw new UserFriendlyException($"Transaction with Id '{id}' not found.");

            }

            var updateModel = ObjectMapper.Map<UpdateTransactionDto,TransactionUpdateModel>(input);
            var updateTransaction = _transactionManager.Update(transaction, updateModel);
            await _transactionRepository.UpdateAsync(updateTransaction);
            return true;
        }

    }
}
