using pjCuentaBancaria.Models;

namespace pjCuentaBancaria.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByIdAsync(int id);
    }
}
