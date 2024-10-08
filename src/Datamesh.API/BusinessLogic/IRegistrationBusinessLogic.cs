using PortalBackend.DBAccess.Models.IsoPay;

namespace Datamesh.API.BusinessLogic
{
    public interface IRegistrationBusinessLogic
    {
        Task<AccountDto> UpsertAccountAsync(AccountDto accountDto);
        Task<AccountHeadDto> UpsertAccountHeadAsync(AccountHeadDto accHeadDto);
        Task<AccountDto> GetAccountDtoAsync(string accNumber);
        Task<AccountHeadDto> GetAccountHeadDtoAsync(Guid accHeadId);
        Task<IList<AccountHeadDto>> GetAccountHeadsAsync(string name, bool includeAccs);
    }
}
