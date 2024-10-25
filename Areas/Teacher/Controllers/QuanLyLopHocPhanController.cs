using System.Diagnostics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//
using qlsv.Models;
using qlsv.ViewModels;
using qlsv.Data;
using qlsv.Helpers;


namespace qlsv.Teacher.Controllers;

[Area("Teacher")]
public class QuanLyLopHocPhanController : Controller
{
    // Variable
    private readonly ILogger<QuanLyLopHocPhanController> _logger;
    private readonly QuanLySinhVienDbContext _context;
    private readonly JwtHelper _jwtHelper;

    // Constructor
    public QuanLyLopHocPhanController(
        ILogger<QuanLyLopHocPhanController> logger,
        QuanLySinhVienDbContext context,
        JwtHelper jwtHelper)
    {
        _logger = logger;
        _context = context;
        _jwtHelper = jwtHelper;
    }

    // GET: Teacher/QuanLyLopHocPhan/Index
    public IActionResult Index()
    {
        var accessToken = HttpContext.Request.Cookies["AccsessToken"];
        var jwtToken = _jwtHelper.DecodeToken(accessToken);
        string idUser = jwtToken.Claims.FirstOrDefault(c => c.Type == "idUser")?.Value;

        List<LopHocPhan> lopHocPhans = _context.LopHocPhans
            .Include(lhp => lhp.MonHocs)
            .Include(lhp => lhp.GiaoViens)
            .Where(lhp => lhp.IdGiaoVien == idUser)
            .ToList();

        return View(lopHocPhans);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
