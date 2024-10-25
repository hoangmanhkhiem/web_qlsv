
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
//
using qlsv.Models;

namespace qlsv.Admin.Controllers;

[Area("Admin")]
public class QuanLySinhVienController : Controller
{
    // Variable
    private readonly ILogger<QuanLySinhVienController> _logger;
    

    // Constructor
    public QuanLySinhVienController(
        ILogger<QuanLySinhVienController> logger
    ) {
        _logger = logger;
    }

    /**
     * GET: /Admin/QuanLySinhVien
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
