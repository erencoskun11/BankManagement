using Abp.BankManagement.Dtos;
using Abp.BankManagement.Dtos.AccountDtos;
using Abp.BankManagement.Dtos.AccountTyoeDtos;
using Abp.BankManagement.Dtos.AccountTypeDtos;
using Abp.BankManagement.Dtos.CardDtos;
using Abp.BankManagement.Dtos.CardTypeDtos;
using Abp.BankManagement.Dtos.CustomerDtos;
using Abp.BankManagement.Dtos.TransactionDtos;
using Abp.BankManagement.Dtos.TransactionTypeDtos;
using Abp.BankManagement.Entities;
using Abp.BankManagement.Models.Accounts;
using Abp.BankManagement.Models.Cards;
using Abp.BankManagement.Models.Transactions;
using AutoMapper;

namespace Abp.BankManagement.AutoMapper;

public class BankManagementApplicationAutoMapperProfile : Profile
{
    public BankManagementApplicationAutoMapperProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>();

        CreateMap<Card, CardDto>();
        CreateMap<CreateCardDto, Card>();
        CreateMap<UpdateCardDto, Card>();

        CreateMap<Account, AccountDto>();
        CreateMap<CreateAccountDto, Account>();
        CreateMap<UpdateAccountDto, Account>();

        CreateMap<Transaction, TransactionDto>();
        CreateMap<CreateTransactionDto, Transaction>();
        CreateMap<UpdateTransactionDto, Transaction>();

        CreateMap<TransactionType, TransactionTypeDto>().ReverseMap();
        CreateMap<TransactionType, TransactionTypeCreateDto>().ReverseMap();

        CreateMap<CardType, CardTypeDto>().ReverseMap();
        CreateMap<CardType,CardTypeCreateDto>().ReverseMap();

        CreateMap<AccountType, AccountTypeDto>().ReverseMap();
        CreateMap<AccountType, AccountTypeCreateDto>().ReverseMap();

        CreateMap<CreateTransactionDto, TransactionCreateModel>();
        CreateMap<UpdateTransactionDto, TransactionUpdateModel>();

        CreateMap<CreateAccountDto, AccountCreateModel>();
        CreateMap<UpdateAccountDto, AccountUpdateModel>();

        CreateMap<CreateCardDto, CardCreateModel>();
        CreateMap<UpdateCardDto, CardUpdateModel>();




    }
}
