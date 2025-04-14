using System.ComponentModel.DataAnnotations;

namespace ProfileService.Controllers.V1.Profile;

public class Ps010SelectProfileResponse : AbstractApiResponse<Ps010SelectProfileEntity>
{
    public override Ps010SelectProfileEntity Response { get; set; }
}

public class Ps010SelectProfileEntity
{
    public string ImageUrl { get; set; }
    
    public string Email { get; set; }

    public string BirthDate { get; set; }

    public short? Gender { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}