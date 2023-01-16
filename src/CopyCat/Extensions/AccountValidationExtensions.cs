using CopyCat.Data.Model;
using CopyCat.Model;

namespace CopyCat.Extensions;

public static class AccountValidationExtensions
{
    public static bool IsValid(this CreateAccountRequest request)
    {
        if (request is null)
            return false;

        return true;
    }
}