using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Domain.interfaces;
using System;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Core.rules
{
    public class TransactionFraudDetectionRules
    {
        private const int MAX_TRANSACTIONS_PER_DAY = 5;
        private const decimal MAX_TRANSACTION_AMOUNT = 5000M;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionFraudDetectionRules(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        }

        public async Task<bool> IsFraudulentTransactionAsync(Transaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            bool isHighValueTransaction = transaction.Amount > MAX_TRANSACTION_AMOUNT;
            bool hasExceededDailyLimit = await HasExceededDailyTransactionLimit(transaction);

            return isHighValueTransaction || hasExceededDailyLimit;
        }

        private async Task<bool> HasExceededDailyTransactionLimit(Transaction transaction)
        {
            var transactionsOnSameDay = await _transactionRepository.GetTransactionCountForUserOnDateAsync(transaction.UserId, transaction.Date.Date);
            return transactionsOnSameDay >= MAX_TRANSACTIONS_PER_DAY;
        }
    }
}
