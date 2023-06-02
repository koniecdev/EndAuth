using Microsoft.IdentityModel.Tokens;

namespace EndAuth.JwtProvider
{
    public interface ITokenParametersFactory
    {
        TokenValidationParameters Create();
    }
}