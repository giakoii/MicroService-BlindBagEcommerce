using Client.SystemClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ProfileService.Services;
using ProfileService.Utils.Const;

namespace ProfileService.Controllers.V1.Address;

/// <summary>
/// Ps020DeleteAddressController - Delete Address
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class Ps020DeleteAddressController : AbstractApiController<Ps020DeleteAddressRequest, Ps020DeleteAddressResponse, string>
{
    private readonly IAddressService _addressService;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="addressService"></param>
    /// <param name="identityApiClient"></param>
    public Ps020DeleteAddressController(IAddressService addressService, IIdentityApiClient identityApiClient)
    {
        _addressService = addressService;
        _identityApiClient = identityApiClient;
    }
 
    /// <summary>
    /// Incoming Patch
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override Ps020DeleteAddressResponse ProcessRequest(Ps020DeleteAddressRequest request)
    {
        return ProcessRequest(request, _logger, new Ps020DeleteAddressResponse());
    }
    
    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override Ps020DeleteAddressResponse Exec(Ps020DeleteAddressRequest request)
    {
        return _addressService.DeleteAddress(request, _identityEntity);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override Ps020DeleteAddressResponse ErrorCheck(Ps020DeleteAddressRequest request, List<DetailError> detailErrorList)
    {
        var response = new Ps020DeleteAddressResponse() { Success = false };
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