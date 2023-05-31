namespace EndAuth.Application.Common.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string typeName, string identifier)
        : base($"Could not find a resource of type '{typeName}', with identifier of '{identifier}'")
    {
    }
}
