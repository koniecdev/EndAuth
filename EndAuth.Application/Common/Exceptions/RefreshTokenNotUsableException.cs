namespace EndAuth.Application.Common.Exceptions;

public class RefreshTokenNotUsableException : Exception
{
    public RefreshTokenNotUsableException(string refreshToken)
        : base($"Following refresh token is not usable anymore: '{refreshToken}'")
    {
    }
}
