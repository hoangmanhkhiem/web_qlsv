using Microsoft.AspNetCore.Mvc;
//
using qlsv.ViewModels;
using qlsv.Helpers;
using qlsv.Data;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JwtApiController : ControllerBase
{
    // Variables
    private readonly qlsv.Data.IdentityDbContext _context;
    private readonly SessionDbContext _session;
    private readonly JwtHelper _jwtHelper;
    private readonly SecurityHelper _sercurityHelper;

    // Constructor
    public JwtApiController(
        qlsv.Data.IdentityDbContext context,
        SessionDbContext session,
        JwtHelper jwtHelper,
        SecurityHelper securityHelper
    )
    {
        _context = context;
        _jwtHelper = jwtHelper;
        _sercurityHelper = securityHelper;
        _session = session;
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
            var jwtToken = _jwtHelper.GenerateToken(user.Id);

            return Ok(jwtToken);
        }
        return Ok("User and password not found");
    }

    // Post: Decode jwt
    [HttpPost]
    [Route("decode")]
    public ActionResult DecodeToken(string token)
    {
        return Ok(_jwtHelper.DecodeToken(token));
    }

    // Post: Refresh jwt
    [HttpPost]
    [Route("refresh")]
    public ActionResult Refresh(string token)
    {
        return Ok("TODO add");
    }

    // Post: Check jwt
    [HttpPost("check")]
    public ActionResult Check(string token)
    {
        return Ok("TODO add");
    }
}

