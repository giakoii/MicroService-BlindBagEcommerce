using AuthService.Logics.Commons;
using AuthService.Models;
using AuthService.Models.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIdConnect.Utils.Consts;
using OpenIddict.Validation.AspNetCore;

namespace AuthService.Controllers.V1.Users;

/// <summary>
/// UserInsertController - Insert new User
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UserInsertController : ControllerBase
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public UserInsertController(AppDbContext context)
    {
        _context = context;   
    }
    
    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public async Task<UserInsertResponse> Post(UserInsertRequest request)
    {
        var response = new UserInsertResponse() { Success = false };
        var detailErrorList = new List<DetailError>();
        var customerRole = ConstantEnum.UserRole.Customer.ToString();
        
        // Check user exists
        var userExist = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName || x.Email == request.Email);
        if (userExist != null)
        {
            response.SetMessage(MessageId.E11004);
            return response;
        }
        
        // Create key
        var key = $"{request.UserName}";
        key = CommonLogic.EncryptText(key, _context);
        
        // Check role
        var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == customerRole);
        if (role == null)
        {
            response.SetMessage(MessageId.E11003);
            return response;
        }
        
        var transaction = await _context.Database.BeginTransactionAsync();

        // Insert information user
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12),
            LockoutEnabled = true,
            LockoutEnd = null,
            EmailConfirmed = false,
            Key = key,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            SecurityStamp = Guid.NewGuid().ToString(),
            TwoFactorEnabled = false,
            RoleId = role.Id,
        };
        
        // Create user
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
            
        // Send mail
        await UserInsertSendMail.SendMailVerifyInformation(_context, user.UserName, user.Email, key, detailErrorList);
        if (detailErrorList.Count > 0)
        {
            response.SetMessage(detailErrorList[0].MessageId, detailErrorList[0].ErrorMessage);
            return response;
        }
        
        // True
        await transaction.CommitAsync();
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }
}