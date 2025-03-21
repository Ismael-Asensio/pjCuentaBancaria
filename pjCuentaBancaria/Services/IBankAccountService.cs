using pjCuentaBancaria.Models;

namespace pjCuentaBancaria.Services
{
    public interface IBankAccountService
    {
        Task<BankAccount> CreateBankAccountAsync(decimal balance, int customerId);
        Task<decimal> GetBalanceAsync(string accountNumber);
    }
}
