using System.Diagnostics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
//
using qlsv.Models;
using qlsv.ViewModels;
using qlsv.Data;
using qlsv.Services;
using qlsv.Helpers;


namespace qlsv.Student.Controllers;

[Area("Student")]
public class UserController : Controller
{
    // Variable
    private readonly ILogger<UserController> _logger;
    private readonly JwtHelper _jwtHelper;
    private readonly qlsv.Data.QuanLySinhVienDbContext _context;

    private readonly qlsv.Data.IdentityDbContext _identityContext;

    // Constructor
    public UserController(
        ILogger<UserController> logger,
        qlsv.Data.QuanLySinhVienDbContext context,
        JwtHelper jwtHelper,
        qlsv.Data.IdentityDbContext contextIdentity)
    {
        _logger = logger;
        _context = context;
        _identityContext = contextIdentity;
        _jwtHelper = jwtHelper;
    }

    // GET: Admin/UserDetails
    public IActionResult UserDetails(string id)
    {   
        var accessToken = HttpContext.Request.Cookies["AccsessToken"];
        if (accessToken == null)
        {
            return RedirectToAction("Index", "Login", new { area = "Identity" });
        }

        // Get iduser info from token
        var jwtToken = _jwtHelper.DecodeToken(accessToken);

        string? idUser = jwtToken.Claims.FirstOrDefault(c => c.Type == "idClaim")?.Value;
        var user = _context.SinhViens.FirstOrDefault(u => u.IdSinhVien == idUser);
        var userIdentity = _identityContext.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        if (userIdentity == null)
        {
            return NotFound();
        }
        UpdateUser updateUser = new UpdateUser()
        {
            Id = userIdentity.Id,
            FullName = user.HoTen,
            Lop = user.Lop,
            Address = user.DiaChi,
            NgaySinh = user.NgaySinh,
            ProfilePicture = userIdentity.ProfilePicture
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
        var user = _context.SinhViens.FirstOrDefault(u => u.IdSinhVien == updateUser.Id);
        var userIdentity = _identityContext.Users.FirstOrDefault(u => u.IdClaim == updateUser.Id);
        if (user == null)
        {
            return NotFound();
        }
        user.DiaChi = updateUser.Address;
        user.NgaySinh = updateUser.NgaySinh;
        user.Lop = updateUser.Lop;
        userIdentity.FullName = updateUser.FullName;
        
        _context.SaveChanges();
        _identityContext.SaveChanges();

        return RedirectToAction("UserDetails", new { id = userIdentity.Id });
    }

    // POST: Identity/UpdatePhotoUser
    [HttpPost]
    public async Task<ActionResult> UpdatePhotoUser(string IdUser, IFormFile file)
    {
        const int maxFileSize = 1024 * 1024; // 2MB
        var user = await _identityContext.Users.FindAsync(IdUser);
        var userSinhVien = _context.SinhViens.FirstOrDefault(u => u.IdSinhVien == IdUser);
        
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
