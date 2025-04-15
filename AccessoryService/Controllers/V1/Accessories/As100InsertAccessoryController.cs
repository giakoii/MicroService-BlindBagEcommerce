using AccessoryService.Dtos.Accessories;
using AccessoryService.Services;
using AccessoryService.SystemClient;
using AccessoryService.Utils.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace AccessoryService.Controllers.V1.Accessories;

/// <summary>
/// As100InsertAccessoryController - Insert Accessory
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class As100InsertAccessoryController : AbstractApiAsyncController<As100InsertAccessoryRequest, As100InsertAccessoryResponse, string>
{
    private readonly IAccessoryService _accessoryService;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="accessoryService"></param>
    /// <param name="identityApiClient"></param>
    public As100InsertAccessoryController(IAccessoryService accessoryService, IIdentityApiClient identityApiClient)
    {
        _accessoryService = accessoryService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override async Task<As100InsertAccessoryResponse> ProcessRequest(As100InsertAccessoryRequest request)
    {
        return await ProcessRequest(request, _logger, new As100InsertAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override Task<As100InsertAccessoryResponse> Exec(As100InsertAccessoryRequest request)
    {
        return _accessoryService.InsertAccessory(request, _identityEntity.UserName);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override As100InsertAccessoryResponse ErrorCheck(As100InsertAccessoryRequest request, List<DetailError> detailErrorList)
    {
        var response = new As100InsertAccessoryResponse() { Success = false };
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