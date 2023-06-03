namespace EndAuth.Application.Common.Exceptions;

public class InvalidRefreshTokenException : Exception
{
    public InvalidRefreshTokenException(string token)
        : base($"Following refresh token does not exists: '{token}'")
    {
    }
}
