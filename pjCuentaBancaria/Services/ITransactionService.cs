using pjCuentaBancaria.Models;

namespace pjCuentaBancaria.Services
{
    public interface ITransactionService
    {
        Task<Transaction> DepositAsync(string accountNumber, decimal amount);
        Task<Transaction> WithdrawAsync(string accountNumber, decimal amount);
        Task<List<Transaction>> GetTransactionHistoryAsync(string accountNumber);
    }
}
