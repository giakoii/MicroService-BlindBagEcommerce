using System.Data;
using Client.SystemClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using ProfileService.Models.Helper;
using ProfileService.Utils;
using ProfileService.Utils.Const;

namespace ProfileService.Controllers;

/// <summary>
/// API Controller Abstract Class
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="U"></typeparam>
/// <typeparam name="V"></typeparam>
public abstract class AbstractApiController<T, U, V> : ControllerBase
    where T : AbstractApiRequest
    where U : AbstractApiResponse<V>
{
    /// <summary>
    /// API entry point
    /// </summary>
    public abstract U ProcessRequest(T request);

    /// <summary>
    /// Main processing
    /// </summary>
    protected abstract U Exec(T request);

    /// <summary>
    /// Error check
    /// </summary>
    protected internal abstract U ErrorCheck(T request, List<DetailError> detailErrorList);

    /// <summary>
    /// Authentication API client
    /// </summary>
    protected IIdentityApiClient _identityApiClient;

    /// <summary>
    /// Transaction isolation level
    /// </summary>
    /// <remarks>
    /// Default SNAPSHOT Change it in the constructor
    /// </remarks>
    protected IsolationLevel _isolationLevel = IsolationLevel.Snapshot;

    /// <summary>
    /// TemplateMethod
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    /// <param name="returnValue"></param>
    /// <returns></returns>
    protected U ProcessRequest(T request, AppDbContext context, Logger logger, U returnValue)
    {
        var loggingUtil = new LoggingUtil(logger, context.IdentityEntity?.UserName ?? "System");

        // Get identity information 
        context.IdentityEntity = _identityApiClient?.GetIdentity(User);
        loggingUtil.StartLog(request);

        // Check authentication information
        if (context.IdentityEntity == null)
        {
            // Authentication error
            loggingUtil.FatalLog($"Authenticated, but information is missing.");
            returnValue.Success = false;
            returnValue.SetMessage(MessageId.E11006);
            loggingUtil.EndLog(returnValue);
            return returnValue;
        }

        // Additional user information
        try
        {
            context.Profiles.AsTracking().FirstOrDefault(x => x.UserName == context.IdentityEntity.UserName);
        }
        catch (Exception e)
        {
            // Additional user information error
            loggingUtil.FatalLog($"Failed to get additional user information.：{e.Message}");
            returnValue.Success = false;
            returnValue.SetMessage(MessageId.E11006);
            loggingUtil.EndLog(returnValue);
            return returnValue;
        }

        try
        {
            // Error check
            var detailErrorList = AbstractFunction<U, V>.ErrorCheck(this.ModelState);
            returnValue = ErrorCheck(request, detailErrorList);

            // If there is no error, execute the main process
            if (returnValue.Success) returnValue = Exec(request);
        }
        catch (Exception e)
        {
            return AbstractFunction<U, V>.GetReturnValue(returnValue, loggingUtil, e);
        }

        // Processing end log
        loggingUtil.EndLog(returnValue);
        return returnValue;
    }
}