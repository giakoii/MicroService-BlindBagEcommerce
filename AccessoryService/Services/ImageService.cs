using AccessoryService.Logics;
using AccessoryService.Models;
using AccessoryService.Repositories;

namespace AccessoryService.Services;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly CloudinaryLogic _cloudinaryLogic;

    public ImageService(IImageRepository imageRepository, CloudinaryLogic cloudinaryLogic)
    {
        _imageRepository = imageRepository;
        _cloudinaryLogic = cloudinaryLogic;
    }

    /// <summary>
    /// Upload image to cloudinary
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    public async Task<bool> InsertImage(List<IFormFile> files, string accessoryId)
    {
        var uploadTasks = files.Select(async image =>
        {
            var imageUrl = await _cloudinaryLogic.UploadImageAsync(image);
            return new Image
            {
                AccessoryId = accessoryId,
                ImageUrl = imageUrl
            };
        }).ToList();
        var images = await Task.WhenAll(uploadTasks);

        // Insert Images
        foreach (var image in images)
        {
            _imageRepository.Add(image);
        }
        return true;
    }
}

public interface IImageService
{
    Task<bool> InsertImage(List<IFormFile> files, string accessoryId);
}