using CopyCat.Data.Model;
using CopyCat.Model;

namespace CopyCat.Services;

public interface IImpersonatedAccountAdminService
{
    Task<Result<ImpersonatedAccount>> CreateAccount(CreateImpersonatedAccountRequest request);

    Task<Result<ImpersonatedAccount>> UpdateSendingClientId(Guid id, string sendingClientId);

    Task<Result<ImpersonatedAccount>> UpdateSendingFacilityId(Guid id, string sendingFacilityId);

    Task<Result<ImpersonatedAccount>> UpdateAccountName(Guid id, string name);

    Task<Result<ImpersonatedAccount>> ActivateAccount(Guid id);

    Task<Result<ImpersonatedAccount>> DeactivateAccount(Guid id);

    Task<Result<IReadOnlyList<ImpersonatedAccount>>> GetAllAccounts();

    Task<Result<IReadOnlyList<ImpersonatedAccount>>> GetAccounts(Guid id);

    Task<Result<ImpersonatedAccount>> FindAccount(Guid id);
}