using Microsoft.AspNetCore.Mvc;
//
using qlsv.ViewModels;
using qlsv.Helpers;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JwtApiController : ControllerBase
{   
    // Variables
    private readonly qlsv.Data.IdentityDbContext _context;
    private readonly JwtHelper _jwtHelper;
    private readonly SecurityHelper _sercurityHelper;

    // Constructor
    public JwtApiController(
        qlsv.Data.IdentityDbContext context,
        JwtHelper jwtHelper,
        SecurityHelper securityHelper  
    ) {
        _context = context;
        _jwtHelper = jwtHelper;
        _sercurityHelper = securityHelper;
    }
    
    // Post: Create jwt
    [HttpPost("create")]
    public ActionResult Create([FromBody] LoginViewModel model)
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
            string jwtToken = _jwtHelper.GenerateJwtToken(
                user.Id, 
                user.UserName,
                null
            );

            return Ok(jwtToken);
        }
        return Ok("User and password not found");
    }

    // Post: Revoke jwt
    [HttpPost]
    [Route("revoke")]
    public ActionResult Revoke(string userId)
    {
        _jwtHelper.RevokeToken(userId);
        var listRefreshToken = _context.RefreshTokens.ToList();
        string res = "";
        foreach (var item in listRefreshToken)
        {
            res += item.Id + " " + item.UserId + " " + item.Token + " " + item.ExpiryDate + "\n";
        }
        return Ok(res);
    }

    // Post: Refresh jwt
    [HttpPost]
    [Route("refresh")]
    public ActionResult Refresh(string token)
    {
        string jwtToken = _jwtHelper.RefreshJwtToken(token);
        return Ok(jwtToken);
    }

    // Post: Check jwt
    [HttpPost("check")]
    public ActionResult Check(string token)
    {
        (bool c, string newToken) = _jwtHelper.CheckJwtToken(token);
        return Ok($"{c}, {newToken}");
    }
}

