using CopyCat.Data.Model;

namespace CopyCat.Data;

public interface IAccountAdminDataProvider
{
    Task<IReadOnlyList<Account>> GetAllAccounts();

    Task<bool> TryCreateAccount(Account account);

    Task<bool> TryUpdateAccountName(AccountEntity account, string name);

    Task<AccountEntity?> GetAccount(Guid accountId);

    Task<bool> TryActivateAccount(AccountEntity account);

    Task<bool> TryDeactivateAccount(AccountEntity account);
}