using CopyCat.Model;

namespace CopyCat.Extensions;

public static class ImpersonatedAccountValidationExtensions
{
    public static bool IsValid(this CreateImpersonatedAccountRequest request)
    {
        if (request is null)
            return false;

        return true;
    }
}