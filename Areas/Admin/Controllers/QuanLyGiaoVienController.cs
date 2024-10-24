
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
//
using qlsv.Models;

namespace qlsv.Admin.Controllers;

[Area("Admin")]
public class QuanLyGiaoVienController : Controller
{
    // Variable
    private readonly ILogger<QuanLyGiaoVienController> _logger;
    

    // Constructor
    public QuanLyGiaoVienController(
        ILogger<QuanLyGiaoVienController> logger
    ) {
        _logger = logger;
    }

    /**
     * GET: /Admin/QuanLyGiaoVien
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
