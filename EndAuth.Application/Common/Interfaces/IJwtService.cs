namespace EndAuth.Application.Common.Interfaces;

public interface IJwtService<TUser>
{
    Task<string> CreateTokenAsync(string email);
}