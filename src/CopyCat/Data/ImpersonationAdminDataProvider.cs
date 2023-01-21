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

    public async Task<IReadOnlyList<ImpersonatedAccount>> GetAccounts(Guid accountId)
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
                SendingClientId = account.SendingClientId,
                CreatedOn = account.CreatedOn
            };

        return await accounts.ToListAsync();
    }

    public async Task<ImpersonatedAccountEntity?> GetAccount(Guid id)
    {
        return await _db.ImpersonatedAccounts.FindAsync(id);
    }

    public async Task<IReadOnlyList<ImpersonatedAccount>> GetAllAccounts()
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
                SendingClientId = account.SendingClientId,
                CreatedOn = account.CreatedOn
            };

        return await accounts.ToListAsync();
    }

    public async Task<bool> TryCreateAccount(ImpersonatedAccount account)
    {
        var entity = new ImpersonatedAccountEntity
        {
            Id = account.Id,
            AccountId = account.AccountId,
            Name = account.Name,
            SendingFacilityId = account.SendingFacilityId,
            SendingClientId = account.SendingClientId,
            CreatedOn = DateTimeOffset.UtcNow,
            UpdatedOn = DateTimeOffset.UtcNow
        };

        _db.ImpersonatedAccounts.Add(entity);
        await _db.SaveChangesAsync();

        return _db.Entry(entity).State == EntityState.Added;
    }

    public async Task<bool> TryUpdateSendingClientId(ImpersonatedAccountEntity account, string sendingClientId)
    {
        if (account is null || string.IsNullOrWhiteSpace(sendingClientId))
            return false;

        account.SendingClientId = sendingClientId;
        
        _db.ImpersonatedAccounts.Add(account);
        await _db.SaveChangesAsync();

        return _db.Entry(account).State == EntityState.Added;
    }

    public async Task<bool> TryUpdateSendingFacilityId(ImpersonatedAccountEntity account, string sendingClientId)
    {
        if (account is null || string.IsNullOrWhiteSpace(sendingClientId))
            return false;

        account.SendingFacilityId = sendingClientId;
        
        _db.ImpersonatedAccounts.Add(account);
        await _db.SaveChangesAsync();

        return _db.Entry(account).State == EntityState.Added;
    }

    public async Task<bool> TryUpdateAccountName(ImpersonatedAccountEntity account, string name)
    {
        if (account is null || string.IsNullOrWhiteSpace(name))
            return false;

        account.Name = name;
        
        _db.ImpersonatedAccounts.Add(account);
        await _db.SaveChangesAsync();
        
        return _db.Entry(account).State == EntityState.Added;
    }

    public async Task<bool> TryActivateAccount(ImpersonatedAccountEntity account)
    {
        return await SetIsActiveAccount(true, account);
    }

    public async Task<bool> TryDeactivateAccount(ImpersonatedAccountEntity account)
    {
        return await SetIsActiveAccount(false, account);
    }

    async Task<bool> SetIsActiveAccount(bool isActive, ImpersonatedAccountEntity account)
    {
        if (account is null)
            return false;

        account.IsActive = isActive;

        _db.ImpersonatedAccounts.Update(account);
        await _db.SaveChangesAsync();
        
        return _db.Entry(account).State == EntityState.Modified;
    }
}