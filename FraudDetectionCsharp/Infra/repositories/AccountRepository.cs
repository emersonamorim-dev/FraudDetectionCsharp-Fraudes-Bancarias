using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Domain.interfaces;
using FraudDetectionCsharp.Infra.database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Infra.repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task CreateAccountAsync(Account account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));

            // O Id é autoincrementado, não defina o valor manualmente
            account.Id = 0;

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();

        }
        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task DeleteAccountAsync(int id)
        {
            var accountToDelete = await _context.Accounts.Include(a => a.Transactions).FirstOrDefaultAsync(a => a.Id == id);
            if (accountToDelete != null)
            {
                _context.Transaction.RemoveRange(accountToDelete.Transactions);
                _context.Accounts.Remove(accountToDelete);
                await _context.SaveChangesAsync();
            }
        }


    }

}
