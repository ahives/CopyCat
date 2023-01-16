using CopyCat.Data;
using CopyCat.Data.Model;
using CopyCat.Extensions;

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
        List<Account> data = _provider.GetAllAccounts();
        
        return new Result<IReadOnlyList<Account>> {Data = data};
    }

    public Result TryCreateAccount(Account account)
    {
        if (!account.IsValid())
            return new Result {IsData = false, HasFaulted = true};
        
        bool isCreated = _provider.TryCreateAccount(account);

        return new Result {IsData = isCreated};
    }
}