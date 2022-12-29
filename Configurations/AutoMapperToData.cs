using AutoMapper;
using CRM.DTO;
using CRM.Models;
using CRM.TransactionModels;

namespace CRM.Configurations
{
    public class AutoMapperToData:Profile
    {
        public AutoMapperToData()
        {
            CreateMap<Account, GetAccountDto>().ReverseMap();
            CreateMap<TransferTransactionModel, TransactionModel>().ReverseMap();
        }
    }
}
