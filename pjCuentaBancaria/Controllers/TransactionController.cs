using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pjCuentaBancaria.Repositories;
using pjCuentaBancaria.Services;

namespace pjCuentaBancaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IBankAccountRepository _bankAccountRepository;

        public TransactionController(ITransactionService transactionService, IBankAccountRepository bankAccountRepository)
        {
            _transactionService = transactionService;
            _bankAccountRepository = bankAccountRepository;
        }

        [HttpPost("deposito")]
        public async Task<IActionResult> Deposit([FromBody] DepositWithdrawRequest request)
        {
            var transaction = await _transactionService.DepositAsync(request.AccountNumber, request.Amount);

            // Obtener el saldo actual de la cuenta
            var account = await _bankAccountRepository.GetBankAccountByNumberAsync(request.AccountNumber);
            decimal currentBalance = account?.Balance ?? 0;

            // Respuesta limpia con saldo actual
            var response = new
            {
                transaction.Id,
                transaction.Type,
                transaction.Amount,
                transaction.TransactionDate,
                transaction.BankAccountId,
                CurrentBalance = currentBalance // Saldo actual
            };

            return Ok(response);
        }

        [HttpPost("retiro")]
        public async Task<IActionResult> Withdraw([FromBody] DepositWithdrawRequest request)
        {
            var transaction = await _transactionService.WithdrawAsync(request.AccountNumber, request.Amount);

            // Obtener el saldo actual de la cuenta
            var account = await _bankAccountRepository.GetBankAccountByNumberAsync(request.AccountNumber);
            decimal currentBalance = account?.Balance ?? 0;

            // Respuesta limpia con saldo actual
            var response = new
            {
                transaction.Id,
                transaction.Type,
                transaction.Amount,
                transaction.TransactionDate,
                transaction.BankAccountId,
                CurrentBalance = currentBalance // Saldo actual
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
