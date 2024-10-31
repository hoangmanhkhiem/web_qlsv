
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
//
using qlsv.Data;
using qlsv.Models;
using qlsv.Helpers;

namespace qlsv.Student.Controllers;

[Area("Student")]
public class DangKyNguyenVongController : Controller
{
    // Variable
    private readonly ILogger<DangKyNguyenVongController> _logger;
    private readonly QuanLySinhVienDbContext _context;
    private readonly JwtHelper _jwtHelper;

    // Constructor
    public DangKyNguyenVongController(
        ILogger<DangKyNguyenVongController> logger,
        QuanLySinhVienDbContext context,
        JwtHelper jwtHelper)
    {
        _logger = logger;
        _context = context;
        _jwtHelper = jwtHelper;
    }

    /**
     * GET: /Student/DangKyNguyenVong
     * Home Page
     */
    public IActionResult Index()
    {
        string idUser = GetIdUser();
        if (idUser == null)
        {
            return RedirectToAction("Index", "Login", new { area = "Identity" });
        }

        SinhVien sinhVien = _context.SinhViens.FirstOrDefault(s => s.IdSinhVien == idUser);
        if (sinhVien == null)
        {
            return RedirectToAction("Index", "Login", new { area = "Identity" });
        }

        return View(sinhVien);

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // Helper get id user 
    private string GetIdUser()
    {
        // Get token from cookie
        var accessToken = HttpContext.Request.Cookies["AccsessToken"];
        if (accessToken == null)
        {
            return null;
        }

        // Get iduser info from token
        var jwtToken = _jwtHelper.DecodeToken(accessToken);

        return jwtToken.Claims.FirstOrDefault(c => c.Type == "idClaim")?.Value;
    }
}
