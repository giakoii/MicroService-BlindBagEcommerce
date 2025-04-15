using System.Text;
using AccessoryService.Dtos.Accessories;
using AccessoryService.Models;
using AccessoryService.Repositories;
using AccessoryService.Utils.Const;
using Microsoft.EntityFrameworkCore;

namespace AccessoryService.Services;

/// <summary>
/// AccessoryService - Accessory Service
/// </summary>
public class AccessoryService : IAccessoryService
{
    private readonly IImageService _imageService;
    private readonly IImageRepository _imageRepository;
    private readonly IAccessoryRepository _accessoryRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="imageService"></param>
    /// <param name="accessoryRepository"></param>
    public AccessoryService(IImageService imageService, IAccessoryRepository accessoryRepository, IImageRepository imageRepository)
    {
        _imageService = imageService;
        _accessoryRepository = accessoryRepository;
        _imageRepository = imageRepository;
    }

    /// <summary>
    /// Select Accessories
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<As100SelectAccessoriesResponse> SelectAccessories(As100SelectAccessoriesRequest request)
    {
        var response = new As100SelectAccessoriesResponse { Success = false };
        
        // Get Accessories
        var accessories = await _accessoryRepository
            .GetView<VwAccessory>()
            .Select(x => new As100SelectAccessoriesEntity
            {
                AccessoryId = x.AccessoryId!,
                Name = x.Name!,
                Discount = x.Discount,
                CategoryId = x.CategoryId,
                Price = x.Price,
                Quantity = x.Quantity,
                ShortDescription = x.ShortDescription,
                ImageUrls = _imageRepository
                    .GetView<VwImage>(y => y.AccessoryId == x.AccessoryId)
                    .Select(y => y.ImageUrl)
                    .ToList()!,
            }).ToListAsync();
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        response.Response = accessories;
        return response;
    }

    /// <summary>
    /// Insert Accessory
    /// </summary>
    /// <param name="request"></param>
    /// <param name="userName"></param>
    /// <returns></returns>
    public async Task<As100InsertAccessoryResponse> InsertAccessory(As100InsertAccessoryRequest request, string userName)
    {
        var response = new As100InsertAccessoryResponse { Success = false };
        
        // Begin transaction
        await _accessoryRepository.ExecuteInTransactionAsync(async () =>
        {
            // Insert Accessory
            var accessory = new Accessory
            {
                AccessoryId = GenerateId(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ShortDescription = request.ShortDescription,
                Quantity = request.Quantity,
                Discount = request.Discount,
                CategoryId = request.CategoryId,
            };
            
            // Insert Images
            await _imageService.InsertImage(
                request.Images.ToList(),
                accessory.AccessoryId
            );
            
            // Save Changes 
            _accessoryRepository.Add(accessory);
            await _accessoryRepository.SaveChangesAsync(userName);
            return true;
        });
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }
    
    /// <summary>
    /// Generate Accessory ID
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public string GenerateId(int length = 8)
    {
        // Create a new instance of Random
        var random = new Random();

        // Create a timestamp string
        string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");

        // Create a StringBuilder to build the ID
        StringBuilder idBuilder = new StringBuilder();
        idBuilder.Append(timestamp.Substring(timestamp.Length - 5));

        // Add random characters to the ID
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        for (int i = 0; i < length - 5; i++)
        {
            idBuilder.Append(chars[random.Next(chars.Length)]);
        }
        
        return $"ACC{idBuilder.ToString()}";
    }
}

public interface IAccessoryService
{
    Task<As100SelectAccessoriesResponse> SelectAccessories(As100SelectAccessoriesRequest request);
    
    Task<As100InsertAccessoryResponse> InsertAccessory(As100InsertAccessoryRequest request, string userName);
}