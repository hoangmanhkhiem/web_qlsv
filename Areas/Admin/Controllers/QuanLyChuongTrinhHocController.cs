
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
//
using qlsv.Models;

namespace qlsv.Admin.Controllers;

[Area("Admin")]
public class QuanLyChuongTrinhHocController : Controller
{
    // Variable
    private readonly ILogger<QuanLyChuongTrinhHocController> _logger;
    

    // Constructor
    public QuanLyChuongTrinhHocController(
        ILogger<QuanLyChuongTrinhHocController> logger
    ) {
        _logger = logger;
    }

    /**
     * GET: /Admin/QuanLyChuongTrinhHoc
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
