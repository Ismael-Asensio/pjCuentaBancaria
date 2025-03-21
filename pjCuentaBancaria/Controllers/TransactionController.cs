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
        public async Task<IActionResult> Deposit([FromBody] TransactionRequest request)
        {
            var transaction = await _transactionService.DepositAsync(request.AccountNumber, request.Amount);
            return Ok(transaction);
        }

        [HttpPost("Retiro")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionRequest request)
        {
            var transaction = await _transactionService.WithdrawAsync(request.AccountNumber, request.Amount);
            return Ok(transaction);
        }

        [HttpGet("{accountNumber}/history")]
        public async Task<IActionResult> GetTransactionHistory(string accountNumber)
        {
            var history = await _transactionService.GetTransactionHistoryAsync(accountNumber);
            return Ok(history);
        }
    }

    public class TransactionRequest
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
