using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Domain.entities
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string AccountNumber { get; set; }

        [Required]
        public string AccountHolderName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public bool IsFrozen { get; set; }

        public bool IsBlocked { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public int UserId { get; set; }

        public void FreezeAccount()
        {
            IsFrozen = true;
        }

        public void UnfreezeAccount()
        {
            IsFrozen = false;
        }

        public void AddTransaction(Transaction transaction)
        {
            if (Transactions == null)
            {
                Transactions = new List<Transaction>();
            }

            Transactions.Add(transaction);
        }

    }
    public enum AccountType
    {
        Checking,
        Savings,
        Business
    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        Transfer
    }
}
