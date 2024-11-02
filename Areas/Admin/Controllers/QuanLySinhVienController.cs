
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//
using qlsv.Models;
using qlsv.Data;

namespace qlsv.Admin.Controllers;

[Area("Admin")]
public class QuanLySinhVienController : Controller
{
    // Variable
    private readonly ILogger<QuanLySinhVienController> _logger;
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public QuanLySinhVienController(
        ILogger<QuanLySinhVienController> logger,
        QuanLySinhVienDbContext context)
    {
        _logger = logger;
        _context = context;
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
