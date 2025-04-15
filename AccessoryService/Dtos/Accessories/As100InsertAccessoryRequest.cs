using System.ComponentModel.DataAnnotations;
using ProfileService.Controllers;

namespace AccessoryService.Dtos.Accessories;

public class As100InsertAccessoryRequest : AbstractApiRequest
{
    public string? Description { get; set; }

    [Required(ErrorMessage = "CategoryId is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    public int Quantity { get; set; }

    public Guid? CategoryId { get; set; }

    public decimal? Discount { get; set; }

    public string? ShortDescription { get; set; }
    
    public List<IFormFile> Images { get; set; }
}
