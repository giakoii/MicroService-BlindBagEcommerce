using System.Security.Claims;
using OpenIdConnect.Systemserver;

namespace OpenIdConnect.Identity;

public interface IIdentityApiClient
{
    public IdentityEntity GetIdentity(ClaimsPrincipal user);
}