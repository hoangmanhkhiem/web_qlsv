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
    ) {
        _context = context;
        _jwtHelper = jwtHelper;
        _sercurityHelper = securityHelper;
    }
    
    // GET: Users
    [HttpGet("users")]
    public IActionResult GetUsers() {
        var listUsers = _context.Users.ToList();
        var userDtos = listUsers.Select(user => new UserCustom {
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
    public IActionResult GetRoles() {
        var listRole = _context.Roles.ToList();
        var roleDtos = listRole.Select(role => new IdentityRole {
            Id = role.Id,
            Name = role.Name,
            NormalizedName = role.NormalizedName,
            ConcurrencyStamp = role.ConcurrencyStamp
        }).ToList();
        return Ok(roleDtos);
    }

    // GET: UserRoles
    [HttpGet("userroles")]
    public IActionResult GetUserRoles() {
        var listUserRoles = _context.UserRoles.ToList();
        var userRoles = listUserRoles.Select(userRole => new IdentityUserRole<string> {
            UserId = userRole.UserId,
            RoleId = userRole.RoleId,
        }).ToList();
        return Ok(userRoles);
    }

    
}

