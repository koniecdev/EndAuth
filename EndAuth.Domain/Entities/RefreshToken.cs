namespace EndAuth.Entities;

public class RefreshToken
{
    public string RefreshTokenId { get; set; } = "";
    public bool IsValid { get; set; } = true;
    public DateTime Expires { get; set; }
}