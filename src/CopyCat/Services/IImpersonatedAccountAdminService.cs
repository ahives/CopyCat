using CopyCat.Data.Model;
using CopyCat.Model;

namespace CopyCat.Services;

public interface IImpersonatedAccountAdminService
{
    Result<ImpersonatedAccount> CreateAccount(CreateImpersonatedAccountRequest request);
    
    Result<ImpersonatedAccount> ActivateAccount(Guid id);
    
    Result<ImpersonatedAccount> DeactivateAccount(Guid id);

    Result<IReadOnlyList<ImpersonatedAccount>> GetAllAccounts();

    Result<IReadOnlyList<ImpersonatedAccount>> GetAccounts(Guid id);
}