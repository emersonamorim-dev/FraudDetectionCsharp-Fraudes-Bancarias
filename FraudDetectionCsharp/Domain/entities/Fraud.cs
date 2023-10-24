using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Domain.entities
{
    public class Fraud
    {
        [Key]
        public int Id { get; set; }

        public int AccountId { get; set; }

        public int TransactionId { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 5)]
        public string Description { get; set; }

        [Required]
        public FraudType Type { get; set; }

        [Required]
        public FraudStatus Status { get; set; }

        [Required]
        public DateTime DetectedDate { get; set; }

        public DateTime? ResolvedDate { get; set; }

        public decimal? FraudAmount { get; set; }

        [StringLength(500)]
        public string DetectedBy { get; set; }

        public int PaymentId { get; set; }

        public string Notes { get; set; }

        // Adicionando métodos que podem ser úteis
        public void ResolveFraud(string resolver, DateTime resolvedDate)
        {
            Status = FraudStatus.Resolved;
            ResolvedDate = resolvedDate;
            Notes += $"Fraud resolved by {resolver} on {resolvedDate}. ";
        }

        public void UpdateStatus(FraudStatus status)
        {
            Status = status;
        }

        public void AddNote(string note)
        {
            Notes += note + " ";
        }
    }

    public enum FraudType
    {
        IdentityTheft,
        CardFraud,
        Phishing,
        AccountTakeover,
        Other
    }

    public enum FraudStatus
    {
        Processed,
        Detected,
        UnderInvestigation,
        Resolved,
        Unresolved
    }
}
