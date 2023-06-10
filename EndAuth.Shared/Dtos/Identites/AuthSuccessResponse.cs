namespace EndAuth.Shared.Dtos.Identites;

public record AuthSuccessResponse(string AccessToken, DateTimeOffset AccessTokenExpiration, string RefreshToken, DateTimeOffset RefreshTokenExpiration);