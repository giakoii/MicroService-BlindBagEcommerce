using System.Net;
using OpenIddict.EntityFrameworkCore.Models;
using YamlDotNet.Core.Tokens;

namespace AuthService.Models.OpenDictModels;

public class Application : OpenIddictEntityFrameworkCoreApplication<string, Authorization, Token>
{
    
}