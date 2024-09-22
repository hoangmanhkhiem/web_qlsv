using Microsoft.AspNetCore.Mvc;
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
    
    // POST: /api/Login/
    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginViewModel model)
    {
        string passwordHash = _sercurityHelper.Hash(model.Password);
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
        // Test
        // return Ok($"User Name: {model.UserNameOrEmail}, PasswordHash: {passwordHash}");
    }
}

