using Microsoft.EntityFrameworkCore;
using pjCuentaBancaria.Data;
using pjCuentaBancaria.Models;

namespace pjCuentaBancaria.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public BankAccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BankAccount> CreateBankAccountAsync(BankAccount bankAccount)
        {
            _context.BankAccounts.Add(bankAccount);
            await _context.SaveChangesAsync();
            return bankAccount;
        }

        public async Task<BankAccount> GetBankAccountByNumberAsync(string accountNumber)
        {
            return await _context.BankAccounts
                .Include(ba => ba.Customer)
                .FirstOrDefaultAsync(ba => ba.AccountNumber == accountNumber);
        }

        public async Task UpdateBankAccountAsync(BankAccount bankAccount)
        {
            _context.BankAccounts.Update(bankAccount);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsAccountNumberUniqueAsync(string accountNumber)
        {
            return !await _context.BankAccounts
                .AnyAsync(ba => ba.AccountNumber == accountNumber);
        }
    }
}
