using AccessoryService.Dtos.Accessories;
using AccessoryService.Services;
using AccessoryService.Utils.Const;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace AccessoryService.Controllers.V1.Accessories;

/// <summary>
/// As100SelectAccessoriesController - Select Accessories
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class As100SelectAccessoriesController : AbstractApiGetAsyncControllerNotToken<As100SelectAccessoriesRequest, As100SelectAccessoriesResponse, List<As100SelectAccessoriesEntity>>
{
    private readonly IAccessoryService _accessoryService;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="accessoryService"></param>
    public As100SelectAccessoriesController(IAccessoryService accessoryService)
    {
        _accessoryService = accessoryService;
    }

    /// <summary>
    /// Incoming GET
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public override Task<As100SelectAccessoriesResponse> Get(As100SelectAccessoriesRequest request)
    {
        return Get(request, _logger, new As100SelectAccessoriesResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override Task<As100SelectAccessoriesResponse> Exec(As100SelectAccessoriesRequest request)
    {
        return _accessoryService.SelectAccessories(request);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override As100SelectAccessoriesResponse ErrorCheck(As100SelectAccessoriesRequest request, List<DetailError> detailErrorList)
    {
        var response = new As100SelectAccessoriesResponse() { Success = false };
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