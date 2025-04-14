using System.Security.Claims;
using OpenIddict.Abstractions;
using ProfileService.SystemClient;

namespace Client.SystemClient;

public class IdentityApiClient : IIdentityApiClient
{
    public IdentityEntity? GetIdentity(ClaimsPrincipal user)
    {
        var identity = user.Identity as ClaimsIdentity;
        
        // Get Id
        var id = identity?.FindFirst(OpenIddictConstants.Claims.Subject)?.Value ?? string.Empty;
        
        // Get username
        var userNm = identity.FindFirst("UserId")?.Value ?? string.Empty;
        
        // Get email
        var email = identity.FindFirst(OpenIddictConstants.Claims.Email)?.Value ?? string.Empty;
        
        // Create IdentityEntity
        var identityEntity = new IdentityEntity
        {
            UserId = id,
            UserName = userNm,
            Email = email,
        };
        return identityEntity;
    }
}