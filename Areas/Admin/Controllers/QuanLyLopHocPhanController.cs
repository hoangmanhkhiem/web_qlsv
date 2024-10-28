
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
//
using qlsv.Models;

namespace qlsv.Admin.Controllers;

[Area("Admin")]
public class QuanLyLopHocPhanController : Controller
{
    // Variable
    private readonly ILogger<QuanLyLopHocPhanController> _logger;
    

    // Constructor
    public QuanLyLopHocPhanController(
        ILogger<QuanLyLopHocPhanController> logger
    ) {
        _logger = logger;
    }

    /**
     * GET: /Admin/QuanLyLopHocPhan
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    // GET: /Admin/QuanLyLopHocPhan/Details?IdGiaoVien={id}
    public IActionResult Details(string IdGiaoVien)
    {  
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
