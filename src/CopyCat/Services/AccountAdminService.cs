using CopyCat.Data;
using CopyCat.Data.Model;
using CopyCat.Extensions;
using CopyCat.Model;
using MassTransit;

namespace CopyCat.Services;

public class AccountAdminService :
    IAccountAdminService
{
    private readonly IAccountAdminDataProvider _dataProvider;

    public AccountAdminService(IAccountAdminDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task<Result<IReadOnlyList<Account>>> GetAllAccounts()
    {
        var data = await _dataProvider.GetAllAccounts();
        
        return new Result<IReadOnlyList<Account>> {Data = data, IsData = data.Any(), HasFaulted = false};
    }

    public async Task<Result<Account>> GetAccount(Guid id)
    {
        var account = await _dataProvider.GetAccount(id);

        return account is null
            ? new Result<Account> {IsData = false, HasFaulted = true}
            : new Result<Account> {Data = account.MapTo(), IsData = true, HasFaulted = false};
    }

    public async Task<Result<Account>> CreateAccount(AccountCreationRequest request)
    {
        if (!request.IsValid())
            return new Result<Account> {IsData = false, HasFaulted = true};

        var account = new Account
        {
            Id = NewId.NextGuid(),
            Name = request.Name,
            IsActive = request.IsActive,
            CreatedOn = DateTimeOffset.UtcNow
        };
        
        bool isCreated = await _dataProvider.TryCreateAccount(account);

        return new Result<Account> {Data = account, HasFaulted = !isCreated, IsData = isCreated};
    }

    public async Task<Result<Account>> ActivateAccount(Guid id)
    {
        var entity = await _dataProvider.GetAccount(id);

        if (entity is null)
            return new Result<Account> {IsData = false, HasFaulted = true};
        
        bool isUpdated = await _dataProvider.TryActivateAccount(entity);

        return new Result<Account> {Data = entity.MapTo(), IsData = isUpdated, HasFaulted = !isUpdated};
    }

    public async Task<Result<Account>> DeactivateAccount(Guid id)
    {
        var entity = await _dataProvider.GetAccount(id);

        if (entity is null)
            return new Result<Account> {IsData = false, HasFaulted = true};
        
        bool isUpdated = await _dataProvider.TryDeactivateAccount(entity);

        return new Result<Account> {Data = entity.MapTo(), IsData = isUpdated, HasFaulted = !isUpdated};
    }

    public async Task<Result<Account>> UpdateAccountName(Guid id, string name)
    {
        var entity = await _dataProvider.GetAccount(id);

        if (entity is null)
            return new Result<Account> {IsData = false, HasFaulted = true};
        
        bool isUpdated = await _dataProvider.TryUpdateAccountName(entity, name);

        return new Result<Account> {Data = entity.MapTo(), IsData = isUpdated, HasFaulted = !isUpdated};
    }
}