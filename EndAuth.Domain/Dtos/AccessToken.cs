namespace EndAuth.Domain.Entities;

public class AccessToken
{
    public string Token { get; set; } = "";
    public DateTimeOffset Expires { get; set; }
}