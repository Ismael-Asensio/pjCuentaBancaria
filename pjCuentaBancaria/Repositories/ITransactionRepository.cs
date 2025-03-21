using pjCuentaBancaria.Models;

namespace pjCuentaBancaria.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
        Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
    }
}
