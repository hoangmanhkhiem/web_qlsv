using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

//
using qlsv.Helpers;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SecurityApiController : ControllerBase
{
    // Variables
    private readonly SecurityHelper _securityHelper;

    // Constructor
    public SecurityApiController(
        SecurityHelper securityHelper
    )
    {
        _securityHelper = securityHelper;
    }
    
    /**
     * POST: Encrypt data
     */
    [HttpPost("encrypt")]
    public IActionResult Encrypt(string data)
    {
        return Ok(_securityHelper.Encrypt(data));
    }

    /**
     * POST: Decrypt data
     */
    [HttpPost("decrypt")]
    public IActionResult Decrypt(string data)
    {
        return Ok(_securityHelper.Decrypt(data));
    }

    /**
     * POST: Hash data
     */
    [HttpPost("hash")]
    public IActionResult Hash(string data)
    {
        return Ok(_securityHelper.Hash(data));
    }

    /**
     * POST: Verify hash
     */
    [HttpPost("verifyHash")]
    public IActionResult Verify(string data, string hash)
    {
        return Ok(_securityHelper.ValidateHash(data, hash));
    }
}

