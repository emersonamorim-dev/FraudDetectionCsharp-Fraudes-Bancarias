using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Domain.services;
using FraudDetectionCsharp.Core.rules;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace FraudDetectionCsharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;
        private readonly TransactionFraudDetectionRules _transactionfraudDetectionRules;

        public TransactionController(TransactionService transactionService, TransactionFraudDetectionRules transactionfraudDetectionRules)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _transactionfraudDetectionRules = transactionfraudDetectionRules ?? throw new ArgumentNullException(nameof(transactionfraudDetectionRules));
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateTransactionAsync([FromBody] Transaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest("A transação é nula.");
            }

            bool isFraudulent = await _transactionfraudDetectionRules.IsFraudulentTransactionAsync(transaction);
            if (isFraudulent)
            {
                return BadRequest("A transação é potencialmente fraudulenta.");
            }

            await _transactionService.CreateTransactionAsync(transaction);
            return Ok(transaction);
        }
    }
}
