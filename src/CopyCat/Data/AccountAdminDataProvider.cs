using CopyCat.Data.Model;
using CopyCat.Extensions;
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

    public List<Account> GetAllAccounts()
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

        return accounts.ToList();
    }

    public bool TryCreateAccount(Account account)
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
        _db.SaveChanges();

        return _db.Entry(entity).State == EntityState.Added;
    }

    public bool FindAccount(Guid accountId)
    {
        var account = _db.Accounts.Find(accountId);

        return account is not null;
    }

    public bool TryActivateAccount(Guid id, out Account account)
    {
        var entity = _db.Accounts.Find(id);

        if (entity is null)
        {
            account = default!;
            return false;
        }

        entity.IsActive = true;

        _db.Accounts.Update(entity);
        _db.SaveChanges();

        account = entity.MapTo();
        
        return _db.Entry(entity).State == EntityState.Modified;
    }

    public bool TryDeactivateAccount(Guid id, out Account account, out IReadOnlyList<ImpersonatedAccount> impersonatedAccounts)
    {
        var entity = _db.Accounts.Find(id);

        if (entity is null)
        {
            account = default!;
            impersonatedAccounts = new List<ImpersonatedAccount>();
            return false;
        }

        entity.IsActive = false;

        _db.Accounts.Update(entity);

        var impersonated = UpdateImpersonatedAccounts(entity.Id);

        _db.SaveChanges();

        account = entity.MapTo();
        impersonatedAccounts = impersonated.MapTo();
        
        return _db.Entry(entity).State == EntityState.Modified;
    }

    List<ImpersonatedAccountEntity> UpdateImpersonatedAccounts(Guid accountId)
    {
        var impersonated = (from acct in _db.ImpersonatedAccounts
                where acct.AccountId == accountId
                select acct)
            .ToList();

        for (int i = 0; i < impersonated.Count; i++)
        {
            impersonated[i].IsActive = false;
            _db.ImpersonatedAccounts.Update(impersonated[i]);
        }

        return impersonated;
    }
}