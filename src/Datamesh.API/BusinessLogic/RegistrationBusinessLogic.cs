
using Datamesh.API.BusinessLogic;
using Microsoft.EntityFrameworkCore; 
using Org.Eclipse.TractusX.Portal.Backend.PortalBackend.DBAccess;
using PortalBackend.DBAccess.Models.IsoPay;
using PortalBackend.DBAccess.Repositories.IsoPay;
using PortalBackend.PortalEntities.EntitiesIsoPay;

namespace Datamesh.API.BusinessLogic;
public sealed class RegistrationBusinessLogic : IRegistrationBusinessLogic
{

    private readonly IPortalRepositories _portalRepositories;
    private readonly ILogger<RegistrationBusinessLogic> _logger;

    public RegistrationBusinessLogic(
        IPortalRepositories portalRepositories,
        ILogger<RegistrationBusinessLogic> logger)
    {
        _portalRepositories = portalRepositories;
        _logger = logger;
    }

    public async Task<AccountDto> GetAccountDtoAsync(string accNumber)
    {
        var acc = await _portalRepositories.GetInstance<IIsoPayRepository>().GetAccountAsync(accNumber).ConfigureAwait(false);
        if (acc == null)
        {
            throw new Exception($"account {accNumber} does not exist.");
        }

        return new AccountDto(acc.Id, acc.AccNumber, PortalBackend.DBAccess.Models.IsoPay.Currency.EURO, acc.Balance, acc.AccountHead.AccountHeadId);
    }

    public async Task<AccountDto> UpsertAccountAsync(AccountDto accountDto)
    {
        var isoPayRepository = _portalRepositories.GetInstance<IIsoPayRepository>();
        var acc = await isoPayRepository.GetAccountAsync(accountDto.AccNumber).ConfigureAwait(false);
        Guid  accountHeadID = accountDto.AccountHead ?? AccountDto.DefaultaccountHead().AccountHeadId;

        var accHead = await isoPayRepository.DBContext.AccountHead.Where(ah=> ah.AccountHeadId == accountHeadID).FirstAsync().ConfigureAwait(false);

        Action<Account> setOptionalParameters = (acc) =>
            {
                acc.AccNumber = accountDto.AccNumber; acc.Balance = 0; acc.AccountHeadId = accHead.AccountHeadId;
            };

        if (acc != null)
        {
            isoPayRepository.AttachAndModifyAccount(accountDto.Id, setOptionalParameters, null);
        }
        else
        {
            isoPayRepository.AttachAndModifyAccount(Guid.NewGuid(), null, setOptionalParameters);
        }

        await _portalRepositories.SaveAsync().ConfigureAwait(false);

        return accountDto;
    }

    public async Task<AccountHeadDto> GetAccountHeadDtoAsync(Guid accHeadId)
    {
        var acc = await _portalRepositories.GetInstance<IIsoPayRepository>().GetAccountHeadAsync(accHeadId).ConfigureAwait(false);
        if (acc == null)
        {
            throw new Exception($"account Head {accHeadId} does not exist.");
        }

        var accHead = new AccountHeadDto(acc.AccountHeadId, acc.Label, Enum.Parse<PortalBackend.DBAccess.Models.IsoPay.Currency>(acc.Currency));
        accHead.Accounts = accHead.ToAccountDtos(acc.Accounts);
        return accHead;
    }

    public async Task<IList<AccountHeadDto>> GetAccountHeadsAsync(string name, bool includeAccs)
    {
        var isoPayRepository = _portalRepositories.GetInstance<IIsoPayRepository>();
        var accHeads = await isoPayRepository.GetAccountHeadsAsync(name, includeAccs).ConfigureAwait(false);
 
        return accHeads;
    }
    public async Task<AccountHeadDto> UpsertAccountHeadAsync(AccountHeadDto accHeadDto)
    {
        var isoPayRepository = _portalRepositories.GetInstance<IIsoPayRepository>();
        var accHead = await isoPayRepository.GetAccountHeadAsync(accHeadDto.Id).ConfigureAwait(false);

        Action<AccountHead> setOptionalParameters = (acc) =>
        {
            acc.Currency = accHeadDto.Currency.ToString(); acc.Label = accHeadDto.Label;
        };

        if (accHead != null)
        {
            isoPayRepository.UpsertAccountHead(accHead.AccountHeadId, setOptionalParameters, null);
        }
        else
        {
            isoPayRepository.UpsertAccountHead(Guid.NewGuid(), null, setOptionalParameters);
        }

        await _portalRepositories.SaveAsync();

        return accHeadDto;
    }
     
}

