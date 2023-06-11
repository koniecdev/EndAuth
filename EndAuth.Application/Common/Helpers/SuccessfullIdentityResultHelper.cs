using Microsoft.AspNetCore.Identity;

namespace EndAuth.Application.Common.Helpers;

public static class SuccessfullIdentityResultHelper
{
    public static void CheckForErrors(IdentityResult results)
    {
        if (!results.Succeeded)
        {
            throw new IdentityResultFailedException(results.Errors);
        }
    }
}
