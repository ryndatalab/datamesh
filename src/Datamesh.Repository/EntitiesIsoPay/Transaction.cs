using System.ComponentModel.DataAnnotations;

namespace PortalBackend.PortalEntities.EntitiesIsoPay
{
    public class Transaction
    {
        private Transaction()
        {
            //Console.WriteLine($"Started acc:{acc} amt: {amt} bal: {amtBal}{currency} trasaction: {transactionType}");
            //
        }

        public Transaction(Guid id) : this()
        {
            Id = id;
            Amount = 0;
            OnDate = DateTime.UtcNow;
        }
        public Transaction(Guid id, Account accCredit, Account accDebit, string currency, decimal amount)
        {
            Id = id;
            OnDate = DateTime.UtcNow;
            AccCredit = accCredit;
            AccDebit = accDebit;
            Currency = currency;
            Amount = amount;
        }

        public Guid Id { get; private set; }

        [MaxLength(255)]
        public DateTimeOffset? OnDate { get; private set; }
        public Guid? AccCreditId { get; set; }
        public Guid? AccDebitId { get; set; }
        public virtual Account? AccCredit { get; set; }
        public virtual Account? AccDebit { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }

        //public virtual TransactionType TransactionType { get; private set; }
    }
}

