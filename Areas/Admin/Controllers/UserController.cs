using System.Diagnostics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
//
using qlsv.Models;
using qlsv.ViewModels;
using qlsv.Data;
using qlsv.Services;
using qlsv.Dto;


namespace qlsv.Admin.Controllers;

[Area("Admin")]
public class UserController : Controller
{
    // Variable
    private readonly ILogger<UserController> _logger;
    private readonly qlsv.Data.IdentityDbContext _context;

    // Constructor
    public UserController(
        ILogger<UserController> logger,
        qlsv.Data.IdentityDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // GET: Admin/UserDetails
    public IActionResult UserDetails(string id)
    {   
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        UpdateRootDto updateUser = new UpdateRootDto()
        {
            Id = user.Id,
            FullName = user.FirstName + " " + user.LastName,
            Address = user.Address,
            ProfilePicture = user.ProfilePicture
        };
        return View(updateUser);
    }

    // POST: Identity/UpdateUser
    [HttpPost]
    public ActionResult UpdateUser(UpdateRootDto updateUser, string? base64_Avatar)
    {
        if (!ModelState.IsValid)
        {   
            if (base64_Avatar != null) {
                ViewBag.Base64_Avatar = base64_Avatar;
            }
            return View("UserDetails", updateUser);
        }

        var user = _context.Users.FirstOrDefault(u => u.Id == updateUser.Id);
        if (user == null)
        {
            return NotFound();
        }
        user.FullName = updateUser.FullName;
        user.Address = updateUser.Address;
        _context.SaveChanges();

        return RedirectToAction("UserDetails", new { id = updateUser.Id });
    }

    // POST: Identity/UpdatePhotoUser
    [HttpPost]
    public async Task<ActionResult> UpdatePhotoUser(string IdUser, IFormFile file)
    {
        const int maxFileSize = 1024 * 1024; // 2MB
        var user = await _context.Users.FindAsync(IdUser);
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
                _context.SaveChanges();
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
