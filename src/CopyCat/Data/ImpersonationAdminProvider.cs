using CopyCat.Data.Model;

namespace CopyCat.Data;

public class ImpersonationAdminProvider :
    IImpersonationAdminProvider
{
    private readonly ImpersonationDbContext _db;

    public ImpersonationAdminProvider(ImpersonationDbContext db)
    {
        _db = db;
    }

    public List<ImpersonatedAccount> GetImpersonatedAccounts(Guid accountId)
    {
        var accounts = from account in _db.ImpersonatedAccounts
            where account.AccountId == accountId && account.IsActive
            select new ImpersonatedAccount
            {
                Id = account.Id,
                AccountId = account.AccountId,
                Name = account.Name,
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
                SendingFacilityId = account.SendingFacilityId,
                SendingAppId = account.SendingAppId,
                CreatedOn = account.CreatedOn
            };

        return accounts.ToList();
    }

    public void AddImpersonatedAccount(ImpersonatedAccount account)
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
    }
}