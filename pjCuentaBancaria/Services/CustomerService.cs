using pjCuentaBancaria.Models;
using pjCuentaBancaria.Repositories;

namespace pjCuentaBancaria.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            if (customer.Income < 0)
                throw new ArgumentException("Income cannot be negative.");

            return await _customerRepository.CreateCustomerAsync(customer);
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }
    }
}
