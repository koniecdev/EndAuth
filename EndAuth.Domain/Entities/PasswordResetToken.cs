namespace EndAuth.Domain.Entities;

public class PasswordResetToken
{
    public string Token { get; set; } = "";
    public string ApplicationUserId { get; set; } = "";
    public DateTimeOffset ValidUntil { get; set; }
    public bool Invalidated { get; set; } = false;
    public virtual ApplicationUser ApplicationUser { get; set; } = null!;
}