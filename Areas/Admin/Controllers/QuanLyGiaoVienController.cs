
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using qlsv.Data;
using Microsoft.EntityFrameworkCore;

//
using qlsv.Models;
using qlsv.Services;
using qlsv.Dto;
using Microsoft.AspNetCore.Identity;

namespace qlsv.Admin.Controllers;

[Area("Admin")]
public class QuanLyGiaoVienController : Controller
{
    // Variable
    private readonly ILogger<QuanLyGiaoVienController> _logger;
    private readonly QuanLySinhVienDbContext _context;
    private readonly IdentityDbContext _identityContext;

    // Constructor
    public QuanLyGiaoVienController(
        ILogger<QuanLyGiaoVienController> logger,
        QuanLySinhVienDbContext context,
        IdentityDbContext identityContext)
    {
        _logger = logger;
        _context = context;
        _identityContext = identityContext;
    }

    /**
     * GET: /Admin/QuanLyGiaoVien
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    // GET: /Admin/QuanLyGiaoVien/{id}
    public IActionResult Details(string IdGiaoVien)
    {
        var gv = _context.GiaoViens
                        .Include(x => x.Khoas)
                        .FirstOrDefault(g => g.IdGiaoVien == IdGiaoVien);

        return View(gv);
    }

    // GET: /Admin/QuanLyGiaoVien/Edit/{id}
    public IActionResult Edit(string IdGiaoVien)
    {
        var giaovien = (
            from gv in _context.GiaoViens
            where gv.IdGiaoVien == IdGiaoVien
            join khoa in _context.Khoas on gv.IdKhoa equals khoa.IdKhoa
            select new GiaoVien
            {
                IdGiaoVien = gv.IdGiaoVien,
                TenGiaoVien = gv.TenGiaoVien,
                SoDienThoai = gv.SoDienThoai,
                Email = gv.Email,
                IdKhoa = gv.IdKhoa,
                Khoas = khoa
            }
        ).FirstOrDefault();

        return View(giaovien);
    }

    // POST: /Admin/QuanLyGiaoVien/Edit/{id}
    [HttpPost]
    public IActionResult Edit(GiaoVien gv)
    {
        if (ModelState.IsValid)
        {
            _context.Update(gv);
            _context.SaveChanges();
            return RedirectToAction("Details", new { IdGiaoVien = gv.IdGiaoVien });
        }
        return View(gv);
    }

    // POST: /Admin/QuanLyGiaoVien/UpgraeAvatar/{id}
    [HttpPost]
    public async Task<IActionResult> UpgraeAvatar(string IdGiaoVien, IFormFile file)
    {
        const int maxFileSize = 1024 * 1024; // 2MB
        var user = await _identityContext.Users.FirstOrDefaultAsync(x => x.IdClaim == IdGiaoVien);
        if (user == null)
        {
            return NotFound("user not found");
        }
        if (file == null || file.Length == 0)
        {
            return RedirectToAction("Edit", new { id = IdGiaoVien });

        }
        ImageService imageService = new ImageService();
        byte[] imageData = await imageService.ToByteAsync(file);

        List<string> dotImage = new List<string>() { "png", "webp", "jpeg", "jpg", "heic" };

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
        return RedirectToAction("Edit", new { id = IdGiaoVien });
    }

    /**
     * POST: /Admin/QuanLyGiaoVien/UploadCSV
     * Upload CSV file create list giao vien
     */
    [HttpPost]
    public async Task<IActionResult> UploadCSV(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File not found");
        }

        var giaoviens = new List<GiaoVien>();
        var IdentityGiaoViens = new List<UserCustom>();
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var values = line.Split(",");
                var gv = new GiaoVienDto
                {
                    IdGiaoVien = values[0].Trim(),
                    TenGiaoVien = values[1].Trim(),
                    SoDienThoai = values[2].Trim(),
                    Email = values[3].Trim(),
                    IdKhoa = values[4].Trim()
                };
                if (GiaoVienExists(gv).Status)
                {
                    giaoviens.Add(new GiaoVien
                    {
                        IdGiaoVien = gv.IdGiaoVien,
                        TenGiaoVien = gv.TenGiaoVien,
                        SoDienThoai = gv.SoDienThoai,
                        Email = gv.Email,
                        IdKhoa = gv.IdKhoa
                    });
                    IdentityGiaoViens.Add(new UserCustom
                    {
                        IdClaim = gv.IdGiaoVien,
                        UserName = gv.IdGiaoVien,
                        Email = gv.Email,
                        PhoneNumber = gv.SoDienThoai,
                        FirstName = gv.TenGiaoVien,
                        PasswordHash = "i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=" // 123
                    });
                }
                else {
                    return BadRequest(GiaoVienExists(gv).Message);
                }
            }
        }

        _context.GiaoViens.AddRange(giaoviens);
        _context.SaveChanges();
        _identityContext.Users.AddRange(IdentityGiaoViens);
        _identityContext.SaveChanges();

        var giaovienRole = _identityContext.Roles.FirstOrDefault(r => r.Name == "GiaoVien");

        foreach (var gv in IdentityGiaoViens)
        {
            _identityContext.UserRoles.AddRange(
                new IdentityUserRole<string> { UserId = gv.Id, RoleId = giaovienRole.Id }
            );
        }
        _identityContext.SaveChanges();

        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // Helper check information of user
    private StatusUploadFileDto GiaoVienExists(GiaoVienDto giaoVienDto)
    {
        // Check id
        var id = giaoVienDto.IdGiaoVien;
        var giaovien = _identityContext.Users.FirstOrDefault(x => x.IdClaim == id);
        if (giaovien != null)
        {
            return new StatusUploadFileDto
            {
                Status = false,
                Message = "Id already exists"
            };
        }
        // Check email
        string? email = giaoVienDto.Email;
        // Check null email in identity context
        var user = _identityContext.Users.FirstOrDefault(x => x.Email == email);
        if (user != null)
        {
            return new StatusUploadFileDto
            {
                Status = false,
                Message = "Email already exists"
            };
        }
        // Check so dien thoai
        string? soDienThoai = giaoVienDto.SoDienThoai;
        var giaovienSoDienThoai = _identityContext.Users.FirstOrDefault(x => x.PhoneNumber == soDienThoai);
        if (giaovienSoDienThoai != null)
        {
            return new StatusUploadFileDto
            {
                Status = false,
                Message = "So dien thoai already exists"
            };
        }

        return new StatusUploadFileDto
        {
            Status = true,
            Message = "Success"
        };
    }


    [HttpPost]
    public async Task<ActionResult> UpdatePhotoUser(string IdUser, IFormFile file)
    {
        const int maxFileSize = 1024 * 1024 * 10; // 20MB
        var user = await _identityContext.Users.FirstOrDefaultAsync(u => u.IdClaim == IdUser);
        var userGiaoVien = _context.GiaoViens.FirstOrDefault(u => u.IdGiaoVien == IdUser);
        
        if (user == null)
        {
            return NotFound();
        }
        if (file == null || file.Length == 0)
        {
            return RedirectToAction("Edit", new { idGiaoVien = IdUser });
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
                // Convert base 64
                user.ProfilePictureBase64 = Convert.ToBase64String(imageData);
                _identityContext.SaveChanges();
            }
        }
        return RedirectToAction("Edit", new { idGiaoVien = IdUser });
    }
}
