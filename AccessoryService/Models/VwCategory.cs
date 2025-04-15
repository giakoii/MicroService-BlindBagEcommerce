using System;
using System.Collections.Generic;

namespace AccessoryService.Models;

public partial class VwCategory
{
    public Guid? CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public Guid? ParentId { get; set; }
}
