using System.Diagnostics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
//
using qlsv.Models;
using qlsv.ViewModels;
using qlsv.Data;
using qlsv.Services;
using qlsv.Dto;


namespace qlsv.Teacher.Controllers;

[Area("Teacher")]
public class UserController : Controller
{
    // Variable
    private readonly ILogger<UserController> _logger;
    private readonly qlsv.Data.IdentityDbContext _identityContext;

    private readonly qlsv.Data.QuanLySinhVienDbContext _context;
    private readonly qlsv.Helpers.SecurityHelper _securityHelper;

    // Constructor
    public UserController(
        QuanLySinhVienDbContext quanLySinhVienDbContext,
        qlsv.Data.IdentityDbContext identityDbContext,
        qlsv.Helpers.SecurityHelper securityHelper)
    {
        _context = quanLySinhVienDbContext;
        _identityContext = identityDbContext;
        _securityHelper = securityHelper;
    }
    // GET: Admin/UserDetails
    public IActionResult UserDetails(string id)
    {   
        var user = _identityContext.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        UpdateUser updateUser = new UpdateUser()
        {
            Id = user.Id,
            FullName = user.FullName,
            Address = user.Address,
            ProfilePicture = user.ProfilePicture
        };
        return View(updateUser);
    }

    // POST: Identity/UpdateUser
    [HttpPost]
    public ActionResult UpdateUser(UpdateUser updateUser, string? base64_Avatar)
    {
        if (!ModelState.IsValid)
        {   
            if (base64_Avatar != null) {
                ViewBag.Base64_Avatar = base64_Avatar;
            }
            return View("UserDetails", updateUser);
        }

        var user = _identityContext.Users.FirstOrDefault(u => u.Id == updateUser.Id);
        if (user == null)
        {
            return NotFound();
        }
        user.FullName = updateUser.FullName;
        user.Address = updateUser.Address;
        _identityContext.SaveChanges();

        return RedirectToAction("UserDetails", new { id = updateUser.Id });
    }

    // POST: Identity/UpdatePhotoUser
    [HttpPost]
    public async Task<ActionResult> UpdatePhotoUser(string IdUser, IFormFile file)
    {
        const int maxFileSize = 1024 * 1024; // 2MB
        var user = await _identityContext.Users.FindAsync(IdUser);
        if (user == null)
        {
            return NotFound();
        }
        if (file == null || file.Length == 0)
        {
            return RedirectToAction("UserDetails", new { id = IdUser });
        }
        ImageService imageService = new ImageService();
        byte[] imageData = await imageService.ToByteAsync(file);

        List<string> dotImage = new List<string>() { "jfif","png", "webp", "jpeg", "jpg", "heic" };

        string[] fileExtension = file.FileName.Split(".");
        string extension = fileExtension[fileExtension.Length - 1];

        if (imageData != null && dotImage.Contains(extension))
        {
            if (imageData.Length <= maxFileSize)
            {
                user.ProfilePicture = imageData;
                _identityContext.SaveChanges();
            }
        }
        return RedirectToAction("UserDetails", new { id = IdUser });
    }

    // POST: api/sinhvien/updatepassword
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
                return NotFound("Sinh viên không tồn tại.");
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
