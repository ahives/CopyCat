using CopyCat.Data.Model;
using CopyCat.Model;

namespace CopyCat.Services;

public interface IImpersonatedAccountAdminService
{
    Result<ImpersonatedAccount> CreateAccount(CreateImpersonatedAccountRequest request);

    Result<ImpersonatedAccount> UpdateSendingClientId(Guid id, string sendingClientId);

    Result<ImpersonatedAccount> UpdateSendingFacilityId(Guid id, string sendingFacilityId);

    Result<ImpersonatedAccount> UpdateAccountName(Guid id, string name);

    Result<ImpersonatedAccount> ActivateAccount(Guid id);

    Result<ImpersonatedAccount> DeactivateAccount(Guid id);

    Result<IReadOnlyList<ImpersonatedAccount>> GetAllAccounts();

    Result<IReadOnlyList<ImpersonatedAccount>> GetAccounts(Guid id);
}