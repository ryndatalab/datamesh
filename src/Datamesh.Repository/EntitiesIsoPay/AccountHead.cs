 
using System.ComponentModel.DataAnnotations;

namespace PortalBackend.PortalEntities.EntitiesIsoPay
{
    public class AccountHead
    {
        private AccountHead()
        {
            Label = null!;
            Currency = null!;
            Accounts = new HashSet<Account>();
        }

        //public AccountHead(Guid id, string label) : this()
        //{
        //    AccountHeadId = id;
        //    Label = label.ToString();
        //}
        public AccountHead(Guid accountHeadId, string label, string currency) : this()
        {
            AccountHeadId = accountHeadId; Label = label; Currency = currency;
        }

        public Guid AccountHeadId { get; private set; }

        [MaxLength(255)]
        public string Label { get; set; }


        [MaxLength(255)]
        public string Currency { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}

