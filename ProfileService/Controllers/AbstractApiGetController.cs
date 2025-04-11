using Client.SystemClient;
using Client.Utils;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using ProfileService.Models.Helper;

namespace ProfileService.Controllers;

public abstract class AbstractApiGetController<T, U, V> : ControllerBase
    where U : AbstractApiResponse<V>
    where T : class 
{
    protected IIdentityApiClient _identityApiClient;

    public abstract U Get([FromQuery] T filter);

    /// <summary> Main processing method for GET with filters </summary>
    protected abstract U ExecGet(T filter);

    /// <summary> Error check method for GET </summary>
    protected internal abstract U ErrorCheck(T request,List<DetailError> detailErrorList);

    /// <summary>
    /// Template method for GET with filters
    /// </summary>
    protected U Get(T filter, AppDbContext context, Logger logger, U returnValue)
    {
        var loggingUtil = new LoggingUtil(logger, context.IdentityEntity?.UserName ?? "System");

        // Get identity information 
        context.IdentityEntity = _identityApiClient.GetIdentity(User);
        loggingUtil.StartLog();

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
            context.Users.AsTracking().FirstOrDefault(x => x.UserName == context.IdentityEntity.UserName);
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
                var detailErrorList = AbstractFunction<U, V>.ErrorCheck(this.ModelState);
                returnValue = ErrorCheck(filter, detailErrorList);

                if (returnValue.Success) returnValue = ExecGet(filter);
        }
        catch (Exception e)
        {
            return AbstractFunction<U, V>.GetReturnValue(returnValue, loggingUtil, e);
        }

        loggingUtil.EndLog(returnValue);
        return returnValue;
    }
}