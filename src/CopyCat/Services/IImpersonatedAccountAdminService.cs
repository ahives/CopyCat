using CopyCat.Data.Model;
using CopyCat.Model;

namespace CopyCat.Services;

public interface IImpersonatedAccountAdminService
{
    Result<ImpersonatedAccount> CreateImpersonatedAccount(CreateImpersonatedAccountRequest request);

    Result<IReadOnlyList<ImpersonatedAccount>> GetAllImpersonatedAccounts();

    Result<IReadOnlyList<ImpersonatedAccount>> GetImpersonatedAccounts(Guid accountId);
}