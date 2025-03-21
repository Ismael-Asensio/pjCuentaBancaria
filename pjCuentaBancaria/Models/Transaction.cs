namespace pjCuentaBancaria.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Type { get; set; } // "Deposito" o "Retiro"
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
