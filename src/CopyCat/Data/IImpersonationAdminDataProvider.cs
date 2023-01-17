using CopyCat.Data.Model;

namespace CopyCat.Data;

public interface IImpersonationAdminDataProvider
{
    List<ImpersonatedAccount> GetAccounts(Guid accountId);

    List<ImpersonatedAccount> GetAllAccounts();

    bool TryCreateAccount(ImpersonatedAccount account);

    bool TryActivateAccount(Guid id, out ImpersonatedAccount account);

    bool TryDeactivateAccount(Guid id, out ImpersonatedAccount account);
}