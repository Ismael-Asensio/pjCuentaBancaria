using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pjCuentaBancaria.Models;
using pjCuentaBancaria.Services;

namespace pjCuentaBancaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            var createdCustomer = await _customerService.CreateCustomerAsync(customer);
            return Ok(createdCustomer);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }
    }
}
