using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
//
using qlsv.ViewModels;
using qlsv.Helpers;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityApiController : ControllerBase
{   
    // Variables
    private readonly qlsv.Data.IdentityDbContext _context;
    private readonly JwtHelper _jwtHelper;

    // Constructor
    public IdentityApiController(
        qlsv.Data.IdentityDbContext context,
        JwtHelper jwtHelper
    ) {
        _context = context;
        _jwtHelper = jwtHelper;
    }
    
    // POST: /api/Login/
    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginViewModel model)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
        var user = _context.Users.FirstOrDefault(
            u => 
                (u.UserName.ToUpper() == model.UserNameOrEmail.ToUpper() ||
                u.Email.ToUpper() == model.UserNameOrEmail.ToUpper())
                && u.PasswordHash == passwordHash
        );
        if (user != null)
        {
            string jwtToken = _jwtHelper.GenerateJwtToken(user.UserName);
            return Ok(jwtToken);
        }
        return Ok("User and password not found");
    }
}

