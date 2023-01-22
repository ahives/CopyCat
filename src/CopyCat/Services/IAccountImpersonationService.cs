using CopyCat.Data.Model;

namespace CopyCat.Services;

public interface IAccountImpersonationService
{
    Task<Result<ImpersonatedAccount>> FindActiveAccount(string sendingFacilityId, string sendingClientId);
}