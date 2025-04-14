using ProfileService.Models;
using ProfileService.Models.Helper;

namespace ProfileService.Repositories;

public class ProfileRepository : BaseRepository<Profile, Guid>, IProfileRepository
{
    public ProfileRepository(AppDbContext context)
    {
        Context = context;
    }
}