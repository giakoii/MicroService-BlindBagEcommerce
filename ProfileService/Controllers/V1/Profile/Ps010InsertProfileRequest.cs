using System.ComponentModel.DataAnnotations;

namespace ProfileService.Controllers.V1.Profile;

public class Ps010InsertProfileRequest : AbstractApiRequest
{
    public IFormFile? ImageUrl { get; set; }

    [Required(ErrorMessage = "BirthDate is required")]
    public DateOnly BirthDate { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    public byte? Gender { get; set; }
    
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; }
    
    public Ps010InsertUserAddressRequest UserAddressRequest { get; set; }
}

public class Ps010InsertUserAddressRequest
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