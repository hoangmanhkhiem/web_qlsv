using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
//
using qlsv.ViewModels;
using qlsv.Helpers;
using qlsv.Models;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityApiController : ControllerBase
{
    // Variables
    private readonly qlsv.Data.IdentityDbContext _context;
    private readonly JwtHelper _jwtHelper;
    private readonly SecurityHelper _sercurityHelper;

    // Constructor
    public IdentityApiController(
        qlsv.Data.IdentityDbContext context,
        JwtHelper jwtHelper,
        SecurityHelper securityHelper
    )
    {
        _context = context;
        _jwtHelper = jwtHelper;
        _sercurityHelper = securityHelper;
    }

    // GET: Users
    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        var listUsers = _context.Users.ToList();
        var userDtos = listUsers.Select(user => new UserCustom
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Address = user.Address,
            Phone = user.Phone,
            ProfilePicture = user.ProfilePicture
        }).ToList();
        return Ok(userDtos);
    }

    // GET: Roles
    [HttpGet("roles")]
    public IActionResult GetRoles()
    {
        var listRole = _context.Roles.ToList();
        var roleDtos = listRole.Select(role => new IdentityRole
        {
            Id = role.Id,
            Name = role.Name,
            NormalizedName = role.NormalizedName,
            ConcurrencyStamp = role.ConcurrencyStamp
        }).ToList();
        return Ok(roleDtos);
    }

    // GET: UserRoles
    [HttpGet("userroles")]
    public IActionResult GetUserRoles()
    {
        var listUserRoles = _context.UserRoles.ToList();
        var userRoles = listUserRoles.Select(userRole => new IdentityUserRole<string>
        {
            UserId = userRole.UserId,
            RoleId = userRole.RoleId,
        }).ToList();
        return Ok(userRoles);
    }

    // GET: RefreshTokens
    [HttpGet("refreshtokens")]
    public IActionResult GetRefreshTokens()
    {
        var listRefreshToken = _context.RefreshTokens.ToList();
        var refreshTokens = listRefreshToken.Select(refreshToken => new RefreshToken
        {
            Id = refreshToken.Id,
            UserId = refreshToken.UserId,
            Token = refreshToken.Token,
            ExpiryDate = refreshToken.ExpiryDate,
        }).ToList();
        return Ok(refreshTokens);
    }

    // GET: RoleClaims
    [HttpGet("roleclaims")]
    public IActionResult GetRoleClaims()
    {
        var listRoleClaims = _context.RoleClaims.ToList();
        var roleClaims = listRoleClaims.Select(roleClaim => new IdentityRoleClaim<string>
        {
            Id = roleClaim.Id,
            RoleId = roleClaim.RoleId,
            ClaimType = roleClaim.ClaimType,
            ClaimValue = roleClaim.ClaimValue,
        }).ToList();
        return Ok(roleClaims);
    }

    // GET: UserClaims
    [HttpGet("userclaims")]
    public IActionResult GetUserClaims()
    {
        var listUserClaims = _context.UserClaims.ToList();
        var userClaims = listUserClaims.Select(userClaim => new IdentityUserClaim<string>
        {
            Id = userClaim.Id,
            UserId = userClaim.UserId,
            ClaimType = userClaim.ClaimType,
            ClaimValue = userClaim.ClaimValue,
        }).ToList();
        return Ok(userClaims);
    }

    // GET: UserLogins
    [HttpGet("userlogins")]
    public IActionResult GetUserLogins()
    {
        var listUserLogins = _context.UserLogins.ToList();
        var userLogins = listUserLogins.Select(userLogin => new IdentityUserLogin<string>
        {
            LoginProvider = userLogin.LoginProvider,
            ProviderKey = userLogin.ProviderKey,
            UserId = userLogin.UserId,
        }).ToList();
        return Ok(userLogins);
    }

    // GET: UserTokens
    [HttpGet("usertokens")]
    public IActionResult GetUserTokens()
    {
        var listUserTokens = _context.UserTokens.ToList();
        var userTokens = listUserTokens.Select(userToken => new IdentityUserToken<string>
        {
            UserId = userToken.UserId,
            LoginProvider = userToken.LoginProvider,
            Name = userToken.Name,
            Value = userToken.Value,
        }).ToList();
        return Ok(userTokens);
    }

    // POST: UpdateUser
    [HttpPost("updateuser")]
    public IActionResult UpdateUser(UserCutomInputJson model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = _context.Users.FirstOrDefault(user => user.Id == model.Id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        user.UserName = model.UserName;
        user.Email = model.Email;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Address = model.Address;
        user.Phone = model.Phone;
        _context.SaveChanges();
        return Ok();
    }
}

