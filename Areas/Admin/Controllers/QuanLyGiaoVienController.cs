
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using qlsv.Data;
using Microsoft.EntityFrameworkCore;

//
using qlsv.Models;
using qlsv.Services;

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


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
