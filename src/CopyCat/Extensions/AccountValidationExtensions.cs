using CopyCat.Data.Model;
using CopyCat.Model;

namespace CopyCat.Extensions;

public static class AccountValidationExtensions
{
    public static bool IsValid(this AccountCreationRequest request)
    {
        if (request is null)
            return false;

        return true;
    }
}