using Client.SystemClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ProfileService.Services;
using ProfileService.Utils.Const;

namespace ProfileService.Controllers.V1.Profile;

public class Ps010UpdateProfileController : AbstractApiAsyncController<Ps010UpdateProfileRequest, Ps010UpdateProfileResponse, string>
{
    private readonly IProfileService _profileService;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="profileService"></param>
    /// <param name="identityApiClient"></param>
    public Ps010UpdateProfileController(IProfileService profileService, IIdentityApiClient identityApiClient)
    {
        _profileService = profileService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Put
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override async Task<Ps010UpdateProfileResponse> ProcessRequest(Ps010UpdateProfileRequest request)
    {
        return await ProcessRequest(request, _logger, new Ps010UpdateProfileResponse());
    }

    protected override async Task<Ps010UpdateProfileResponse> Exec(Ps010UpdateProfileRequest request)
    {
        return await _profileService.UpdateProfile(request, _identityEntity);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override Ps010UpdateProfileResponse ErrorCheck(Ps010UpdateProfileRequest request, List<DetailError> detailErrorList)
    {
        var response = new Ps010UpdateProfileResponse() { Success = false };
        if (detailErrorList.Count > 0)
        {
            // Error
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }
        // True
        response.Success = true;
        return response;
    }
}