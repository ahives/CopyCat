using CopyCat.Data.Model;
using CopyCat.Model;

namespace CopyCat.Services;

public interface IAccountService
{
    Result<IReadOnlyList<Account>> GetAllAccounts();

    Result<Account> CreateAccount(CreateAccountRequest request);
}