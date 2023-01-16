using CopyCat.Data.Model;

namespace CopyCat.Data;

public interface IAccountAdminProvider
{
    List<Account> GetAccounts();

    bool TryAddAccount(Account account);
}