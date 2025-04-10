namespace AuthService.Controllers.V1.Users;

public class UserInsertRequest
{
    public string UserName { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
}