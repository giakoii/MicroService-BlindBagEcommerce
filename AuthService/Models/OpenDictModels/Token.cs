using OpenIddict.EntityFrameworkCore.Models;

namespace AuthService.Models.OpenDictModels;

public class Token: OpenIddictEntityFrameworkCoreToken<string, Application, Authorization>
{
}