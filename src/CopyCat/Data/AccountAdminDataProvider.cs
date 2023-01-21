using CopyCat.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CopyCat.Data;

public class AccountAdminDataProvider :
    IAccountAdminDataProvider
{
    private readonly ImpersonationDbContext _db;

    public AccountAdminDataProvider(ImpersonationDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<Account>> GetAllAccounts()
    {
        var accounts = from account in _db.Accounts
            where account.IsDeleted == false
            select new Account
            {
                Id = account.Id,
                Name = account.Name,
                IsActive = account.IsActive,
                CreatedOn = account.CreatedOn
            };

        return await accounts.ToListAsync();
    }

    public async Task<bool> TryCreateAccount(Account account)
    {
        var entity = new AccountEntity
        {
            Id = account.Id,
            Name = account.Name,
            IsActive = account.IsActive,
            IsDeleted = false,
            CreatedOn = DateTimeOffset.UtcNow,
            UpdatedOn = DateTimeOffset.UtcNow
        };
        
        _db.Accounts.Add(entity);
        await _db.SaveChangesAsync();

        return _db.Entry(entity).State == EntityState.Added;
    }

    public async Task<bool> TryUpdateAccountName(AccountEntity account, string name)
    {
        if (account is null)
            return false;

        account.Name = name;

        _db.Accounts.Update(account);
        await _db.SaveChangesAsync();
        
        return _db.Entry(account).State == EntityState.Modified;
    }

    public async Task<AccountEntity?> GetAccount(Guid accountId)
    {
        return await _db.Accounts.FindAsync(accountId);
    }

    public async Task<bool> TryActivateAccount(AccountEntity account)
    {
        if (account is null)
            return false;

        account.IsActive = true;

        _db.Accounts.Update(account);
        await _db.SaveChangesAsync();
        
        return _db.Entry(account).State == EntityState.Modified;
    }

    public async Task<bool> TryDeactivateAccount(AccountEntity account)
    {
        if (account is null)
            return false;

        account.IsActive = false;

        _db.Accounts.Update(account);

        var impersonated = (from acct in _db.ImpersonatedAccounts
                where acct.AccountId == account.Id
                select acct)
            .ToList();

        for (int i = 0; i < impersonated.Count; i++)
        {
            impersonated[i].IsActive = false;
            _db.ImpersonatedAccounts.Update(impersonated[i]);
        }

        await _db.SaveChangesAsync();
        
        return _db.Entry(account).State == EntityState.Modified;
    }
}