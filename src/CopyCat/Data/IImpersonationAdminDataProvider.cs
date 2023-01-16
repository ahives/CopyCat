using CopyCat.Data.Model;

namespace CopyCat.Data;

public interface IImpersonationAdminDataProvider
{
    List<ImpersonatedAccount> GetImpersonatedAccounts(Guid accountId);

    List<ImpersonatedAccount> GetImpersonatedAccounts();

    bool TryCreateImpersonatedAccount(ImpersonatedAccount account);
}