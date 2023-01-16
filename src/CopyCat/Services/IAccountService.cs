using CopyCat.Data.Model;

namespace CopyCat.Services;

public interface IAccountService
{
    Result<IReadOnlyList<Account>> GetAllAccounts();

    Result TryCreateAccount(Account account);
}