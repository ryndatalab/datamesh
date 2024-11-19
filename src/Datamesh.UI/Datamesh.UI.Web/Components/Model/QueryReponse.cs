namespace Datamesh.UI.Web.Components.Model
{
    public class Data
    {
        public DbDirectQuery dbDirectQuery { get; set; }
        public string isLive { get; set; }
    }

    public class Account
    {
        public string accNumber { get; set; }
        public object accountHeadId { get; set; }
        public double balance { get; set; }
        public string id { get; set; }
    }

    public class AccountHead
    {
        public string accountHeadId { get; set; }
        public string currency { get; set; }
        public string label { get; set; }
    }

    public class Currency
    {
        public string id { get; set; }
        public string label { get; set; }
    }

    

    public class DbDirectQuery
    {
        public List<Currency> currency { get; set; }
        public List<Account> account { get; set; }
        public List<AccountHead> accountHead { get; set; }
        public List<Transaction> transaction { get; set; }
    }

    public class Root
    {
        public Data data { get; set; }
    }

    public class Transaction
    {
        public string accCreditId { get; set; }
        public string accDebitId { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public string id { get; set; }
        public DateTime onDate { get; set; }
    }

}
