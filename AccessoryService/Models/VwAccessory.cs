using System;
using System.Collections.Generic;

namespace AccessoryService.Models;

public partial class VwAccessory
{
    public string? AccessoryId { get; set; }

    public string? Description { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CategoryId { get; set; }

    public decimal? Discount { get; set; }

    public string? ShortDescription { get; set; }

    public string? CreatedBy { get; set; }
}
