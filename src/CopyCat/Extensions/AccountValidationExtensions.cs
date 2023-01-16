using CopyCat.Data.Model;

namespace CopyCat.Extensions;

public static class AccountValidationExtensions
{
    public static bool IsValid(this Account account)
    {
        if (account is null)
            return false;

        return true;
    }
}