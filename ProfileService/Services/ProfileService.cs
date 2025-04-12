using Microsoft.EntityFrameworkCore;
using ProfileService.Controllers.V1.Profile;
using ProfileService.Logics;
using ProfileService.Models;
using ProfileService.Repositories;
using ProfileService.SystemClient;
using ProfileService.Utils.Const;

namespace ProfileService.Services;

/// <summary>
/// ProfileService - Service for managing user profiles
/// </summary>
public class ProfileService : IProfileService
{
    private readonly IProfileRepository _profileRepository;
    private readonly IAddressService _addressService;
    private readonly CloudinaryLogic _cloudinaryLogic;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="profileRepository"></param>
    /// <param name="cloudinaryLogic"></param>
    /// <param name="addressService"></param>
    public ProfileService(IProfileRepository profileRepository, CloudinaryLogic cloudinaryLogic, IAddressService addressService)
    {
        _profileRepository = profileRepository;
        _cloudinaryLogic = cloudinaryLogic;
        _addressService = addressService;
    }

    /// <summary>
    /// Insert Profile
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityEntity"></param>
    /// <returns></returns>
    public async Task<Ps010InsertProfileResponse> InsertProfile(Ps010InsertProfileRequest request, IdentityEntity identityEntity)
    {
        var response = new Ps010InsertProfileResponse() { Success = false };
        
        // Check User exists
        var user = await _profileRepository.Find(x => x.UserName == identityEntity.UserName).FirstOrDefaultAsync();
        if (user != null)
        {
            response.SetMessage(MessageId.E11004);
            return response;
        }
        
        // Begin transaction
        await _profileRepository.ExecuteInTransactionAsync(async () =>
        {
            // Insert new user
            var newProfile = new Profile
            {
                UserId = Guid.Parse(identityEntity.UserId),
                UserName = identityEntity.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                ImageUrl = await _cloudinaryLogic.UploadImageAsync(request.ImageUrl),
            };
            
            // Insert new address
            await _addressService.InsertUserAddress(request.UserAddressRequest, identityEntity);
            
            // Save changes
            await _profileRepository.AddAsync(newProfile);
            await _profileRepository.SaveChangesAsync(identityEntity.UserName);
            
            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return true;
        });
        
        return response;
    }

    /// <summary>
    /// Select Profile
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public Ps010SelectProfileResponse SelectProfile(string userName)
    {
        throw new NotImplementedException();
    }
}