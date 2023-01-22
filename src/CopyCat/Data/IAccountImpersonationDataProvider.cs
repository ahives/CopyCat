using CopyCat.Data.Model;

namespace CopyCat.Data;

public interface IAccountImpersonationDataProvider
{
    Task<ImpersonatedAccountEntity?> GetAccount(string sendingFacilityId, string sendingClientId);
}