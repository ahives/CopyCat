using CopyCat.Data.Model;
using CopyCat.Model;

namespace CopyCat.Services;

public interface IAccountAdminService
{
    Result<IReadOnlyList<Account>> GetAllAccounts();

    Result<Account> CreateAccount(CreateAccountRequest request);
}