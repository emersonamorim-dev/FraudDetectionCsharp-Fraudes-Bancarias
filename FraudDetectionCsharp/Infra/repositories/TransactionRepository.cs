using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Domain.interfaces;
using FraudDetectionCsharp.Infra.database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Infra.repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;


        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            // O Id é autoincrementado, não defina o valor manualmente
            transaction.Id = 0;

            _context.Transaction.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetTransactionCountForUserOnDateAsync(int userId, DateTime date)
        {
            // Obtendo a data de início e fim para garantir que estamos pegando todas as transações
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            // Consulta o banco de dados para contar as transações do usuário na data especificada
            return await _context.Transaction
                .Where(t => t.UserId == userId && t.Date >= startDate && t.Date < endDate)
                .CountAsync();
        }


    }
}
