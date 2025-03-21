using pjCuentaBancaria.Models;
using pjCuentaBancaria.Repositories;

namespace pjCuentaBancaria.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IBankAccountRepository bankAccountRepository, ITransactionRepository transactionRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> DepositAsync(string accountNumber, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("El Deposito debe ser positivo.");

            var account = await _bankAccountRepository.GetBankAccountByNumberAsync(accountNumber);
            if (account == null)
                throw new KeyNotFoundException("Cuenta no encontrada.");

            account.Balance += amount;
            await _bankAccountRepository.UpdateBankAccountAsync(account);

            var transaction = new Transaction
            {
                Type = "Deposito",
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                BankAccountId = account.Id
            };

            return await _transactionRepository.CreateTransactionAsync(transaction);
        }

        public async Task<List<Transaction>> GetTransactionHistoryAsync(string accountNumber)
        {
            var account = await _bankAccountRepository.GetBankAccountByNumberAsync(accountNumber);
            if (account == null)
                throw new KeyNotFoundException("Account not found.");

            return await _transactionRepository.GetTransactionsByAccountIdAsync(account.Id);
        }

        public async Task<Transaction> WithdrawAsync(string accountNumber, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("El Retiro debe hacerse de un Saldo Positivo.");

            var account = await _bankAccountRepository.GetBankAccountByNumberAsync(accountNumber);
            if (account == null)
                throw new KeyNotFoundException("Cuenta no encontrada.");

            if (account.Balance < amount)
                throw new InvalidOperationException("Saldo Insuficiente.");

            account.Balance -= amount;
            await _bankAccountRepository.UpdateBankAccountAsync(account);

            var transaction = new Transaction
            {
                Type = "Retiro",
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                BankAccountId = account.Id
            };

            return await _transactionRepository.CreateTransactionAsync(transaction);
        }
    }
}
