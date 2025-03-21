using Moq;
using pjCuentaBancaria.Models;
using pjCuentaBancaria.Repositories;
using pjCuentaBancaria.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestBanco.Test
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly CustomerService _customerService;

        // Constructor para inicializar las dependencias
        public CustomerServiceTests()
        {
            // Inicializa el mock del repositorio
            _mockCustomerRepository = new Mock<ICustomerRepository>();

            // Inicializa el servicio con el mock del repositorio
            _customerService = new CustomerService(_mockCustomerRepository.Object);
        }

        [Fact]
        public async Task CreateCustomerAsync_ShouldReturnCustomer_WhenCustomerIsValid()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "John Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = "Male",
                Income = 50000
            };

            _mockCustomerRepository
                .Setup(repo => repo.CreateCustomerAsync(It.IsAny<Customer>()))
                .ReturnsAsync(customer);

            // Act
            var result = await _customerService.CreateCustomerAsync(customer);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal(50000, result.Income);
        }

        [Fact]
        public async Task CreateCustomerAsync_ShouldThrowException_WhenIncomeIsNegative()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "John Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = "Male",
                Income = -1000
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _customerService.CreateCustomerAsync(customer));
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ShouldReturnCustomer_WhenCustomerExists()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                Name = "Jane Doe",
                DateOfBirth = new DateTime(1985, 5, 15),
                Gender = "Female",
                Income = 60000
            };

            _mockCustomerRepository
                .Setup(repo => repo.GetCustomerByIdAsync(1))
                .ReturnsAsync(customer);

            // Act
            var result = await _customerService.GetCustomerByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Jane Doe", result.Name);
            Assert.Equal(60000, result.Income);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            // Arrange
            _mockCustomerRepository
                .Setup(repo => repo.GetCustomerByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Customer)null);

            // Act
            var result = await _customerService.GetCustomerByIdAsync(999);

            // Assert
            Assert.Null(result);
        }
    }
}
