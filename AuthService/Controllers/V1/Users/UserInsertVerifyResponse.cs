namespace AuthService.Controllers.V1.Users;

public class UserInsertVerifyResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}