using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;
using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Domain.services;
using FraudDetectionCsharp.Core.rules;

namespace FraudDetectionCsharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly FraudDetectionRules _fraudDetectionRules;

        public AccountController(AccountService accountService, FraudDetectionRules fraudDetectionRules)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _fraudDetectionRules = fraudDetectionRules ?? throw new ArgumentNullException(nameof(fraudDetectionRules));
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            if (account == null)
            {
                return BadRequest("A conta é nula.");
            }

            bool isFraudulent = await _fraudDetectionRules.IsFraudulentAsync(account);
            if (isFraudulent)
            {
                return BadRequest("A conta é potencialmente fraudulenta.");
            }

            await _accountService.CreateAccountAsync(account);

            return Ok(account);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            if (accounts == null || !accounts.Any())
            {
                return NotFound("Nenhuma conta encontrada.");
            }

            return Ok(accounts);
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound($"Conta com ID {id} não encontrada.");
            }

            return Ok(account);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var existingAccount = await _accountService.GetAccountByIdAsync(id);
            if (existingAccount == null)
            {
                return NotFound($"Conta com ID {id} não encontrada.");
            }

            await _accountService.DeleteAccountAsync(id);

            return NoContent(); 
        }

    }
}
