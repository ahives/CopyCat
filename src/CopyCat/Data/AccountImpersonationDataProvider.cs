using CopyCat.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CopyCat.Data;

public class AccountImpersonationDataProvider :
    IAccountImpersonationDataProvider
{
    private readonly ImpersonationDbContext _db;

    public AccountImpersonationDataProvider(ImpersonationDbContext db)
    {
        _db = db;
    }

    public async Task<ImpersonatedAccountEntity?> GetAccount(string sendingFacilityId, string sendingClientId)
    {
        return await (from acct in _db.ImpersonatedAccounts
                where acct.SendingFacilityId == sendingFacilityId && acct.SendingClientId == sendingClientId
                select acct)
            .FirstOrDefaultAsync();
    }
}