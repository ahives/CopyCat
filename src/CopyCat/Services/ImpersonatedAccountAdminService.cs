using CopyCat.Data;
using CopyCat.Data.Model;
using CopyCat.Extensions;
using CopyCat.Model;
using MassTransit;

namespace CopyCat.Services;

public class ImpersonatedAccountAdminService :
    IImpersonatedAccountAdminService
{
    private readonly IImpersonationAdminProvider _dataProvider;

    public ImpersonatedAccountAdminService(IImpersonationAdminProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public Result<ImpersonatedAccount> CreateImpersonatedAccount(CreateImpersonatedAccountRequest request)
    {
        if (!request.IsValid())
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

        bool isCreated = _dataProvider.TryCreateImpersonatedAccount(entity);

        return new Result<ImpersonatedAccount> {Data = entity, IsData = isCreated, HasFaulted = isCreated};
    }

    public Result<IReadOnlyList<ImpersonatedAccount>> GetAllImpersonatedAccounts()
    {
        var data = _dataProvider.GetImpersonatedAccounts();
        
        return new Result<IReadOnlyList<ImpersonatedAccount>> {Data = data, IsData = data.Any(), HasFaulted = false};
    }

    public Result<IReadOnlyList<ImpersonatedAccount>> GetImpersonatedAccounts(Guid accountId)
    {
        var data = _dataProvider.GetImpersonatedAccounts(accountId);
        
        return new Result<IReadOnlyList<ImpersonatedAccount>> {Data = data, IsData = data.Any(), HasFaulted = false};
    }
}