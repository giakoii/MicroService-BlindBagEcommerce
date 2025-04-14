using ProfileService.Controllers.V1.Profile;
using ProfileService.SystemClient;

namespace ProfileService.Services;

public interface IProfileService
{
    Task<Ps010InsertProfileResponse> InsertProfile(Ps010InsertProfileRequest request, IdentityEntity identityEntity);
    
    Ps010SelectProfileResponse SelectProfile(IdentityEntity identityEntity);
    Task<Ps010UpdateProfileResponse> UpdateProfile(Ps010UpdateProfileRequest request, IdentityEntity identityEntity);
}