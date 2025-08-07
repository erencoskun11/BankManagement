using Abp.BankManagement.Dtos.TransactionDtos;
using Abp.BankManagement.Entities;
using Abp.BankManagement.Managers;
using Abp.BankManagement.Models.Transactions;
using Abp.BankManagement.Repositories;
using Abp.BankManagement.Etos.TransactionDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Distributed;

namespace Abp.BankManagement.Services
{
    public class TransactionAppService : ApplicationService, ITransactionAppService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly TransactionManager _transactionManager;
        private readonly IDistributedEventBus _distributedEventBus;

        public TransactionAppService(
            ITransactionRepository transactionRepository,
            TransactionManager transactionManager,
            IDistributedEventBus distributedEventBus)
        {
            _transactionRepository = transactionRepository;
            _transactionManager = transactionManager;
            _distributedEventBus = distributedEventBus;
        }

        public async Task<bool> BulkCreateAsync(IEnumerable<CreateTransactionDto> transactions)
        {
            foreach (var dto in transactions)
            {
                var model = ObjectMapper.Map<CreateTransactionDto, TransactionCreateModel>(dto);
                var transaction = _transactionManager.Create(model);
                await _transactionRepository.InsertAsync(transaction);

                var eto = new TransactionCreateEto
                {
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    TransactionDate = transaction.TransactionDate,
                    AccountId = transaction.AccountId,
                    CardId = transaction.CardId,
                    TransactionTypeId = transaction.TransactionTypeId
                };

                await _distributedEventBus.PublishAsync(eto);
            }

            return true;
        }

        public async Task<bool> CreateAsync(CreateTransactionDto input)
        {
            var model = ObjectMapper.Map<CreateTransactionDto, TransactionCreateModel>(input);
            var transaction = _transactionManager.Create(model);
            await _transactionRepository.InsertAsync(transaction);

            var eto = new TransactionCreateEto
            {
                Amount = transaction.Amount,
                Description = transaction.Description,
                TransactionDate = transaction.TransactionDate,
                AccountId = transaction.AccountId,
                CardId = transaction.CardId,
                TransactionTypeId = transaction.TransactionTypeId
            };

            await _distributedEventBus.PublishAsync(eto);
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
            var transactions = await _transactionRepository.GetListAsync();
            return ObjectMapper.Map<List<Transaction>, List<TransactionDto>>(transactions);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateTransactionDto input)
        {
            var transaction = await _transactionRepository.GetAsync(id);
            if (transaction == null)
            {
                throw new UserFriendlyException("Transaction not found", $"No transaction exists with ID: {id}");
            }

            var updateModel = ObjectMapper.Map<UpdateTransactionDto, TransactionUpdateModel>(input);
            var updatedTransaction = _transactionManager.Update(transaction, updateModel);
            await _transactionRepository.UpdateAsync(updatedTransaction);

            var eto = new TransactionUpdateEto
            {
                Amount = updatedTransaction.Amount,
                Description = updatedTransaction.Description,
                TransactionDate = updatedTransaction.TransactionDate,
                AccountId = updatedTransaction.AccountId,
                CardId = updatedTransaction.CardId,
                TransactionTypeId = updatedTransaction.TransactionTypeId
            };

            await _distributedEventBus.PublishAsync(eto);
            return true;
        }
    }
}
