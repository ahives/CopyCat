using CopyCat.Data.Model;

namespace CopyCat.Data;

public interface IImpersonationAdminProvider
{
    List<ImpersonatedAccount> GetImpersonatedAccounts(Guid accountId);

    List<ImpersonatedAccount> GetImpersonatedAccounts();

    bool TryCreateImpersonatedAccount(ImpersonatedAccount account);
}