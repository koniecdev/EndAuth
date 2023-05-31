namespace EndAuth.Application.Common.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException(string identifier)
        : base($"Provided credentials are not valid for user '{identifier}'")
    {
    }
}
