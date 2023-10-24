using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FraudDetectionCsharp.Domain.entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public string PaymentConfirmationNumber { get; set; }



    }

}