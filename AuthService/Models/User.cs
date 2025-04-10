using System;
using System.Collections.Generic;

namespace AuthService.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public string? UserName { get; set; }
    
    public string? Email { get; set; }
    
    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string? Key { get; set; }

    public virtual Role Role { get; set; } = null!;
    
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
