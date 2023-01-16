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

    public ImpersonatedAccountAdminService(IImpersonationAdminDataProvider impersonationDataProvider, IAccountAdminDataProvider accountDataProvider)
    {
        _impersonationDataProvider = impersonationDataProvider;
        _accountDataProvider = accountDataProvider;
    }

    public Result<ImpersonatedAccount> CreateImpersonatedAccount(CreateImpersonatedAccountRequest request)
    {
        if (!request.IsValid())
            return new Result<ImpersonatedAccount> {IsData = false, HasFaulted = true};
        
        if (!_accountDataProvider.FindAccount(request.AccountId))
            return new Result<ImpersonatedAccount> {IsData = false, HasFaulted = true};

        var entity = new ImpersonatedAccount
        {
            Id = NewId.NextGuid(),
            Name = request.Name,
            SendingFacilityId = request.SendingFacilityId,
            SendingAppId = request.SendingAppId,
            AccountId = request.AccountId,
            IsActive = request.IsActive,
            CreatedOn = DateTimeOffset.UtcNow
        };

        bool isCreated = _impersonationDataProvider.TryCreateImpersonatedAccount(entity);

        return new Result<ImpersonatedAccount> {Data = entity, IsData = isCreated, HasFaulted = isCreated};
    }

    public Result<IReadOnlyList<ImpersonatedAccount>> GetAllImpersonatedAccounts()
    {
        var data = _impersonationDataProvider.GetImpersonatedAccounts();
        
        return new Result<IReadOnlyList<ImpersonatedAccount>> {Data = data, IsData = data.Any(), HasFaulted = false};
    }

    public Result<IReadOnlyList<ImpersonatedAccount>> GetImpersonatedAccounts(Guid accountId)
    {
        var data = _impersonationDataProvider.GetImpersonatedAccounts(accountId);
        
        return new Result<IReadOnlyList<ImpersonatedAccount>> {Data = data, IsData = data.Any(), HasFaulted = false};
    }
}