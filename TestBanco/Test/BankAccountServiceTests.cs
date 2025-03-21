using Moq;
using pjCuentaBancaria.Models;
using pjCuentaBancaria.Repositories;
using pjCuentaBancaria.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBanco.Test
{
    public class BankAccountServiceTests
    {
        private readonly Mock<IBankAccountRepository> _mockBankAccountRepository;
        private readonly BankAccountService _bankAccountService;
        public BankAccountServiceTests()
        {
            // Inicializa el mock del repositorio
            _mockBankAccountRepository = new Mock<IBankAccountRepository>();

            // Inicializa el servicio con el mock del repositorio
            _bankAccountService = new BankAccountService(_mockBankAccountRepository.Object);
        }


        [Fact]
        public async Task CreateBankAccountAsync_ShouldReturnBankAccount_WhenAccountIsValid()
        {
            // Arrange
            decimal balance = 1000;
            int customerId = 1;

            // Simular que el número de cuenta es único
            _mockBankAccountRepository
                .Setup(repo => repo.IsAccountNumberUniqueAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            // Simular la creación de la cuenta
            var bankAccount = new BankAccount
            {
                AccountNumber = "3012312312", // Número de cuenta generado automáticamente
                Balance = balance,
                CustomerId = customerId
            };

            _mockBankAccountRepository
                .Setup(repo => repo.CreateBankAccountAsync(It.IsAny<BankAccount>()))
                .ReturnsAsync(bankAccount);

            // Act
            var result = await _bankAccountService.CreateBankAccountAsync(balance, customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("3012312312", result.AccountNumber); // Verificar el número de cuenta generado
            Assert.Equal(1000, result.Balance);
            Assert.Equal(1, result.CustomerId);
        }

        [Fact]
        public async Task CreateBankAccountAsync_ShouldThrowException_WhenBalanceIsNegative()
        {
            // Arrange
            decimal balance = -500; // Saldo negativo
            int customerId = 1;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _bankAccountService.CreateBankAccountAsync(balance, customerId));
        }

        [Fact]
        public async Task CreateBankAccountAsync_ShouldGenerateUniqueAccountNumber()
        {
            // Arrange
            decimal balance = 1000;
            int customerId = 1;

            // Simular que el primer número de cuenta no es único, pero el segundo sí
            _mockBankAccountRepository
                .SetupSequence(repo => repo.IsAccountNumberUniqueAsync(It.IsAny<string>()))
                .ReturnsAsync(false) // Primer número no es único
                .ReturnsAsync(true); // Segundo número es único

            // Simular la creación de la cuenta
            var bankAccount = new BankAccount
            {
                AccountNumber = "3012312312", // Número de cuenta generado automáticamente
                Balance = balance,
                CustomerId = customerId
            };

            _mockBankAccountRepository
                .Setup(repo => repo.CreateBankAccountAsync(It.IsAny<BankAccount>()))
                .ReturnsAsync(bankAccount);

            // Act
            var result = await _bankAccountService.CreateBankAccountAsync(balance, customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("3012312312", result.AccountNumber); // Verificar el número de cuenta generado
        }

    }
}
