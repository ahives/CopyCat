using CopyCat.Data.Model;

namespace CopyCat.Data;

public interface IAccountAdminDataProvider
{
    List<Account> GetAllAccounts();

    bool TryCreateAccount(Account account);

    bool FindAccount(Guid accountId);

    bool TryActivateAccount(Guid id, out Account account);

    bool TryDeactivateAccount(Guid id, out Account account, out IReadOnlyList<ImpersonatedAccount> impersonatedAccounts);
}