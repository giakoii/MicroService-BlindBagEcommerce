using ProfileService.Controllers.V1.Address;
using ProfileService.Controllers.V1.Profile;
using ProfileService.SystemClient;

namespace ProfileService.Services;

public interface IAddressService
{ 
    Task<bool> InsertUserAddress(Ps010InsertUserAddressRequest request, IdentityEntity identityEntity);
    
    Task<Ps020InsertAddressResponse> InsertAddress(Ps020InsertAddressRequest request, IdentityEntity identityEntity);
    
    Task<Ps020SelectAddressesResponse> SelectAddresses(Ps020SelectAddressesRequest request, IdentityEntity identityEntity);
    
    Ps020UpdateAddressResponse UpdateAddress(Ps020UpdateAddressRequest request, IdentityEntity identityEntity);
    Ps020DeleteAddressResponse DeleteAddress(Ps020DeleteAddressRequest request, IdentityEntity identityEntity);
}