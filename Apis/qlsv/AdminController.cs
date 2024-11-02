using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

//
using qlsv.Data;
using qlsv.Models;
using qlsv.Dto;
using qlsv.Helpers;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AdminController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;
    private readonly SecurityHelper _securityHelper;
    private readonly IdentityDbContext _identityContext;

    // Constructor
    public AdminController(
        QuanLySinhVienDbContext context,
        SecurityHelper securityHelper,
        IdentityDbContext identityContext)
    {
        _context = context;
        _securityHelper = securityHelper;
        _identityContext = identityContext;
    }

    [HttpPost("updatepassword")]
    public IActionResult UpdatePassword([FromBody] UpdatePasswordDto updatePasswordDto)
    {
        if (string.IsNullOrWhiteSpace(updatePasswordDto.NewPassword) ||
        string.IsNullOrWhiteSpace(updatePasswordDto.OldPassword) ||
        updatePasswordDto.NewPassword != updatePasswordDto.ConfirmPassword ||
        updatePasswordDto.IdUser == null)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            // Find the user in the Identity system by teacher ID
            var user = _identityContext.Users.FirstOrDefault(u => u.Id == updatePasswordDto.IdUser);
            if (user == null)
            {
                return NotFound("Giáo viên không tồn tại.");
            }

            // Verify the old password
            var passwordHasher = user.PasswordHash;
            bool verificationResult = _securityHelper.ValidateHash(updatePasswordDto.OldPassword, passwordHasher);

            if (verificationResult == false)
            {
                return Unauthorized("Mật khẩu cũ không chính xác.");
            }

            // Hash the new password and update the user's PasswordHash field
            user.PasswordHash = _securityHelper.Hash(updatePasswordDto.NewPassword);

            // Save changes to the Identity database
            _identityContext.Users.Update(user);
            _identityContext.SaveChanges();

            return Ok("Password updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
    
}