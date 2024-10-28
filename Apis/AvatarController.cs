using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;

//
using qlsv.Data;
using qlsv.Models;
using Microsoft.EntityFrameworkCore;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AvatarController : ControllerBase
{
    // Variables
    private readonly qlsv.Data.IdentityDbContext _context;

    // Constructor
    public AvatarController(
        qlsv.Data.IdentityDbContext context)
    {
        _context = context;
    }

    // GET: Get avatar for user with idUserClaim
    [HttpGet("user/{idUserClaim}")]
    public async Task<IActionResult> GetAvatar(string idUserClaim)
    {
        var stringBase64 = (
            from us in _context.Users
            where us.IdClaim == idUserClaim
            select new {
                ProfilePictureBase64 = us.ProfilePictureBase64
            }
        ).FirstOrDefault();
        if (stringBase64 == null)
        {
            return NotFound();
        }
        return Ok(stringBase64);
    }

    // PUT: Update avatar for user with idUserClaim
    [HttpPut("user/{idUserClaim}")]
    public async Task<IActionResult> PutAvatar(string idUserClaim, [FromBody] string stringBase64)
    {
        var user = (
            from us in _context.Users
            where us.IdClaim == idUserClaim
            select us
        ).FirstOrDefault();
        if (user == null)
        {
            return NotFound();
        }
        // Check if stringBase64 is img/png
        if (stringBase64 == null)
        {
            return BadRequest();
        }
        if (stringBase64.Length < 22)
        {
            return BadRequest();
        }
        if (stringBase64.Substring(0, 22) != "data:image/png;base64,")
        {
            return BadRequest("Invalid base64 string parameter format img/png");  
        }
        user.ProfilePictureBase64 = stringBase64;
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: Delete avatar for user with idUserClaim
    [HttpDelete("user/{idUserClaim}")]
    public async Task<IActionResult> DeleteAvatar(string idUserClaim)
    {
        var user = (
            from us in _context.Users
            where us.IdClaim == idUserClaim
            select us
        ).FirstOrDefault();
        if (user == null)
        {
            return NotFound();
        }
        user.ProfilePictureBase64 = null;
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

