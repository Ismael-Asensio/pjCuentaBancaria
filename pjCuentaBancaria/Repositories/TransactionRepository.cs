using Microsoft.EntityFrameworkCore;
using pjCuentaBancaria.Data;
using System.Transactions;

namespace pjCuentaBancaria.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Models.Transaction> CreateTransactionAsync(Models.Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<List<Models.Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _context.Transactions
                .Where(t => t.BankAccountId == accountId)
                .ToListAsync();
        }
    }
}
