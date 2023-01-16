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

    public List<ImpersonatedAccount> GetImpersonatedAccounts(Guid accountId)
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

    public List<ImpersonatedAccount> GetImpersonatedAccounts()
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

    public bool TryCreateImpersonatedAccount(ImpersonatedAccount account)
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
}