using AuthService.Models.Helpers;
using Client.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using OpenIdConnect.Controllers;
using OpenIdConnect.Identity;
using OpenIdConnect.Utils.Consts;

namespace AuthService.Controllers;

public abstract class AbstractApiAsyncController<T, U, V> : ControllerBase
    where T : AbstractApiRequest
    where U : AbstractApiResponse<V>
{
    /// <summary>
    /// API entry point
    /// </summary>
    public abstract Task<U> Post(T request);

    /// <summary>
    /// Main processing
    /// </summary>
    protected abstract Task<U> Exec(T request, IDbContextTransaction transaction);

    /// <summary>
    /// Error check
    /// </summary>
    protected internal abstract U ErrorCheck(T request, List<DetailError> detailErrorList, IDbContextTransaction transaction);

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
    protected System.Data.IsolationLevel _isolationLevel = System.Data.IsolationLevel.Snapshot;
    
    /// <summary>
    /// TemplateMethod
    /// </summary>
    /// <param name="request"></param>
    /// <param name="appDbContext"></param>
    /// <param name="logger"></param>
    /// <param name="returnValue"></param>
    /// <returns></returns>
    protected async Task<U> Post(T request, AppDbContext appDbContext, Logger logger, U returnValue)
    {
        // Get identity information 
        appDbContext.IdentityEntity = _identityApiClient?.GetIdentity(User);
        logger.Warn(request);

        // Check authentication information
        if (appDbContext.IdentityEntity == null)
        {
            // Authentication error
            logger.Fatal($"Authenticated, but information is missing.");
            returnValue.Success = false;
            returnValue.SetMessage(MessageId.E11006);
            logger.Warn(returnValue);
            return returnValue;
        }
        // Additional user information
        try
        {
            appDbContext.IdentityEntity.UserName = appDbContext.VwUserLogins
                .AsNoTracking()
                .FirstOrDefault(x => x.UserName == appDbContext.IdentityEntity.UserName).UserName;
        }
        catch (Exception e)
        {
            // Additional user information error
            logger.Error($"Failed to get additional user information.：{e.Message}");
            returnValue.Success = false;
            returnValue.SetMessage(MessageId.E11006);
            logger.Warn(returnValue);
            return returnValue;
        }

        try
        {
            appDbContext. _Logger = logger;

            // Start transaction
            using (var transaction = appDbContext.Database.BeginTransaction())
            {
                // Error check
                var detailErrorList = AbstractFunction<T, U, V>.ErrorCheck(this.ModelState);
                returnValue = ErrorCheck(request, detailErrorList, transaction);

                // If there is no error, execute the main process
                if (returnValue.Success && !request.IsOnlyValidation) returnValue = await Exec(request, transaction);
            }
        }
        catch (Exception e)
        {
            return AbstractFunction<T, U, V>.GetReturnValue(returnValue, logger, e, appDbContext);
        }

        // Processing end log
        logger.Warn(returnValue);
        return returnValue;
    }
}