using System.ComponentModel.DataAnnotations;

namespace PortalBackend.PortalEntities.EntitiesIsoPay
{
    public class Account  
    {
        private Account()
        {
            AccNumber = null!;
            Balance = 0;
            AccountHead = null!;
        }
        public Account(Guid id) : this()
        {
            Id = id; 
        }
         
        public Guid Id { get; private set; }

        [StringLength(36)]
        public string AccNumber { get;  set; }

        public decimal Balance { get;  set; }
        
        public Guid? AccountHeadId { get; set; }

        public virtual AccountHead? AccountHead { get; private set; }

    }
}
