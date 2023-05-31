using EndAuth.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace EndAuth.Application.Common.Exceptions;
public class IdentityResultFailedException : Exception
{
    public IdentityResultFailedException(IEnumerable<IdentityError> errors)
        : base(GenerateErrorMessage(errors))
    {
    }

    private static string GenerateErrorMessage(IEnumerable<IdentityError> errors)
    {
        StringBuilder errorBuilder = new();
        foreach(IdentityError error in errors)
        {
            errorBuilder.Append($"{error.Code} - {error.Description} \n");
        }
        return errorBuilder.ToString();
    }
}