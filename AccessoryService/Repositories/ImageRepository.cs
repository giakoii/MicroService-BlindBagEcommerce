using AccessoryService.Models;
using AccessoryService.Models.Helper;

namespace AccessoryService.Repositories;

public class ImageRepository : BaseRepository<Image, Guid>, IImageRepository
{
    public ImageRepository(AppDbContext context)
    {
        Context = context;
    }
}

public interface IImageRepository : IBaseRepository<Image, Guid>
{
}