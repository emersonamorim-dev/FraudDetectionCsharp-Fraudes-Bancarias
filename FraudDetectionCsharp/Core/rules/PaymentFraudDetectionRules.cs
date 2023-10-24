using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Domain.interfaces;
using System;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Core.rules
{
    public class PaymentFraudDetectionRules
    {
        private const decimal LARGE_PAYMENT_THRESHOLD = 5000M;


        public PaymentFraudDetectionRules()
        {
        }

        public async Task<bool> IsFraudulentPaymentAsync(Payment payment)

        {
            ValidatePayment(payment);
            var isLargePayment = IsLargePayment(payment);

            return isLargePayment;
        }

        private void ValidatePayment(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }
        }

        private bool IsLargePayment(Payment payment)
        {
            return payment.Amount > LARGE_PAYMENT_THRESHOLD;
        }



    }
}
