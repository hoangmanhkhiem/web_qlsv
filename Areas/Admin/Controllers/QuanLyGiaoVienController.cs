
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using qlsv.Data;
using Microsoft.EntityFrameworkCore;

//
using qlsv.Models;

namespace qlsv.Admin.Controllers;

[Area("Admin")]
public class QuanLyGiaoVienController : Controller
{
    // Variable
    private readonly ILogger<QuanLyGiaoVienController> _logger;
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public QuanLyGiaoVienController(
        ILogger<QuanLyGiaoVienController> logger,
        QuanLySinhVienDbContext context)
    {
        _logger = logger;
        _context = context;
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
    public IActionResult Details(string id)
    {
        var gv = _context.GiaoViens
                        .Include(x => x.Khoas)
                        .FirstOrDefault(g => g.IdGiaoVien == id);

        return View(gv);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
