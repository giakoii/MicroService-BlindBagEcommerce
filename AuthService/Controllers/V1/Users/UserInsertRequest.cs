using System.ComponentModel.DataAnnotations;

namespace AuthService.Controllers.V1.Users;

public class UserInsertRequest
{
    [Required(ErrorMessage = "UserName is required")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, ErrorMessage = "Password must be at least 6 characters long", MinimumLength = 8)]
    public string Password { get; set; }
}