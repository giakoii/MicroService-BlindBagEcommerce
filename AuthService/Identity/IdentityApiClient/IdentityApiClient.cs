using System.Security.Claims;
using OpenIdConnect.Identity;
using OpenIdConnect.Systemserver;
using OpenIddict.Abstractions;

namespace OpenIdConnect.SystemClient;

public class IdentityApiClient : IIdentityApiClient
{
    /// <summary>
    /// Get the identity
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public IdentityEntity GetIdentity(ClaimsPrincipal user)
    {
        // Get the identity
        var identity = user.Identity as ClaimsIdentity;
        
        // Get the user id
        var userNm = identity.FindFirst("UserId")?.Value ?? identity.FindFirst(OpenIddictConstants.Claims.Name)?.Value;
        
        // Get role name
        var roleName = identity.FindFirst(OpenIddictConstants.Claims.Role)?.Value;
        
        if (String.IsNullOrEmpty(userNm)) return null;
        // Create the identity entity
        var identityEntity = new IdentityEntity()
        {
            UserName = userNm,
            RoleName = roleName,
        };
        return identityEntity;
    }
}