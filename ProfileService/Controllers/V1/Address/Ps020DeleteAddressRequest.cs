using System.ComponentModel.DataAnnotations;

namespace ProfileService.Controllers.V1.Address;

public class Ps020DeleteAddressRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "AddressId is required.")]
    public Guid AddressId { get; set; }
}