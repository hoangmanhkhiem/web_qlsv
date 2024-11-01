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
public class GiaoVienController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;
    private readonly SecurityHelper _securityHelper;
    private readonly IdentityDbContext _identityContext;

    // Constructor
    public GiaoVienController(
        QuanLySinhVienDbContext context,
        SecurityHelper securityHelper,
        IdentityDbContext identityContext)
    {
        _context = context;
        _securityHelper = securityHelper;
        _identityContext = identityContext;
    }

    /** 
     * GET: api/giaovien/
     * Get tat ca giao vien
     */
    [HttpGet]
    public async Task<IActionResult> GetGiaoViens()
    {
        // Query
        var query = await (
            from gv in _context.GiaoViens
            join k in _context.Khoas on gv.IdKhoa equals k.IdKhoa
            select new
            {
                IdGiaoVien = gv.IdGiaoVien,
                TenGiaoVien = gv.TenGiaoVien,
                Email = gv.Email,
                SoDienThoai = gv.SoDienThoai,
                IdKhoa = gv.IdKhoa,
                TenKhoa = k.TenKhoa
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /** 
     * GET: api/giaovien/{id}
     * Get giao vien tu id
     */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGiaoVienById(string id)
    {
        // Query
        var query = await (
            from gv in _context.GiaoViens
            where gv.IdGiaoVien == id
            join k in _context.Khoas on gv.IdKhoa equals k.IdKhoa
            select new
            {
                IdGiaoVien = gv.IdGiaoVien,
                TenGiaoVien = gv.TenGiaoVien,
                Email = gv.Email,
                SoDienThoai = gv.SoDienThoai,
                IdKhoa = gv.IdKhoa,
                TenKhoa = k.TenKhoa
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    // POST: api/giaovien/
    [HttpPost]
    public IActionResult CreateGiaoVien([FromBody] GiaoVienDto newGiaoVien)
    {
        if (newGiaoVien == null)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            // Convert the DTO to the entity model, assuming your entity model is GiaoVien
            var giaoVien = new GiaoVien
            {
                IdGiaoVien = newGiaoVien.IdGiaoVien,
                TenGiaoVien = newGiaoVien.TenGiaoVien,
                Email = newGiaoVien.Email,
                SoDienThoai = newGiaoVien.SoDienThoai,
                IdKhoa = newGiaoVien.IdKhoa
            };
            // Check duplicate ID, email, phone number
            if (_context.GiaoViens.Any(gv => gv.IdGiaoVien == giaoVien.IdGiaoVien))
            {
                return BadRequest("ID giáo viên đã tồn tại.");
            }
            if (_context.GiaoViens.Any(gv => gv.Email == giaoVien.Email))
            {
                return BadRequest("Email đã tồn tại.");
            }
            if (_context.GiaoViens.Any(gv => gv.SoDienThoai == giaoVien.SoDienThoai))
            {
                return BadRequest("Số điện thoại đã tồn tại.");
            }
            // Create user identity with default password is 123456
            // Find id role is GiaoVien
            var role = _identityContext.Roles.FirstOrDefault(r => r.Name == "GiaoVien");
            if (role == null)
            {
                return NotFound("Role GiaoVien not found.");
            }
            // Create new user
            var user = new UserCustom
            {
                IdClaim = giaoVien.IdGiaoVien,
                UserName = giaoVien.IdGiaoVien,
                PasswordHash = _securityHelper.Hash("123456"),
                FullName = giaoVien.TenGiaoVien,
                Email = giaoVien.Email,
                PhoneNumber = giaoVien.SoDienThoai,
            };
            // Add role to user
            _identityContext.UserRoles.Add(new IdentityUserRole<string>
            {
                UserId = user.Id,
                RoleId = role.Id
            });

            // Add to database
            _identityContext.Users.Add(user);
            _identityContext.SaveChanges();
            _context.GiaoViens.Add(giaoVien);
            _context.SaveChanges();

            return Ok(giaoVien); // Return success with the newly added teacher's data
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // PUT /api/giaovien/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGiaoVien(string id, [FromBody] GiaoVienDto giaoVien)
    {
        // Find the existing giao vien
        var existingGiaoVien = await _context.GiaoViens.FindAsync(id);
        if (existingGiaoVien == null)
        {
            return NotFound("Không tìm thấy giáo viên");
        }

        // Update the existing giao vien
        existingGiaoVien.TenGiaoVien = giaoVien.TenGiaoVien;
        existingGiaoVien.Email = giaoVien.Email;
        existingGiaoVien.SoDienThoai = giaoVien.SoDienThoai;
        existingGiaoVien.IdKhoa = giaoVien.IdKhoa;

        // Save changes
        await _context.SaveChangesAsync();

        // Return the updated giao vien
        return Ok(existingGiaoVien);
    }

    [HttpPost("adminupdatepassword")]
    public IActionResult UpdatePassword([FromBody] AdminUpdatePasswordDto updatePasswordDto)
    {
        // if (string.IsNullOrWhiteSpace(updatePasswordDto.NewPassword) ||
        // string.IsNullOrWhiteSpace(updatePasswordDto.OldPassword) ||
        // updatePasswordDto.NewPassword != updatePasswordDto.ConfirmPassword ||
        // updatePasswordDto.IdUser == null)
        // {
        //     return BadRequest("Invalid data.");
        // }
                if (string.IsNullOrWhiteSpace(updatePasswordDto.NewPassword) ||
        updatePasswordDto.NewPassword != updatePasswordDto.ConfirmPassword ||
        updatePasswordDto.IdUser == null)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            // Find the user in the Identity system by teacher ID
            var user = _identityContext.Users.FirstOrDefault(u => u.IdClaim == updatePasswordDto.IdUser);
            if (user == null)
            {
                return NotFound("Giáo viên không tồn tại.");
            }

            // Verify the old password
            // var passwordHasher = user.PasswordHash;
            // bool verificationResult = _securityHelper.ValidateHash(updatePasswordDto.OldPassword, passwordHasher);

            // if (verificationResult == false)
            // {
            //     return Unauthorized("Mật khẩu cũ không chính xác.");
            // }

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

