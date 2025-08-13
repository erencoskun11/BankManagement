/*using Abp.BankManagement.Dtos.TransactionDtos;
using Abp.BankManagement.Etos.TransactionDtos;
using Abp.BankManagement.Models.Transactions;
using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;

namespace Abp.BankManagement.Grpc
{
    public class TransactionGrpcService
    {
    }

    public async Task<bool> CreateAsync(TransactionCreateRequest input)
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
            return TransactionCreateRespnse { };
            ;
        }
    }
*/