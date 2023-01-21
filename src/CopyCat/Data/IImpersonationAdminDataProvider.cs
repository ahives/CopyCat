using CopyCat.Data.Model;

namespace CopyCat.Data;

public interface IImpersonationAdminDataProvider
{
    Task<IReadOnlyList<ImpersonatedAccount>> GetAccounts(Guid accountId);

    Task<ImpersonatedAccountEntity?> GetAccount(Guid id);

    Task<IReadOnlyList<ImpersonatedAccount>> GetAllAccounts();

    Task<bool> TryCreateAccount(ImpersonatedAccount account);

    Task<bool> TryUpdateSendingClientId(ImpersonatedAccountEntity account, string sendingClientId);

    Task<bool> TryUpdateSendingFacilityId(ImpersonatedAccountEntity account, string sendingClientId);

    Task<bool> TryUpdateAccountName(ImpersonatedAccountEntity account, string name);

    Task<bool> TryActivateAccount(ImpersonatedAccountEntity account);

    Task<bool> TryDeactivateAccount(ImpersonatedAccountEntity account);
}