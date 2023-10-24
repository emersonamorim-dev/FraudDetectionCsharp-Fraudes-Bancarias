using FraudDetectionCsharp.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Core.rules
{
    public class FraudDetectionRules
    {

        public async Task<bool> IsFraudulentAsync(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            // Verifica se a conta tem transações em países estrangeiros frequentes em um curto período de tempo
            bool hasFrequentForeignTransactions = await HasFrequentForeignTransactionsAsync(account);

            // Verifica se há uma série de saques grandes em um curto período de tempo
            bool hasLargeWithdrawals = await HasLargeWithdrawalsAsync(account);

            // Verifica se há múltiplas transações de grande valor em um curto período de tempo
            bool hasMultipleLargeTransactions = await HasMultipleLargeTransactionsAsync(account);

            // Verifica se o saldo da conta teve flutuações significativas em um curto período de tempo
            bool hasRapidBalanceFluctuation = await HasRapidBalanceFluctuationAsync(account);

            // Retorna true se qualquer uma das regras acima for verdadeira, indicando uma possível fraude
            return hasFrequentForeignTransactions
                || hasLargeWithdrawals
                || hasMultipleLargeTransactions
                || hasRapidBalanceFluctuation;

        }

        // Verifica se a conta tem transações em países estrangeiros frequentes em um curto período de tempo
        public async Task<bool> HasFrequentForeignTransactionsAsync(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            // Simula uma operação assíncrona, como uma chamada ao banco de dados
            await Task.Delay(100);

            return account.Transactions.Where(t => t.IsForeignTransaction).Count() > 10;
        }

        // Verifica se há uma série de saques grandes em um curto período de tempo
        public async Task<bool> HasLargeWithdrawalsAsync(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            // Simula uma operação assíncrona, como uma chamada ao banco de dados
            await Task.Delay(100);

            var largeWithdrawals = account.Transactions
                .Where(t => t.Type == TransactionType.Withdrawal && t.Amount > 1000)
                .ToList();

            return largeWithdrawals.Count > 5;
        }

        // Verifica se há múltiplas transações de grande valor em um curto período de tempo
        public async Task<bool> HasMultipleLargeTransactionsAsync(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            await Task.Delay(100);

            return account.Transactions.Where(t => t.Amount > 5000).Count() > 3;
        }

        // Verifica se o saldo da conta teve flutuações significativas em um curto período de tempo
        public async Task<bool> HasRapidBalanceFluctuationAsync(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            await Task.Delay(100);

            var recentTransactions = account.Transactions
                .OrderByDescending(t => t.Date)
                .Take(10)
                .ToList();

            var totalDebit = recentTransactions.Where(t => t.Type == TransactionType.Withdrawal).Sum(t => t.Amount);
            var totalCredit = recentTransactions.Where(t => t.Type == TransactionType.Deposit).Sum(t => t.Amount);

            return Math.Abs(totalDebit - totalCredit) > 5000;
        }


        // Verifica se a conta tem transações consecutivas com valores idênticos
        public async Task<bool> HasConsecutiveIdenticalTransactionsAsync(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            await Task.Delay(100);

            var consecutiveIdenticalTransactions = account.Transactions
                .OrderBy(t => t.Date)
                .GroupBy(t => t.Amount)
                .Any(g => g.Count() > 2);

            return consecutiveIdenticalTransactions;
        }

        // Verifica se a conta tem transações frequentes durante a noite
        public async Task<bool> HasFrequentNightTimeTransactionsAsync(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            await Task.Delay(100);

            var nightTimeTransactions = account.Transactions
                .Where(t => t.Date.Hour >= 0 && t.Date.Hour <= 5)
                .Count();

            return nightTimeTransactions > 5;
        }

        // Verifica se a conta tem uma alta taxa de transações recusadas
        public async Task<bool> HasHighRateOfDeclinedTransactionsAsync(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            await Task.Delay(100);

            var totalTransactions = account.Transactions.Count();
            var declinedTransactions = account.Transactions.Count(t => !t.IsApproved);

            return (double)declinedTransactions / totalTransactions > 0.2;
        }

        // Verifica se a conta tem transações em múltiplos países em um curto período de tempo
        public async Task<bool> HasTransactionsInMultipleCountriesAsync(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            await Task.Delay(100);

            var countries = account.Transactions
                .Select(t => t.Country)
                .Distinct()
                .Count();

            return countries > 3;
        }

        // Verifica se a conta tem um aumento súbito na frequência de transações
        public async Task<bool> HasSuddenIncreaseInTransactionFrequencyAsync(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            await Task.Delay(100);

            var recentTransactions = account.Transactions
                .OrderByDescending(t => t.Date)
                .Take(10)
                .Count();

            var previousTransactions = account.Transactions
                .OrderByDescending(t => t.Date)
                .Skip(10)
                .Take(10)
                .Count();

            return recentTransactions > previousTransactions * 2;
        }
    }

}
