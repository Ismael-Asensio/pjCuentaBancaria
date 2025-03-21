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
    public class TransactionServiceTests
    {
        private readonly Mock<IBankAccountRepository> _mockBankAccountRepository;
        private readonly Mock<ITransactionRepository> _mockTransactionRepository;
        private readonly TransactionService _transactionService;

        public TransactionServiceTests()
        {
            // Inicializa los mocks de los repositorios
            _mockBankAccountRepository = new Mock<IBankAccountRepository>();
            _mockTransactionRepository = new Mock<ITransactionRepository>();

            // Inicializa el servicio con los mocks de los repositorios
            _transactionService = new TransactionService(_mockBankAccountRepository.Object, _mockTransactionRepository.Object);
        }

        [Fact]
        public async Task DepositAsync_ShouldReturnTransaction_WhenDepositIsSuccessful()
        {
            // Arrange
            var bankAccount = new BankAccount
            {
                Id = 1,
                AccountNumber = "123456",
                Balance = 1000
            };

            _mockBankAccountRepository
                .Setup(repo => repo.GetBankAccountByNumberAsync("123456"))
                .ReturnsAsync(bankAccount);

            _mockTransactionRepository
                .Setup(repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()))
                .ReturnsAsync(new Transaction
                {
                    Id = 1,
                    Type = "Deposito",
                    Amount = 500,
                    TransactionDate = DateTime.UtcNow
                });

            // Act
            var result = await _transactionService.DepositAsync("123456", 500);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Deposito", result.Type);
            Assert.Equal(500, result.Amount);
        }

        [Fact]
        public async Task DepositAsync_ShouldThrowException_WhenAmountIsNegative()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _transactionService.DepositAsync("123456", -100));
        }

        [Fact]
        public async Task WithdrawAsync_ShouldReturnTransaction_WhenWithdrawalIsSuccessful()
        {
            // Arrange
            var bankAccount = new BankAccount
            {
                Id = 1,
                AccountNumber = "123456",
                Balance = 1000
            };

            _mockBankAccountRepository
                .Setup(repo => repo.GetBankAccountByNumberAsync("123456"))
                .ReturnsAsync(bankAccount);

            _mockTransactionRepository
                .Setup(repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()))
                .ReturnsAsync(new Transaction
                {
                    Id = 1,
                    Type = "Retiro",
                    Amount = 500,
                    TransactionDate = DateTime.UtcNow
                });

            // Act
            var result = await _transactionService.WithdrawAsync("123456", 500);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Retiro", result.Type);
            Assert.Equal(500, result.Amount);
        }

        [Fact]
        public async Task WithdrawAsync_ShouldThrowException_WhenInsufficientFunds()
        {
            // Arrange
            var bankAccount = new BankAccount
            {
                Id = 1,
                AccountNumber = "123456",
                Balance = 100
            };

            _mockBankAccountRepository
                .Setup(repo => repo.GetBankAccountByNumberAsync("123456"))
                .ReturnsAsync(bankAccount);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _transactionService.WithdrawAsync("123456", 500));
        }
    }
}
