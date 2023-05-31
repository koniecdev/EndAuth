namespace EndAuth.Shared.Dtos;

public record TokensResponse(string AccessToken, string RefreshToken, DateTime RefreshTokenValidUntil);