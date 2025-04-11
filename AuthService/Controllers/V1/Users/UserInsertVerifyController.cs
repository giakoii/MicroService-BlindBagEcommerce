using AuthService.Logics.Commons;
using AuthService.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIdConnect.Utils.Consts;

namespace AuthService.Controllers.V1.Users;

/// <summary>
/// UserInsertVerifyController - Verify the user registration
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UserInsertVerifyController : ControllerBase
{
    private readonly AppDbContext _context;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public UserInsertVerifyController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<UserInsertVerifyResponse> Post(UserInsertVerifyRequest request)
    {
        var response = new UserInsertVerifyResponse() { Success = false };
        // Decrypt
        var keyDecrypt = CommonLogic.DecryptText(request.Key, _context);
        string[] values = keyDecrypt.Split(",");
        string userNameDecrypt = values[0];
        
        // Check user exists
        var userExist = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userNameDecrypt && 
                                                           x.EmailConfirmed == false &&
                                                           x.LockoutEnabled == true &&
                                                           x.Key == request.Key);
        if (userExist == null)
        {
            response.SetMessage(MessageId.E11001);
            return response;
        }
        
        var transaction = await _context.Database.BeginTransactionAsync();
        
        // Update information user
        userExist.Key = null;
        userExist.LockoutEnd = null;
        userExist.LockoutEnabled = false;
        userExist.EmailConfirmed = true;

        _context.Users.Update(userExist);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }
}