using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM.DAL;
using CRM.Models;
using System.Numerics;
using CRM.DTO;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CRM.Enums;
using CRM.Services.Interface;
using AutoMapper;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAccountsService _accountsService;
        private readonly IMapper _mapper;

        public AccountsController(AppDbContext context, UserManager<AppUser> userManager, IAccountsService accountsService, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _accountsService = accountsService;
            _mapper = mapper;
        }
        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAccountDto>>> GetAccounts()
        {
            var result = _accountsService.GetAccounts();
            if (result != null) return result;
            else return NotFound();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAccountDto>> GetAccount(int id)
        {
            var account = _accountsService.GetAccount(id);
            if (account == null)
                return NotFound();
            return account;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountDto accountModel)
        {
            var result = _accountsService.CreateAccount(accountModel);
            if (result != null)
            {
                AppUser user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Name).Value);
                if (user == null) { return NotFound(); }
                else
                {
                    result.User = user;
                    await _context.SaveChangesAsync();
                }
            }
            return Ok(result);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var result = _accountsService.DeleteAccount(id);
            if (result) { return Ok("account successfully deleted"); }
            else { return NotFound("account not found"); }
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
        [HttpPatch("changeCurrency")]
        public async Task<IActionResult> ChangeCurrency(int id, byte currency)
        {
            var result = _accountsService.ChangeCurrency(id, currency);
            if (result) return Ok("currency successfully changed");
            else return NotFound("Currency not found");
        }
        [HttpPut("block")]
        public async Task<IActionResult> BlockAccount(int id)
        {
            var result = _accountsService.BlockAccount(id);
            if (result) return Ok("account successfully blocked");
            else return NotFound();
        }
        [HttpPut("unBlock")]
        public async Task<IActionResult> UnBlockAccount(int id)
        {
            var result = _accountsService.UnBlockAccount(id);
            if (result) return Ok("Account successfully unblocked");
            else return NotFound();
        }
    }
}