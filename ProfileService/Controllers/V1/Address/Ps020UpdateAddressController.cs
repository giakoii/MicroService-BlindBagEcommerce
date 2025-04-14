using Client.SystemClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ProfileService.Services;
using ProfileService.Utils.Const;

namespace ProfileService.Controllers.V1.Address;

/// <summary>
/// Ps020UpdateAddressController - Update Address
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class Ps020UpdateAddressController : AbstractApiController<Ps020UpdateAddressRequest, Ps020UpdateAddressResponse, string>
{
    private readonly IAddressService _addressService;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="addressService"></param>
    /// <param name="identityApiClient"></param>
    public Ps020UpdateAddressController(IAddressService addressService, IIdentityApiClient identityApiClient)
    {
        _addressService = addressService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Put
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override Ps020UpdateAddressResponse ProcessRequest(Ps020UpdateAddressRequest request)
    {
        return ProcessRequest(request, _logger, new Ps020UpdateAddressResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override Ps020UpdateAddressResponse Exec(Ps020UpdateAddressRequest request)
    {
        return _addressService.UpdateAddress(request, _identityEntity);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override Ps020UpdateAddressResponse ErrorCheck(Ps020UpdateAddressRequest request, List<DetailError> detailErrorList)
    {
        var response = new Ps020UpdateAddressResponse() { Success = false };
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