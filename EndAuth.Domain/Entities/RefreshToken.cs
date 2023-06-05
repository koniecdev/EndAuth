namespace EndAuth.Domain.Entities;

public class RefreshToken
{
    public string Token { get; set; } = "";
    public string JwtId { get; set; } = "";
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset Expires { get; set; }
    public bool Used { get; set; } = false;
    public bool Invalidated { get; set; } = false;
    public string ApplicationUserId { get; set; } = "";
    public virtual ApplicationUser ApplicationUser { get; set; } = null!;
}