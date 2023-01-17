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

    public Result<ImpersonatedAccount> CreateAccount(CreateImpersonatedAccountRequest request)
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
            SendingAppId = request.SendingAppId,
            AccountId = request.AccountId,
            IsActive = request.IsActive,
            CreatedOn = DateTimeOffset.UtcNow
        };

        bool isCreated = _impersonationDataProvider.TryCreateAccount(account);

        return new Result<ImpersonatedAccount> {Data = account, IsData = isCreated, HasFaulted = isCreated};
    }

    public Result<ImpersonatedAccount> ActivateAccount(Guid id)
    {
        bool isUpdated = _impersonationDataProvider.TryActivateAccount(id, out ImpersonatedAccount account);

        return new Result<ImpersonatedAccount> {Data = account, IsData = isUpdated, HasFaulted = !isUpdated};
    }

    public Result<ImpersonatedAccount> DeactivateAccount(Guid id)
    {
        bool isUpdated = _impersonationDataProvider.TryDeactivateAccount(id, out ImpersonatedAccount account);

        return new Result<ImpersonatedAccount> {Data = account, IsData = isUpdated, HasFaulted = !isUpdated};
    }

    public Result<IReadOnlyList<ImpersonatedAccount>> GetAllAccounts()
    {
        var data = _impersonationDataProvider.GetAllAccounts();
        
        return new Result<IReadOnlyList<ImpersonatedAccount>> {Data = data, IsData = data.Any(), HasFaulted = false};
    }

    public Result<IReadOnlyList<ImpersonatedAccount>> GetAccounts(Guid id)
    {
        var data = _impersonationDataProvider.GetAccounts(id);
        
        return new Result<IReadOnlyList<ImpersonatedAccount>> {Data = data, IsData = data.Any(), HasFaulted = false};
    }
}