using CopyCat.Data.Model;
using CopyCat.Model;

namespace CopyCat.Services;

public interface IAccountAdminService
{
    Task<Result<IReadOnlyList<Account>>> GetAllAccounts();

    Task<Result<Account>> GetAccount(Guid id);

    Task<Result<Account>> CreateAccount(AccountCreationRequest request);

    Task<Result<Account>> ActivateAccount(Guid id);

    Task<Result<Account>> DeactivateAccount(Guid id);
    
    Task<Result<Account>> UpdateAccountName(Guid id, string name);
}