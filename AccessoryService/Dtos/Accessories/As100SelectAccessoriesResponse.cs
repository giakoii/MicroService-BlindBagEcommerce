using AccessoryService.Controllers;

namespace AccessoryService.Dtos.Accessories;

public class As100SelectAccessoriesResponse : AbstractApiResponse<List<As100SelectAccessoriesEntity>>
{
    public override List<As100SelectAccessoriesEntity> Response { get; set; }
}

public class As100SelectAccessoriesEntity
{
    public string AccessoryId { get; set; }
    
    public string Name { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public Guid? CategoryId { get; set; }

    public decimal? Discount { get; set; }

    public string? ShortDescription { get; set; }
    
    public List<string> ImageUrls { get; set; }
}