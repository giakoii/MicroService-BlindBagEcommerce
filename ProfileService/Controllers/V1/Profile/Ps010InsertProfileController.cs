using Client.SystemClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ProfileService.Services;
using ProfileService.Utils.Const;

namespace ProfileService.Controllers.V1.Profile;

/// <summary>
/// Ps010InsertProfileController - Insert User Profile
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class Ps010InsertProfileController : AbstractApiAsyncController<Ps010InsertProfileRequest, Ps010InsertProfileResponse, string>
{
    private readonly IProfileService _profileService;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="profileService"></param>
    /// <param name="identityApiClient"></param>
    public Ps010InsertProfileController(IProfileService profileService, IIdentityApiClient identityApiClient)
    {
        _profileService = profileService;
        _identityApiClient = identityApiClient;
    }
   
    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override async Task<Ps010InsertProfileResponse> ProcessRequest(Ps010InsertProfileRequest request)
    {
        return await ProcessRequest(request, _logger, new Ps010InsertProfileResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override async Task<Ps010InsertProfileResponse> Exec(Ps010InsertProfileRequest request)
    {
        return await _profileService.InsertProfile(request, _identityEntity);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override Ps010InsertProfileResponse ErrorCheck(Ps010InsertProfileRequest request, List<DetailError> detailErrorList)
    {
        var response = new Ps010InsertProfileResponse() { Success = false };
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