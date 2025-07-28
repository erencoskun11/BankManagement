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
using Abp.BankManagement.Publishers;
using Abp.BankManagement.Etos.TransactionDtos;

namespace Abp.BankManagement.Services
{
    public class TransactionAppService : ApplicationService, ITransactionAppService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly TransactionManager _transactionManager;
        private readonly TransactionEventPublisher _transactionEventPublisher;
        
        public TransactionAppService(ITransactionRepository transactionRepository,TransactionManager transactionManager,
            TransactionEventPublisher transactionEventPublisher)
        {
            _transactionRepository = transactionRepository;
            _transactionManager = transactionManager;
            _transactionEventPublisher = transactionEventPublisher;
        }

        public async Task<bool> CreateAsync(CreateTransactionDto input)
        {
            var createModel = ObjectMapper.Map<CreateTransactionDto, TransactionCreateModel>(input);
            var transaction = _transactionManager.Create(createModel); 
            await _transactionRepository.InsertAsync(transaction);

            var eto = new TransactionCreateEto
            {
                Amount = transaction.Amount,
                Description = transaction.Description,
                TransactionDate = transaction.TransactionDate,
                AccountId = transaction.AccountId,
                CardId = transaction.CardId,
                TransactionTypeId = transaction.TransactionTypeId,
            };
            await _transactionEventPublisher.PublishTransactionCreatedAsync(eto);
            
            
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
                throw new UserFriendlyException($"Transaction with Id '{id}' not found.");// bunu degistirmeliyim bu sekilde olmaz 

            }

            var updateModel = ObjectMapper.Map<UpdateTransactionDto,TransactionUpdateModel>(input);
            var updatedTransaction = _transactionManager.Update(transaction, updateModel);
            await _transactionRepository.UpdateAsync(updatedTransaction);

            var updateEto = new TransactionUpdateEto
            {
                Amount = updatedTransaction.Amount,
                Description = updatedTransaction.Description,
                TransactionDate = updatedTransaction.TransactionDate,
                AccountId = updatedTransaction.AccountId,
                CardId = updatedTransaction.CardId,
                TransactionTypeId = updatedTransaction.TransactionTypeId,
            };

            await _transactionEventPublisher.PublishTransactionUpdatedAsync(updateEto);
            
            return true;
        }

    }
}
