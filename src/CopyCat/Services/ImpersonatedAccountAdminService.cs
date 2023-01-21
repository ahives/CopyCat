using CopyCat.Data;
using CopyCat.Data.Model;
using CopyCat.Extensions;
using CopyCat.Model;
using MassTransit;

namespace CopyCat.Services;

public class ImpersonatedAccountAdminService :
    IImpersonatedAccountAdminService
{
    private readonly IImpersonationAdminDataProvider _impersonationDataProvider;
    private readonly IAccountAdminDataProvider _accountDataProvider;

    public ImpersonatedAccountAdminService(
        IImpersonationAdminDataProvider impersonationDataProvider,
        IAccountAdminDataProvider accountDataProvider)
    {
        _impersonationDataProvider = impersonationDataProvider;
        _accountDataProvider = accountDataProvider;
    }

    public async Task<Result<ImpersonatedAccount>> CreateAccount(CreateImpersonatedAccountRequest request)
    {
        if (!request.IsValid())
            return new Result<ImpersonatedAccount> {IsData = false, HasFaulted = true};
        
        if (!_accountDataProvider.FindAccount(request.AccountId))
            return new Result<ImpersonatedAccount> {IsData = false, HasFaulted = true};

        var account = new ImpersonatedAccount
        {
            Id = NewId.NextGuid(),
            Name = request.Name,
            SendingFacilityId = request.SendingFacilityId,
            SendingClientId = request.SendingClientId,
            AccountId = request.AccountId,
            IsActive = request.IsActive,
            CreatedOn = DateTimeOffset.UtcNow
        };

        bool isCreated = await _impersonationDataProvider.TryCreateAccount(account);

        return new Result<ImpersonatedAccount> {Data = account, IsData = isCreated, HasFaulted = !isCreated};
    }

    public async Task<Result<ImpersonatedAccount>> UpdateSendingClientId(Guid id, string sendingClientId)
    {
        var entity = await _impersonationDataProvider.GetAccount(id);

        if (entity is null)
            return new Result<ImpersonatedAccount> {IsData = false, HasFaulted = true};
        
        bool isUpdated = await _impersonationDataProvider.TryUpdateSendingClientId(entity, sendingClientId);

        return new Result<ImpersonatedAccount> {Data = entity.MapTo(), IsData = isUpdated, HasFaulted = !isUpdated};
    }

    public async Task<Result<ImpersonatedAccount>> UpdateSendingFacilityId(Guid id, string sendingFacilityId)
    {
        var entity = await _impersonationDataProvider.GetAccount(id);

        if (entity is null)
            return new Result<ImpersonatedAccount> {IsData = false, HasFaulted = true};
        
        bool isUpdated = await _impersonationDataProvider.TryUpdateSendingFacilityId(entity, sendingFacilityId);

        return new Result<ImpersonatedAccount> {Data = entity.MapTo(), IsData = isUpdated, HasFaulted = !isUpdated};
    }

    public async Task<Result<ImpersonatedAccount>> UpdateAccountName(Guid id, string name)
    {
        var entity = await _impersonationDataProvider.GetAccount(id);

        if (entity is null)
            return new Result<ImpersonatedAccount> {IsData = false, HasFaulted = true};
        
        bool isUpdated = await _impersonationDataProvider.TryUpdateAccountName(entity, name);
        
        return new Result<ImpersonatedAccount> {Data = entity.MapTo(), IsData = isUpdated, HasFaulted = !isUpdated};
    }

    public async Task<Result<ImpersonatedAccount>> ActivateAccount(Guid id)
    {
        var entity = await _impersonationDataProvider.GetAccount(id);

        if (entity is null)
            return new Result<ImpersonatedAccount> {IsData = false, HasFaulted = true};
        
        bool isUpdated = await _impersonationDataProvider.TryActivateAccount(entity);

        return new Result<ImpersonatedAccount> {Data = entity.MapTo(), IsData = isUpdated, HasFaulted = !isUpdated};
    }

    public async Task<Result<ImpersonatedAccount>> DeactivateAccount(Guid id)
    {
        var entity = await _impersonationDataProvider.GetAccount(id);

        if (entity is null)
            return new Result<ImpersonatedAccount> {IsData = false, HasFaulted = true};
        
        bool isUpdated = await _impersonationDataProvider.TryDeactivateAccount(entity);

        return new Result<ImpersonatedAccount> {Data = entity.MapTo(), IsData = isUpdated, HasFaulted = !isUpdated};
    }

    public async Task<Result<IReadOnlyList<ImpersonatedAccount>>> GetAllAccounts()
    {
        var data = await _impersonationDataProvider.GetAllAccounts();
        
        return new Result<IReadOnlyList<ImpersonatedAccount>> {Data = data, IsData = data.Any(), HasFaulted = false};
    }

    public async Task<Result<IReadOnlyList<ImpersonatedAccount>>> GetAccounts(Guid id)
    {
        var data = await _impersonationDataProvider.GetAccounts(id);
        
        return new Result<IReadOnlyList<ImpersonatedAccount>> {Data = data, IsData = data.Any(), HasFaulted = false};
    }

    public Task<Result<ImpersonatedAccount>> FindAccount(Guid id)
    {
        throw new NotImplementedException();
    }
}