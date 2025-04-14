﻿using System;
using System.Collections.Generic;

namespace ProfileService.Models;

public partial class VwUserProfile
{
    public Guid? UserId { get; set; }

    public string? Username { get; set; }

    public string? ImageUrl { get; set; }

    public DateOnly? BirthDate { get; set; }

    public short? Gender { get; set; }

    public Guid? PlanId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? PlanExpired { get; set; }
}
