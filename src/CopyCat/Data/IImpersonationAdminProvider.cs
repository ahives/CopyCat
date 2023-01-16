namespace CopyCat.Data;

public interface IImpersonationAdminProvider
{
    List<ImpersonatedAccount> GetImpersonatedAccounts(Guid accountId);

    List<ImpersonatedAccount> GetImpersonatedAccounts();

    void AddImpersonatedAccount(ImpersonatedAccount account);
}