
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
