using PortalBackend.PortalEntities.EntitiesIsoPay;

namespace Datamesh.API.Mutation
{
    public class MasterMutation
    {
        public async Task<MutationResult> Currency([Service] PortalDbContext portalDbContext, string label)
        {
            Currency entity = new Currency(Guid.NewGuid()) { Label = label };
            portalDbContext.Currency.Add(entity: entity);
            await portalDbContext.SaveChangesAsync();

            return new MutationResult() { Success = true, Message = "added", ResultId = entity.Id.ToString() };
        }

        public async Task<MutationResult> Account([Service] PortalDbContext portalDbContext, Account account)
        {
            Account entity = account;
            portalDbContext.Account.Add(entity: account);
            await portalDbContext.SaveChangesAsync();

            return new MutationResult() { Success = true, Message = "added", ResultId = entity.Id.ToString() };
        }

        public async Task<MutationResult> AccountHead([Service] PortalDbContext portalDbContext, string label, string currency)
        {
            AccountHead entity = new  AccountHead(
                 Guid.NewGuid(), label, currency );
            portalDbContext.AccountHead.Add(entity: entity);
            await portalDbContext.SaveChangesAsync();

            return new MutationResult() { Success = true, Message = "added", ResultId = entity.AccountHeadId.ToString() };
        }
    }
}
