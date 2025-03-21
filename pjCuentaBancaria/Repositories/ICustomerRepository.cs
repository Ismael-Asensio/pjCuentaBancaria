using pjCuentaBancaria.Models;

namespace pjCuentaBancaria.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByIdAsync(int id);
    }
}
