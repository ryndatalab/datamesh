using Microsoft.EntityFrameworkCore;  
using PortalBackend.DBAccess.Models.IsoPay;
using PortalBackend.PortalEntities.EntitiesIsoPay; 

namespace PortalBackend.DBAccess.Repositories.IsoPay;

/// Implementation of <see cref="IOfferRepository"/> accessing database with EF Core.
public class IsoPayRepository : IIsoPayRepository
{
    private readonly PortalDbContext _context;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="portalDbContext">PortalDb context.</param>
    public IsoPayRepository(PortalDbContext portalDbContext)
    {
        _context = portalDbContext;
        DBContext = _context;
    }

    public PortalDbContext DBContext { get; set; }

    ///<inheritdoc/>
    public async Task<Account> GetAccountAsync(string accNumber) =>
        await _context.Account.AsNoTracking().Where(o => o.AccNumber == accNumber).Include(x => x.AccountHead)
        .SingleOrDefaultAsync();

    public async Task<AccountHead> GetAccountHeadAsync(Guid accHeadId) =>
        await _context.AccountHead.AsNoTracking()
            .Where(o => o.AccountHeadId == accHeadId)
             .SingleOrDefaultAsync();

    public async Task<List<AccountHeadDto>> GetAccountHeadsAsync(string label, bool includeAccs)
    {
        label = label ?? string.Empty;
        return await _context.AccountHead.AsNoTracking()
           .Where(o => o.Label == label || label == "").Include(ah => ah.Accounts)
           .Select(acc => new AccountHeadDto(acc.AccountHeadId, acc.Label, Enum.Parse<Models.IsoPay.Currency>(acc.Currency))
           {
               Accounts = acc.Accounts.Select(a => new AccountDto(a.Id, a.AccNumber, null , a.Balance, null))
           }
           ).ToListAsync();
    }

    public async Task<List<AccountDto>> GetAccountsAsync(string accNumberSender, string accNumberReciever) => await
      _context.Account.AsNoTracking()
          .Where(o => o.AccNumber == accNumberSender || o.AccNumber == accNumberReciever)
        .Select(a =>
          new AccountDto(a.Id, a.AccNumber, Models.IsoPay.Currency.EURO, a.Balance, null)

          ).ToListAsync();

    public Task<AccountHead> IsoPayAccountHeadAsync() =>
    _context.AccountHead.AsNoTracking().SingleOrDefaultAsync();

    /// <inheritdoc /> 
    public void AttachAndModifyAccount(Guid id, Action<Account> setOptionalParameters = null, Action<Account> initializeParemeters = null)
    {
        if (initializeParemeters != null)
        {
            var entity = new Account(id);
            entity = _context.Account.Add(entity).Entity;
            initializeParemeters?.Invoke(entity);

        }
        if (setOptionalParameters != null)
        {
            var acc = _context.Account.Attach(new Account(id)).Entity;
            setOptionalParameters?.Invoke(acc);
        }
    }
    public void UpdateBalance(Guid id, decimal bal)
    {
        var acc = _context.Account.Attach(new Account(id)).Entity;
        acc.Balance = bal;
    }

    public void RecordTransaction(AccountDto senderAccDetail, AccountDto recieverAcc, TransferDto transferDto)
    {
        Transaction transaction = _context.Transaction.Add(new Transaction(Guid.NewGuid())).Entity;
        transaction.AccCreditId = recieverAcc.Id;
        transaction.AccDebitId = senderAccDetail.Id;
        transaction.Amount = transferDto.Amt;
        transaction.Currency = "EURO";
    }

    public void AccountHeadAttachAndModify(Guid id, Action<AccountHead> setOptionalParameters = null, Action<AccountHead> initializeParemeters = null)
    {
        var entity = new AccountHead(id, default, default);
        if (initializeParemeters != null)
        {
            entity = _context.AccountHead.Add(entity).Entity;
            initializeParemeters?.Invoke(entity);

        }
        //if (setOptionalParameters != null)
        //{
        //    var offer = _context.Account.Attach(entity).Entity;
        //    setOptionalParameters?.Invoke(offer);
        //}
    }

    public void UpsertAccountHead(Guid id, Action<AccountHead> setOptionalParameters = null, Action<AccountHead> initializeParemeters = null)
    {
        if (initializeParemeters != null)
        {
            var entity = new AccountHead(id, "", "Euro");
            entity = _context.AccountHead.Add(entity).Entity;
            initializeParemeters?.Invoke(entity);

        }
        if (setOptionalParameters != null)
        {
            var acc = _context.AccountHead.Attach(new AccountHead(id, "", "Euro")).Entity;
            setOptionalParameters?.Invoke(acc);
        }
    }
}


