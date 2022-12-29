using CRM.DAL;
using CRM.DTO;
using CRM.Enums;
using CRM.Models;
using CRM.Repositories.Interfaces;
using CRM.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AutoMapper;

namespace CRM.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly AppDbContext _context;
        private readonly IAccountsRepository _accountsRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AccountsService(AppDbContext context, IAccountsRepository accountsRepository, UserManager<AppUser> userManager,IMapper mapper) 
        {
            _context = context;
            _accountsRepository = accountsRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        public Account CreateAccount(AccountDto accountDto)
        {
            var result = _accountsRepository.CreateAccount(accountDto);
            if (result == true)
            {
                Account account = new Account
                {
                    Name = accountDto.Name,
                    Currency = accountDto.Currency,
                    IsDeleted = false
                };
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return account;
            }
            else
                return null;
        }

        public bool DeleteAccount(int id)
        {
            var result = _accountsRepository.DeleteAccount(id);
            if (result != null)
            {
                result.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            else { return false; }
        }

        public bool BlockAccount(int id)
        {
            var result = _accountsRepository.BlockAccount(id);
            if (result != null)
            {
                result.IsBlocked = true;
                result.LockDate = DateTime.UtcNow.AddHours(4);
                _context.SaveChanges();
                return true;
            }
            else { return false; }
        }

        public GetAccountDto GetAccount(int id)
        {
            var result = _accountsRepository.GetAccount(id);
            if (result != null)
                //_mapper.Map<Account, GetAccountDto>(result);
                return _mapper.Map<Account, GetAccountDto>(result);
            return null;
        }

        public List<GetAccountDto> GetAccounts()
        {
            var result = _accountsRepository.GetAccounts();
            if (result != null)
                return _mapper.Map<List<GetAccountDto>>(result);
            else return null;
        }

        public bool UnBlockAccount(int id)
        {
            var result = _accountsRepository.UnBlockAccount(id);
            if (result != null)
            {
                result.IsBlocked = true;
                result.LockDate = null;
                _context.SaveChanges();
                return true;
            }
            else { return false; }
        }

        public bool ChangeCurrency(int id, byte currency)
        {
            var result = _accountsRepository.ChangeCurrency(id);
            if (result == null) { return false; }
            if (Enum.IsDefined(typeof(Currency), currency))
            {
                result.Currency = (Currency)currency;
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

    }
}
