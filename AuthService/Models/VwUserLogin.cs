using System;
using System.Collections.Generic;

namespace AuthService.Models;

public partial class VwUserLogin
{
    public string UserId { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? PasswordHash { get; set; }

    public int AccessFailedCount { get; set; }

    public string? RoleName { get; set; }
}
