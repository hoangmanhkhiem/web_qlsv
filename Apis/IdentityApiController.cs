using Microsoft.AspNetCore.Mvc;
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
}

