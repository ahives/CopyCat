using CopyCat.Data.Model;

namespace CopyCat.Data;

public interface IAccountAdminDataProvider
{
    List<Account> GetAllAccounts();

    bool TryCreateAccount(Account account);

    bool FindAccount(Guid accountId);
}