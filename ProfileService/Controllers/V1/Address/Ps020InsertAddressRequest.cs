using System.ComponentModel.DataAnnotations;

namespace ProfileService.Controllers.V1.Address;

public class Ps020InsertAddressRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "AddressLine is required")]
    public string AddressLine { get; set; } = null!;

    [Required(ErrorMessage = "Ward is required")]
    public string Ward { get; set; } = null!;

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = null!;

    [Required(ErrorMessage = "District is required")]
    public string District { get; set; } = null!;
    
    [Required(ErrorMessage = "Province is required")]
    public string Province { get; set; } = null!;
}