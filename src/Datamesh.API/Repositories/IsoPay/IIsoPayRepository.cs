
using PortalBackend.DBAccess.Models.IsoPay;
using PortalBackend.PortalEntities.EntitiesIsoPay; 

namespace PortalBackend.DBAccess.Repositories.IsoPay;

/// <summary>
/// Repository for accessing apps on persistence layer.
/// </summary>
public interface IIsoPayRepository
{
    void AccountHeadAttachAndModify(Guid id, Action<AccountHead> setOptionalParameters = null, Action<AccountHead> initializeParemeters = null);
    void AttachAndModifyAccount(Guid id, Action<Account> setOptionalParameters=null, Action<Account> initializeParemeters = null);
    void UpsertAccountHead(Guid id, Action<AccountHead> setOptionalParameters = null, Action<AccountHead> initializeParemeters = null);

    //void CurrencyAttachAndModify(Guid id, Action<Currency> setOptionalParameters = null, Action<Currency> initializeParemeters = null);
    Task<Account> GetAccountAsync(string accNumber);
    Task<AccountHead> GetAccountHeadAsync(Guid accHeadId);
    Task<List<AccountDto>> GetAccountsAsync(string accNumberSender, string accNumberReciever);
    Task<AccountHead> IsoPayAccountHeadAsync();
    void RecordTransaction(AccountDto senderAccDetail, AccountDto recieverAcc, TransferDto transferDto);
    void UpdateBalance(Guid id, decimal amt);
    Task<List<AccountHeadDto>> GetAccountHeadsAsync(string label, bool includeAccs);
    public PortalDbContext DBContext { get; set; }

}
