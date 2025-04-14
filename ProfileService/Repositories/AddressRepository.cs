using ProfileService.Models;
using ProfileService.Models.Helper;

namespace ProfileService.Repositories;

public class AddressRepository : BaseRepository<Address, Guid>, IAddressRepository
{
    public AddressRepository(AppDbContext context)
    {
        Context = context;
    }
}