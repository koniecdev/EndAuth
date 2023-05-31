namespace EndAuth.Application.Common.Interfaces;

public interface IJwtService
{
    Task<string> CreateTokenAsync();
}