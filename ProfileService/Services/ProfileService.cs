using Microsoft.EntityFrameworkCore;
using ProfileService.Controllers.V1.Profile;
using ProfileService.Logics;
using ProfileService.Models;
using ProfileService.Repositories;
using ProfileService.SystemClient;
using ProfileService.Utils;
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
        var user = await _profileRepository.Find(x => x.Username == identityEntity.UserName).FirstOrDefaultAsync();
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
                Username = identityEntity.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                ImageUrl = await _cloudinaryLogic.UploadImageAsync(request.ImageUrl!),
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
    /// <returns></returns>
    public Ps010SelectProfileResponse SelectProfile(IdentityEntity identityEntity)
    {
        var response = new Ps010SelectProfileResponse() { Success = false };
        // x => x.Username == identityEntity.UserName
        // Get user profile
        var userProfile = _profileRepository.GetView<VwUserProfile>().FirstOrDefault();
        if (userProfile == null)
        {
            response.SetMessage(MessageId.E11001);
            return response;
        }

        response.Response = new Ps010SelectProfileEntity
        {
            Email = identityEntity.Email,
            FirstName = userProfile.FirstName!,
            LastName = userProfile.LastName!,
            Gender = userProfile.Gender,
            BirthDate = StringUtil.ConvertToDateAsDdMmYyyy(userProfile.BirthDate),
            ImageUrl = userProfile.ImageUrl!,
        };
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Update Profile
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityEntity"></param>
    /// <returns></returns>
    public async Task<Ps010UpdateProfileResponse> UpdateProfile(Ps010UpdateProfileRequest request, IdentityEntity identityEntity)
    {
        var response = new Ps010UpdateProfileResponse() { Success = false };
        
        // Get user profile
        var userProfile = _profileRepository.Find(x => x.Username == identityEntity.UserName).FirstOrDefault();
        if (userProfile == null)
        {
            response.SetMessage(MessageId.E11001);
            return response;
        }
        
        // Update user profile
        await _profileRepository.ExecuteInTransactionAsync(async () =>
        {
            userProfile.FirstName = request.FirstName;
            userProfile.LastName = request.LastName;
            userProfile.Gender = request.Gender;
            userProfile.BirthDate = DateOnly.ParseExact(request.BirthDate!, "dd/MM/yyyy");
            userProfile.ImageUrl = await _cloudinaryLogic.UploadImageAsync(request.ImageUrl!);
            _profileRepository.Update(userProfile);
            await _profileRepository.SaveChangesAsync(identityEntity.UserName);
            return true;
        });
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }
}