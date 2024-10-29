
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
//
using qlsv.Models;

namespace qlsv.Admin.Controllers;

[Area("Admin")]
public class QuanLyKhoaController : Controller
{
    // Variable
    private readonly ILogger<QuanLyKhoaController> _logger;
    

    // Constructor
    public QuanLyKhoaController(
        ILogger<QuanLyKhoaController> logger
    ) {
        _logger = logger;
    }

    /**
     * GET: /Admin/QuanLyKhoa
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    /**
     * GET: /Admin/QuanLyKhoa/Deatail
     * Xem thong tin chi tiet trong khoa
     */
    public IActionResult Details(string idKhoa){
        
        ViewBag.Id = idKhoa;
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
