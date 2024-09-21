using Microsoft.AspNetCore.Mvc;
//
using qlsv.ViewModels;
namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityApiController : ControllerBase
{
    // POST: /api/Login/
    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginViewModel model)
    {
        // TODO code
        return Ok("Api login route ok");
    }
}

