using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PortalBackend.PortalEntities.EntitiesIsoPay;

namespace Datamesh.API.Query
{
    public class DBDirectQuery
    { 
        [UseFiltering]
        public IQueryable<Currency> Currency( [Service] PortalDbContext portalDbContext) =>
              portalDbContext.Currency.Select(t => t );

        [UseFiltering]
        public IQueryable<AccountHead> AccountHead([Service] PortalDbContext portalDbContext) =>
          portalDbContext.AccountHead.Select(t => t);

        [UseFiltering]
        public IQueryable<Account> Account([Service] PortalDbContext portalDbContext) =>
          portalDbContext.Account.Select(t => t);

        [UseFiltering]
        public IQueryable<Transaction> Transaction([Service] PortalDbContext portalDbContext) =>
          portalDbContext.Transaction.Select(t => t);


    }

    public class CurrencyOut : Currency {
        public CurrencyOut(Guid id): base( id)
        {
                
        }
     }
}
