using Client.SystemClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ProfileService.Services;
using ProfileService.Utils.Const;

namespace ProfileService.Controllers.V1.Address;

/// <summary>
/// Ps020InsertAddressController - Insert Address
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class Ps020InsertAddressController : AbstractApiAsyncController<Ps020InsertAddressRequest, Ps020InsertAddressResponse, string>
{
    private readonly IAddressService _addressService;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="addressService"></param>
    /// <param name="identityApiClient"></param>
    public Ps020InsertAddressController(IAddressService addressService, IIdentityApiClient identityApiClient)
    {
        _addressService = addressService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override async Task<Ps020InsertAddressResponse> ProcessRequest(Ps020InsertAddressRequest request)
    {
        return await ProcessRequest(request, _logger, new Ps020InsertAddressResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override async Task<Ps020InsertAddressResponse> Exec(Ps020InsertAddressRequest request)
    {
        return await _addressService.InsertAddress(request, _identityEntity);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override Ps020InsertAddressResponse ErrorCheck(Ps020InsertAddressRequest request, List<DetailError> detailErrorList)
    {
        var response = new Ps020InsertAddressResponse() { Success = false };
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