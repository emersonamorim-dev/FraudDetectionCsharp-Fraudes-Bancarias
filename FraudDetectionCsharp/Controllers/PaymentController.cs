using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FraudDetectionCsharp.Domain.services;
using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Core.rules;

namespace FraudDetectionCsharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly PaymentFraudDetectionRules _paymentfraudDetectionRules;

        public PaymentController(PaymentService paymentService, PaymentFraudDetectionRules paymentfraudDetectionRules)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            _paymentfraudDetectionRules = paymentfraudDetectionRules ?? throw new ArgumentNullException(nameof(paymentfraudDetectionRules));
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreatePaymentAsync([FromBody] Payment payment)
        {
            if (payment == null)
            {
                return BadRequest("O pagamento é nulo.");
            }

            // Verifique se o pagamento é potencialmente fraudulento
            bool isFraudulent = await _paymentfraudDetectionRules.IsFraudulentPaymentAsync(payment);


            if (isFraudulent)
            {
                return BadRequest("O pagamento é potencialmente fraudulento.");
            }

            await _paymentService.CreatePaymentAsync(payment);

            return Ok(payment);
        }
    }
}

