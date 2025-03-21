using pjCuentaBancaria.Models;
using pjCuentaBancaria.Repositories;
using System.Text;

namespace pjCuentaBancaria.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public BankAccountService(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<BankAccount> CreateBankAccountAsync(decimal balance, int customerId)
        {
            if (balance < 0)
                throw new ArgumentException("El saldo inicial no puede ser negativo.");

            // Generar un número de cuenta único
            string accountNumber;
            bool isUnique;
            do
            {
                accountNumber = GenerateAccountNumber();
                isUnique = await _bankAccountRepository.IsAccountNumberUniqueAsync(accountNumber);
            } while (!isUnique);

            // Crear la cuenta bancaria
            var bankAccount = new BankAccount
            {
                AccountNumber = accountNumber,
                Balance = balance,
                CustomerId = customerId
            };

            // Guardar la cuenta en la base de datos
            return await _bankAccountRepository.CreateBankAccountAsync(bankAccount);
        }

        public async Task<decimal> GetBalanceAsync(string accountNumber)
        {
            var account = await _bankAccountRepository.GetBankAccountByNumberAsync(accountNumber);
            if (account == null)
                throw new KeyNotFoundException("Account not found.");

            return account.Balance;
        }
        private string GenerateAccountNumber()
        {
            // Generar un número de cuenta numérico de 10 dígitos
            Random random = new Random();
            StringBuilder accountNumber = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                accountNumber.Append(random.Next(0, 10)); // Agrega un dígito aleatorio entre 0 y 9
            }

            return accountNumber.ToString();
        }
    }
}
