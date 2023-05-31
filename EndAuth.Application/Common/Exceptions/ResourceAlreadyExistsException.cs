namespace EndAuth.Application.Common.Exceptions;

public class ResourceAlreadyExistsException : Exception
{
    public ResourceAlreadyExistsException(string typeName, string identifier)
        : base($"Resource of type '{typeName}', with identifier of '{identifier}', already exists")
    {
    }
}
