 using CRM.DTO;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace CRM.Services.Interface
{
    public interface IAccountsService
    {
        List<GetAccountDto> GetAccounts();
        GetAccountDto GetAccount(int id);
        Account CreateAccount(AccountDto account);
        bool ChangeCurrency(int id, byte currency);
        bool DeleteAccount(int id);
        bool BlockAccount(int id);
        bool UnBlockAccount(int id);
    }
}
