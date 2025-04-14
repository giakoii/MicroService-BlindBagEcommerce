using Client.SystemClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ProfileService.Services;
using ProfileService.Utils.Const;

namespace ProfileService.Controllers.V1.Address;

/// <summary>
/// Ps020SelectAddressesController - Select Addresses
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class Ps020SelectAddressesController : AbstractApiGetAsyncController<Ps020SelectAddressesRequest, Ps020SelectAddressesResponse, List<Ps020SelectAddressesEntity>>
{
    private readonly IAddressService _addressService;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="addressService"></param>
    /// <param name="identityApiClient"></param>
    public Ps020SelectAddressesController(IAddressService addressService, IIdentityApiClient identityApiClient)
    {
        _addressService = addressService;
        _identityApiClient = identityApiClient;
    }
    
    [HttpGet]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override async Task<Ps020SelectAddressesResponse> Get(Ps020SelectAddressesRequest request)
    {
        return await Get(request, _logger, new Ps020SelectAddressesResponse());
    }

    
    protected override async Task<Ps020SelectAddressesResponse> Exec(Ps020SelectAddressesRequest request)
    {
        return await _addressService.SelectAddresses(request, _identityEntity);
    }

    protected internal override Ps020SelectAddressesResponse ErrorCheck(Ps020SelectAddressesRequest request, List<DetailError> detailErrorList)
    {
        var response = new Ps020SelectAddressesResponse() { Success = false };
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