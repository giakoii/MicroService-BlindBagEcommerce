using System.Security.Claims;

namespace AccessoryService.SystemClient;

public interface IIdentityApiClient
{
    public IdentityEntity? GetIdentity(ClaimsPrincipal user);
}