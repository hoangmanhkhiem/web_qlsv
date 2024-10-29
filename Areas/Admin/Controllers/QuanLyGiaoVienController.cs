
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using qlsv.Data;
using Microsoft.EntityFrameworkCore;

//
using qlsv.Models;
using qlsv.Services;
using qlsv.Dto;

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
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var values = line.Split(",");
                var gv = new GiaoVienDto
                {
                    IdGiaoVien = values[0],
                    TenGiaoVien = values[1],
                    SoDienThoai = values[2],
                    Email = values[3],
                    IdKhoa = values[4]
                };
                // giaoviens.Add(gv);
                Console.WriteLine(gv);
            }
        }

        // _context.GiaoViens.AddRange(giaoviens);
        // _context.SaveChanges();

        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
