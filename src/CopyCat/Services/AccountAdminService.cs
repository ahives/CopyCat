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

    public Result<IReadOnlyList<Account>> GetAllAccounts()
    {
        var data = _dataProvider.GetAllAccounts();
        
        return new Result<IReadOnlyList<Account>> {Data = data, IsData = data.Any(), HasFaulted = false};
    }

    public Result<Account> CreateAccount(CreateAccountRequest request)
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
        
        bool isCreated = _dataProvider.TryCreateAccount(account);

        return new Result<Account> {Data = account, HasFaulted = !isCreated, IsData = isCreated};
    }

    public Result<Account> ActivateAccount(Guid id)
    {
        bool isUpdated = _dataProvider.TryActivateAccount(id, out Account account);

        return new Result<Account> {Data = account, IsData = isUpdated, HasFaulted = !isUpdated};
    }

    public Result<Account> DeactivateAccount(Guid id)
    {
        bool isUpdated = _dataProvider.TryDeactivateAccount(id, out Account account, out IReadOnlyList<ImpersonatedAccount> impersonatedAccounts);

        return new Result<Account> {Data = account, IsData = isUpdated, HasFaulted = !isUpdated};
    }
}