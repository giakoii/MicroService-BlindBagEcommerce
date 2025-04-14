using System.ComponentModel.DataAnnotations;

namespace ProfileService.Controllers.V1.Profile;

public class Ps010UpdateProfileRequest : AbstractApiRequest
{
    public IFormFile? ImageUrl { get; set; }

    [Required(ErrorMessage = "BirthDate is required")]
    public string BirthDate { get; set; } = null!;

    [Required(ErrorMessage = "Gender is required")]
    public byte Gender { get; set; }
    
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; } = null!;
    
    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; } = null!;
}