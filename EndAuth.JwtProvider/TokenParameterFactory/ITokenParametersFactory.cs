using Microsoft.IdentityModel.Tokens;

namespace EndAuth.JwtProvider.TokenParameterFactory
{
    public interface ITokenParametersFactory
    {
        TokenValidationParameters Create();
    }
}