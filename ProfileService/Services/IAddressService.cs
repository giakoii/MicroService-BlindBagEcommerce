using ProfileService.Controllers.V1.Profile;
using ProfileService.SystemClient;

namespace ProfileService.Services;

public interface IAddressService
{ 
    Task<bool> InsertUserAddress(Ps010InsertUserAddressRequest request, IdentityEntity identityEntity);
}