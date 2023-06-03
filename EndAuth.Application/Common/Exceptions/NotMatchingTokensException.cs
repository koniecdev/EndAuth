namespace EndAuth.Application.Common.Exceptions;

public class NotMatchingTokensException : Exception
{
    public NotMatchingTokensException(string jwt, string refreshToken)
        : base($"Provided JWT Access Token and Refresh Tokens are not related: '{jwt}', '{refreshToken}'")
    {
    }
}
