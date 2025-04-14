using ProfileService.Models;

namespace ProfileService.Repositories;

public interface IProfileRepository : IBaseRepository<Profile, Guid>
{
    
}