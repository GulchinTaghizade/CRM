using CRM.DTO;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;

namespace CRM.Repositories.Interfaces
{
    public interface IAccountsRepository
    {
        List<Account> GetAccounts();
        Account GetAccount(int id);
        bool CreateAccount(AccountDto accountdto);
        Account ChangeCurrency(int id);
        Account DeleteAccount(int id);
        Account BlockAccount(int id);
        Account UnBlockAccount(int id);
    }
}
