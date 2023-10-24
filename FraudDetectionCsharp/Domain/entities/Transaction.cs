using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Domain.entities
{
    [Table("Transactions")]
    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
        public bool IsForeignTransaction { get; set; }
        public string Country { get; set; }

        public bool IsApproved { get; set; }

        public TransactionStatus Status { get; set; }
    }

    public enum TransactionStatus
    {
        Pending,
        Completed,
        Failed,
        Fraudulent
    }
}
