
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


using qlsv.Dto;
using qlsv.Models;
using qlsv.Data;
using qlsv.Services;

namespace qlsv.Admin.Controllers;

[Area("Admin")]
public class QuanLySinhVienController : Controller
{
    // Variable
    private readonly ILogger<QuanLySinhVienController> _logger;
    private readonly QuanLySinhVienDbContext _context;

    private readonly IdentityDbContext _identityContext;

    // Constructor
    public QuanLySinhVienController(
        ILogger<QuanLySinhVienController> logger,
        QuanLySinhVienDbContext context,
        IdentityDbContext identityContext
        )
    {
        _logger = logger;
        _context = context;
        _identityContext = identityContext;
    }

    /**
     * GET: /Admin/QuanLySinhVien
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    /**
     * GET: /Admin/QuanLySinhVien/Details?IdSinhVien={IdSinhVien}
     * get details sinh vien
     */
    public IActionResult Details(string IdSinhVien)
    {
        SinhVien? sv = _context.SinhViens
            .Include(x => x.ChuongTrinhHocs)
            .Include(x => x.Khoas)
            .FirstOrDefault(x => x.IdSinhVien == IdSinhVien);
        if (sv == null)
        {
            return NotFound();
        }

        return View(sv);
    }

    /**
     * GET: /Admin/QuanLySinhVie/Edit?IdSinhVien={IdSinhVien}
     * Edit sinh vien information
     */
    public IActionResult Edit(string IdSinhVien)
    {
        SinhVien? sv = _context.SinhViens
            .Include(x => x.ChuongTrinhHocs)
            .Include(x => x.Khoas)
            .FirstOrDefault(x => x.IdSinhVien == IdSinhVien);
        if (sv == null)
        {
            return NotFound();
        }

        return View(sv);
    }

    /**
     * POST: /Admin/QuanLySinhVien/Edit
     * POST Edit sinh vien information
     */
    [HttpPost]
    public async Task<IActionResult> Edit(SinhVien sv)
    {
        var qr = _context.SinhViens
            .Include(x => x.ChuongTrinhHocs)
            .Include(x => x.Khoas)
            .FirstOrDefault(x => x.IdSinhVien == sv.IdSinhVien);
        if (qr == null)
        {
            return NotFound();
        }

        qr.HoTen = sv.HoTen;
        qr.Lop = sv.Lop;
        qr.DiaChi = sv.DiaChi;
        qr.NgaySinh = sv.NgaySinh;

        _context.SaveChanges();
        return RedirectToAction("Details", new { IdSinhVien = sv.IdSinhVien });
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

        var sinhviens = new List<SinhVien>();
        var _IdentitySinhviens = new List<UserCustom>();
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var values = line.Split(",");
                var idCTH = _context.ChuongTrinhHocs.FirstOrDefault(x => x.TenChuongTrinhHoc == values[6].Trim());
                if (idCTH == null)
                {
                    return BadRequest("Chuong trinh hoc not found");
                }
                var sv = new SinhVienDto
                {
                    IdSinhVien = values[0].Trim(),
                    HoTen = values[1].Trim(),
                    Lop = values[2].Trim(),
                    NgaySinh = DateTime.Parse(values[3].Trim()),
                    DiaChi = values[4].Trim(),
                    IdKhoa = values[5].Trim(),
                    IdChuongTrinhHoc = idCTH.IdChuongTrinhHoc,
                    Email = values[7].Trim(),
                    SoDienThoai = values[8].Trim()
                };
                if (SinhVienExists(sv).Status)
                {
                    sinhviens.Add(new SinhVien
                    {
                        IdSinhVien = sv.IdSinhVien,
                        HoTen = sv.HoTen,
                        Lop = sv.Lop,
                        NgaySinh = sv.NgaySinh,
                        DiaChi = sv.DiaChi,
                        IdKhoa = sv.IdKhoa,
                        IdChuongTrinhHoc = sv.IdChuongTrinhHoc
                    });
                    _IdentitySinhviens.Add(new UserCustom
                    {
                        IdClaim = sv.IdSinhVien,
                        UserName = sv.IdSinhVien,
                        FullName = sv.HoTen,
                        Email = sv.Email,
                        PhoneNumber = sv.SoDienThoai,
                        PasswordHash = "i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=" // 123
                    });
                }
                else
                {
                    return BadRequest(SinhVienExists(sv).Message);
                }
            }
        }

        _context.SinhViens.AddRange(sinhviens);
        _context.SaveChanges();
        _identityContext.Users.AddRange(_IdentitySinhviens);
        _identityContext.SaveChanges();

        // LinQ gan role cho user trong identity

        var sinhvienRole = _identityContext.Roles.FirstOrDefault(r => r.Name == "SinhVien");

        foreach (var sv in _IdentitySinhviens)
        {
            _identityContext.UserRoles.AddRange(
                new IdentityUserRole<string> { UserId = sv.Id, RoleId = sinhvienRole.Id }
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
    private StatusUploadFileDto SinhVienExists(SinhVienDto _sinhVien)
    {
        // Check id
        var id = _sinhVien.IdSinhVien;
        var sinhVien = _identityContext.Users.FirstOrDefault(x => x.IdClaim == id);
        if (sinhVien != null)
        {
            return new StatusUploadFileDto
            {
                Status = false,
                Message = "Id already exists"
            };
        }
        // Check email
        string? email = _sinhVien.Email;
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
        string? soDienThoai = _sinhVien.SoDienThoai;
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
        const int maxFileSize = 1024 * 1024; // 2MB
        var user = await _identityContext.Users.FirstOrDefaultAsync(u => u.IdClaim == IdUser);
        var userSinhVien = _context.SinhViens.FirstOrDefault(u => u.IdSinhVien == IdUser);

        if (user == null)
        {
            TempData["MessageUpLoadAvatar"] = "Don't find user upgrade avatar.";
            TempData["StatusUpdateAvatar"] = false;
            return NotFound();
        }
        if (file == null || file.Length == 0)
        {
            TempData["MessageUpLoadAvatar"] = "File not found.";
            TempData["StatusUpdateAvatar"] = false;
            return RedirectToAction("Edit", new { idSinhVien = IdUser });
        }
        ImageService imageService = new ImageService();
        byte[] imageData = await imageService.ToByteAsync(file);

        List<string> dotImage = new List<string>() { "jfif", "png", "webp", "jpeg", "jpg", "heic" };

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
                TempData["MessageUpLoadAvatar"] = "File Upload Successful.";
                TempData["StatusUpdateAvatar"] = true;
            }
            else
            {
                TempData["MessageUpLoadAvatar"] = "Please Upload A Picture Smaller Than 2 MB.";
                TempData["StatusUpdateAvatar"] = false;
            }
        }
        else
        {
            TempData["MessageUpLoadAvatar"] = "File Upload Failed, Please Upload A Picture.";
            TempData["StatusUpdateAvatar"] = false;
        }

        return RedirectToAction("Edit", new { idSinhVien = IdUser });
    }
}
