using AuthService.Models.Helpers;
using Client.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using OpenIdConnect.Controllers;

namespace AuthService.Controllers;

public abstract class AbstractApiAsyncControllerNotToken<T, U, V> : ControllerBase
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