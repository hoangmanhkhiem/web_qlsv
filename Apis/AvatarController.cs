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
        byte[] bytes = (from a in _context.Users
                        where a.IdClaim == idUserClaim
                        select a.ProfilePicture
                        ).FirstOrDefault();

        if (bytes == null)
        {
            return NotFound("Avatar not found");
        }

        // Serialize the byte array to JSON
        var json = JsonSerializer.Serialize(bytes);

        return Ok(
            json
        );
    }
}

