using AccessoryService.Models;
using AccessoryService.Models.Helper;

namespace AccessoryService.Repositories;

public class AccessoryRepository : BaseRepository<Accessory, string>, IAccessoryRepository
{
    public AccessoryRepository(AppDbContext context)
    {
        Context = context;
    }
}

public interface IAccessoryRepository : IBaseRepository<Accessory, string>
{
}