using CopyCat.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CopyCat.Data;

public class ImpersonationAdminDataProvider :
    IImpersonationAdminDataProvider
{
    private readonly ImpersonationDbContext _db;

    public ImpersonationAdminDataProvider(ImpersonationDbContext db)
    {
        _db = db;
    }

    public List<ImpersonatedAccount> GetAccounts(Guid accountId)
    {
        var accounts = from account in _db.ImpersonatedAccounts
            where account.AccountId == accountId
            select new ImpersonatedAccount
            {
                Id = account.Id,
                AccountId = account.AccountId,
                Name = account.Name,
                IsActive = account.IsActive,
                SendingFacilityId = account.SendingFacilityId,
                SendingAppId = account.SendingAppId,
                CreatedOn = account.CreatedOn
            };

        return accounts.ToList();
    }

    public List<ImpersonatedAccount> GetAllAccounts()
    {
        var accounts = from account in _db.ImpersonatedAccounts
            where account.IsDeleted == false
            select new ImpersonatedAccount
            {
                Id = account.Id,
                AccountId = account.AccountId,
                Name = account.Name,
                IsActive = account.IsActive,
                SendingFacilityId = account.SendingFacilityId,
                SendingAppId = account.SendingAppId,
                CreatedOn = account.CreatedOn
            };

        return accounts.ToList();
    }

    public bool TryCreateAccount(ImpersonatedAccount account)
    {
        var entity = new ImpersonatedAccountEntity
        {
            Id = account.Id,
            AccountId = account.AccountId,
            Name = account.Name,
            SendingFacilityId = account.SendingFacilityId,
            SendingAppId = account.SendingAppId,
            CreatedOn = DateTimeOffset.UtcNow,
            UpdatedOn = DateTimeOffset.UtcNow
        };

        _db.ImpersonatedAccounts.Add(entity);
        _db.SaveChanges();

        return _db.Entry(entity).State == EntityState.Added;
    }

    public bool TryActivateAccount(Guid id, out ImpersonatedAccount account)
    {
        return SetIsActiveAccount(id, true, out account);
    }

    public bool TryDeactivateAccount(Guid id, out ImpersonatedAccount account)
    {
        return SetIsActiveAccount(id, false, out account);
    }

    bool SetIsActiveAccount(Guid id, bool isActive, out ImpersonatedAccount account)
    {
        var entity = _db.ImpersonatedAccounts.Find(id);

        if (entity is null)
        {
            account = default!;
            return false;
        }

        entity.IsActive = isActive;

        _db.ImpersonatedAccounts.Update(entity);
        _db.SaveChanges();

        account = new ImpersonatedAccount
        {
            Id = entity.Id,
            AccountId = entity.AccountId,
            Name = entity.Name,
            IsActive = entity.IsActive,
            SendingFacilityId = entity.SendingFacilityId,
            SendingAppId = entity.SendingAppId,
            CreatedOn = entity.CreatedOn
        };
        
        return _db.Entry(entity).State == EntityState.Modified;
    }
}