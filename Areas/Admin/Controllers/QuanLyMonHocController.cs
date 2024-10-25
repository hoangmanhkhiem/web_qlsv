
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
//
using qlsv.Models;

namespace qlsv.Admin.Controllers;

[Area("Admin")]
public class QuanLyMonHocController : Controller
{
    // Variable
    private readonly ILogger<QuanLyMonHocController> _logger;
    

    // Constructor
    public QuanLyMonHocController(
        ILogger<QuanLyMonHocController> logger
    ) {
        _logger = logger;
    }

    /**
     * GET: /Admin/QuanLyMonHoc
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
