using Microsoft.EntityFrameworkCore;
using ProfileService.Controllers.V1.Address;
using ProfileService.Controllers.V1.Profile;
using ProfileService.Models;
using ProfileService.Repositories;
using ProfileService.SystemClient;
using ProfileService.Utils.Const;

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
    /// Insert User Address for Screen Ps010
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

    /// <summary>
    /// InsertAddress for Screen Ps020
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityEntity"></param>
    /// <returns></returns>
    public async Task<Ps020InsertAddressResponse> InsertAddress(Ps020InsertAddressRequest request, IdentityEntity identityEntity)
    {
        var response = new Ps020InsertAddressResponse() { Success = false };
        
        // Begin transaction
        await _addressRepository.ExecuteInTransactionAsync(async () =>
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
            await _addressRepository.SaveChangesAsync(identityEntity.UserName);
            return true;
        });
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// SelectAddresses for Screen Ps020
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityEntity"></param>
    /// <returns></returns>
    public async Task<Ps020SelectAddressesResponse> SelectAddresses(Ps020SelectAddressesRequest request, IdentityEntity identityEntity)
    {
        var response = new Ps020SelectAddressesResponse() { Success = false };
        
        // Select addresses
        var addresses = await _addressRepository
            .GetView<VwUserAddress>(a => a.Username == identityEntity.UserName)
            .Select(x => new Ps020SelectAddressesEntity
            {
                AddressLine = x.AddressLine!,
                Ward = x.Ward!,
                City = x.City!,
                District = x.District!,
                Province = x.Province!,
            }).ToListAsync();
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        response.Response = addresses;
        return response;
    }
}