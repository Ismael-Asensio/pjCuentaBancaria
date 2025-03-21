using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pjCuentaBancaria.Services;

namespace pjCuentaBancaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("deposito")]
        public async Task<IActionResult> Deposit([FromBody] DepositWithdrawRequest request)
        {
            var transaction = await _transactionService.DepositAsync(request.AccountNumber, request.Amount);

            // Respuesta limpia
            var response = new
            {
                transaction.Id,
                transaction.Type,
                transaction.Amount,
                transaction.TransactionDate,
                transaction.BankAccountId
            };

            return Ok(response);
        }

        [HttpPost("retiro")]
        public async Task<IActionResult> Withdraw([FromBody] DepositWithdrawRequest request)
        {
            var transaction = await _transactionService.WithdrawAsync(request.AccountNumber, request.Amount);

            // Respuesta limpia
            var response = new
            {
                transaction.Id,
                transaction.Type,
                transaction.Amount,
                transaction.TransactionDate,
                transaction.BankAccountId
            };

            return Ok(response);
        }

        [HttpGet("{accountNumber}/historial")]
        public async Task<IActionResult> GetTransactionHistory(string accountNumber)
        {
            var transactions = await _transactionService.GetTransactionHistoryAsync(accountNumber);

            // Respuesta limpia
            var response = transactions.Select(t => new
            {
                t.Id,
                t.Type,
                t.Amount,
                t.TransactionDate,
                AccountNumber = accountNumber
            }).ToList();

            return Ok(response);
        }
    }

    // Modelo para depósitos y retiros
    public class DepositWithdrawRequest
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
