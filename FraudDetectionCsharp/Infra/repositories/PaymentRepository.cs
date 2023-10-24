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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreatePaymentAsync(Payment payment)
        {
            if (payment == null) throw new ArgumentNullException(nameof(payment));

            // Se o Id é autoincrementado, não defina o valor manualmente
            payment.Id = 0;

            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();
        }

    }
}
