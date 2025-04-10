using OpenIddict.EntityFrameworkCore.Models;
using YamlDotNet.Core.Tokens;

namespace AuthService.Models.OpenDictModels;

public class Authorization : OpenIddictEntityFrameworkCoreAuthorization<string, Application, Token>
{
    
}