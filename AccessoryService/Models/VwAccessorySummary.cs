using System;
using System.Collections.Generic;

namespace AccessoryService.Models;

public partial class VwAccessorySummary
{
    public Guid? CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public long? TotalAccessories { get; set; }

    public long? TotalImages { get; set; }

    public long? TotalQuantity { get; set; }
}
