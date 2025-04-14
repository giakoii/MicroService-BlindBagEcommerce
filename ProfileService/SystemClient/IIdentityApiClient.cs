using System.Security.Claims;
using ProfileService.SystemClient;

namespace Client.SystemClient;

public interface IIdentityApiClient
{
    public IdentityEntity? GetIdentity(ClaimsPrincipal user);
}