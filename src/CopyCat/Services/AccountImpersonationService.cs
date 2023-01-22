using CopyCat.Data;
using CopyCat.Data.Model;
using CopyCat.Extensions;

namespace CopyCat.Services;

public class AccountImpersonationService :
    IAccountImpersonationService
{
    private readonly IAccountImpersonationDataProvider _dataProvider;

    public AccountImpersonationService(IAccountImpersonationDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task<Result<ImpersonatedAccount>> FindActiveAccount(string sendingFacilityId, string sendingClientId)
    {
        var account = await _dataProvider.GetAccount(sendingFacilityId, sendingClientId);

        if (account == default)
            return new Result<ImpersonatedAccount> {IsData = false, HasFaulted = true};

        return account.IsActive
            ? new Result<ImpersonatedAccount> {Data = account.MapTo(), IsData = true, HasFaulted = false}
            : new Result<ImpersonatedAccount> {IsData = false, HasFaulted = false};
    }
}