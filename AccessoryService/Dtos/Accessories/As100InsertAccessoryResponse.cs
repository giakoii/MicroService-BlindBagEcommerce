using AccessoryService.Controllers;

namespace AccessoryService.Dtos.Accessories;

public class As100InsertAccessoryResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}