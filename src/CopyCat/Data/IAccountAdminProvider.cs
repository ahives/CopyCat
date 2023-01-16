using CopyCat.Data.Model;

namespace CopyCat.Data;

public interface IAccountAdminProvider
{
    List<Account> GetAllAccounts();

    bool TryCreateAccount(Account account);
}