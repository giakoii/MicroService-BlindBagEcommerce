namespace AuthService.Controllers.V1.Users;

public class UserInsertResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}