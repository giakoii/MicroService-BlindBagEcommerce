using System;
using System.Collections.Generic;

namespace AccessoryService.Models;

public partial class Image
{
    public Guid ImageId { get; set; }

    public string AccessoryId { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}
