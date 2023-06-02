namespace EndAuth.Shared.Dtos;

public record AuthSuccessResponse(string AccessToken, string RefreshToken);