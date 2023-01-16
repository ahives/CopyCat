using CopyCat.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CopyCat.Data;

public class AccountAdminProvider :
    IAccountAdminProvider
{
    private readonly ImpersonationDbContext _db;

    public AccountAdminProvider(ImpersonationDbContext db)
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
}