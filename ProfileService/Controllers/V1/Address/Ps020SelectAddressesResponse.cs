using System.ComponentModel.DataAnnotations;

namespace ProfileService.Controllers.V1.Address;

public class Ps020SelectAddressesResponse : AbstractApiResponse<List<Ps020SelectAddressesEntity>>
{
    public override List<Ps020SelectAddressesEntity> Response { get; set; }
}

public class Ps020SelectAddressesEntity
{
    public string AddressLine { get; set; }

    public string Ward { get; set; }

    public string City { get; set; }

    public string District { get; set; }

    public string Province { get; set; }
}