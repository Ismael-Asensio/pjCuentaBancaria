using pjCuentaBancaria.Models;

namespace pjCuentaBancaria.Repositories
{
    public interface IBankAccountRepository
    {
        Task<BankAccount> CreateBankAccountAsync(BankAccount bankAccount);
        Task<BankAccount> GetBankAccountByNumberAsync(string accountNumber);
        Task<bool> IsAccountNumberUniqueAsync(string accountNumber);
        Task UpdateBankAccountAsync(BankAccount bankAccount);
    }
}
