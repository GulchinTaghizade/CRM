using CRM.DAL;
using CRM.DTO;
using CRM.Enums;
using CRM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using CRM.Repositories.Interfaces;

namespace CRM.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly AppDbContext _context;

        public AccountsRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool CreateAccount(AccountDto accountDto)
        {
            var account = _context.Accounts.Where(x=>x.IsDeleted!=true).FirstOrDefault(x=>x.Name == accountDto.Name && x.Currency == accountDto.Currency);
            if (account == null)
                return true;
            else
                return false;
        }

        public Account DeleteAccount(int id)
        {
            return _context.Accounts.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == id);
        }

        public Account BlockAccount(int id)
        {
            return _context.Accounts.Where(x => x.IsDeleted == false && x.IsBlocked == false).FirstOrDefault(x => x.Id == id);
        }
        public Account GetAccount(int id)
        {
            return _context.Accounts.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == id);
        }
        public List<Account> GetAccounts()
        {
            return _context.Accounts.Include(x => x.User).Where(x => x.IsDeleted == false).ToList();
        }
        public Account UnBlockAccount(int id)
        {
            return _context.Accounts.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == id && x.IsBlocked == true);
        }
        public Account ChangeCurrency(int id)
        {
            return _context.Accounts.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == id);
        }
    }
}
