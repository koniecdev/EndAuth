﻿using EndAuth.Domain;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EndAuth.Domain.Entities;

public class RefreshToken
{
    [Key]
    public string Token { get; set; } = "";
    public string JwtId { get; set; } = "";
    public DateTime CreationDate { get; set; }
    public DateTime Expires { get; set; }
    public bool Used { get; set; } = false;
    public bool Invalidated { get; set; } = false;
    public string ApplicationUserId { get; set; } = "";
    public virtual ApplicationUser ApplicationUser { get; set; } = null!;
}