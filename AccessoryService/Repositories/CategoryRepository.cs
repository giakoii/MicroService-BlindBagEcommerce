using AccessoryService.Models;
using AccessoryService.Models.Helper;

namespace AccessoryService.Repositories;

public class CategoryRepository : BaseRepository<Category, Guid>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context)
    {
        Context = context;
    }
}

public interface ICategoryRepository : IBaseRepository<Category, Guid>
{
}