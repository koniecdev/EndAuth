namespace EndAuth.Shared.Dtos;

public record AuthSuccessResponse(string AccessToken, DateTimeOffset AccessTokenExpiration, string RefreshToken, DateTimeOffset RefreshTokenExpiration);