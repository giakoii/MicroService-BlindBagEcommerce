using ProfileService.Controllers.V1.Profile;
using ProfileService.Models;
using ProfileService.Repositories;
using ProfileService.SystemClient;

namespace ProfileService.Services;

/// <summary>
/// AddressService - Service for managing user addresses
/// </summary>
public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="addressRepository"></param>
    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }
    
    /// <summary>
    /// Insert User Address
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityEntity"></param>
    /// <returns></returns>
    public async Task<bool> InsertUserAddress(Ps010InsertUserAddressRequest request, IdentityEntity identityEntity)
    {
        // Insert new address
        var newAddress = new Address
        {
            Ward = request.Ward,
            City = request.City,
            District = request.District,
            Province = request.Province,
            AddressLine = request.AddressLine,
            Username = identityEntity.UserName,
        };
        
        // Save changes
        await _addressRepository.AddAsync(newAddress);
        return true;
    }
}