namespace EndAuth.Application.Common.Exceptions;

public class InvalidTokenException : Exception
{
    public InvalidTokenException(string token)
        : base($"Following token is not valid anymore: '{token}'")
    {
    }
}
