using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pjCuentaBancaria.Models;
using pjCuentaBancaria.Services;

namespace pjCuentaBancaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;

        public BankAccountController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CreateBankAccount([FromBody] decimal balance, [FromQuery] int customerId)
        {
            try
            {
                var account = await _bankAccountService.CreateBankAccountAsync(balance, customerId);

                // Respuesta limpia usando solo los campos necesarios
                var response = new
                {
                    account.AccountNumber,
                    account.Balance,
                    account.CustomerId
                };

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{accountNumber}/balance")]
        public async Task<IActionResult> GetBalance(string accountNumber)
        {
            try
            {
                var balance = await _bankAccountService.GetBalanceAsync(accountNumber);
                return Ok(new { Balance = balance });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
