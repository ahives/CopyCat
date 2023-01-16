using CopyCat.Data;
using CopyCat.Data.Model;
using CopyCat.Extensions;
using CopyCat.Model;
using MassTransit;

namespace CopyCat.Services;

public class AccountService :
    IAccountService
{
    private readonly IAccountAdminProvider _provider;

    public AccountService(IAccountAdminProvider provider)
    {
        _provider = provider;
    }

    public Result<IReadOnlyList<Account>> GetAllAccounts()
    {
        var data = _provider.GetAllAccounts();
        
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
        
        bool isCreated = _provider.TryCreateAccount(account);

        return new Result<Account> {Data = account, HasFaulted = !isCreated, IsData = isCreated};
    }
}