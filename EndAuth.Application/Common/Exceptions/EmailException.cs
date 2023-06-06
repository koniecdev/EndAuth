namespace EndAuth.Application.Common.Exceptions;

public class EmailException : Exception
{
    public EmailException(string emailOfReciever)
        : base($"Could not send an email to: '{emailOfReciever}'")
    {
    }
}
