using CopyCat.Model;

namespace CopyCat.Extensions;

public static class ImpersonatedAccountValidationExtensions
{
    public static bool IsValid(this ImpersonatedAccountCreationRequest request)
    {
        if (request is null)
            return false;

        return true;
    }
}