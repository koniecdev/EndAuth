namespace EndAuth.Application.Common.Exceptions;

public class TokenExpiredException : Exception
{
    public TokenExpiredException(string token)
        : base($"Following token has expried: '{token}'")
    {
    }
}
