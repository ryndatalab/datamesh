using PortalBackend.PortalEntities.EntitiesIsoPay;
using PortalBackend.PortalEntities.Enums.IsoPay;

namespace PortalBackend.DBAccess.Models.IsoPay
{
    public record TransactionDto(Guid Id, DateTimeOffset? OnDate, Guid AccId, decimal amt, TransactionTypeId transactionTypeId);
    public record AccountDto(Guid Id, string AccNumber, IsoPay.Currency? Currency, decimal Balance,  Guid? AccountHead)
    {
        //public void SetAccountHead(Guid guid) { AccountHead = guid; }
        public static AccountHead DefaultaccountHead() => new AccountHead(Guid.Parse("5e6cf98c-9a76-4cb7-b547-858290e979e4"), "IsoPay", "EURO");
    }

    public record TransferDto(string SenderAcc, string RecieverAcc, string Currency, decimal Amt, string comment = "");

    public record AccountHeadDto(Guid Id, string Label = "IsoPay", IsoPay.Currency Currency = Currency.EURO)
    {
        public IEnumerable<AccountDto>? Accounts { get; set; }
        public AccountDto ToAccountDto(Account acc) => 
            new AccountDto(acc.Id, acc.AccNumber, this.Currency, acc.Balance, this.Id);
        public IEnumerable<AccountDto> ToAccountDtos(IEnumerable<Account> accs) =>
            accs.Select(acc => this.ToAccountDto(acc));
    }

}
