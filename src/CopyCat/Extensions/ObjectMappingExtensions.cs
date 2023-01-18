using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CopyCat.Data.Model;

namespace CopyCat.Extensions;

public static class ObjectMappingExtensions
{
    public static ImpersonatedAccount MapTo(this ImpersonatedAccountEntity entity)
    {
        return new ImpersonatedAccount
        {
            Id = entity.Id,
            AccountId = entity.AccountId,
            Name = entity.Name,
            SendingFacilityId = entity.SendingFacilityId,
            SendingClientId = entity.SendingClientId,
            IsActive = entity.IsActive,
            CreatedOn = entity.CreatedOn
        };
    }

    public static Account MapTo(this AccountEntity entity)
    {
        return new Account
        {
            Id = entity.Id,
            Name = entity.Name,
            IsActive = entity.IsActive,
            CreatedOn = entity.CreatedOn
        };
    }

    public static List<ImpersonatedAccount> MapTo(this List<ImpersonatedAccountEntity> accounts)
    {
        var impersonated = new List<ImpersonatedAccount>();
        
        var memory = CollectionsMarshal.AsSpan(accounts);
        ref var ptr = ref MemoryMarshal.GetReference(memory);

        for (int i = 0; i < accounts.Count; i++)
        {
            var account = Unsafe.Add(ref ptr, i);
            impersonated.Add(account.MapTo());
        }

        return impersonated;
    }
}