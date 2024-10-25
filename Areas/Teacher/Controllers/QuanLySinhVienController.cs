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
public class QuanLySinhVienController : Controller
{
    // Variable
    private readonly ILogger<QuanLySinhVienController> _logger;
    private readonly QuanLySinhVienDbContext _context;
    private readonly JwtHelper _jwtHelper;

    // Constructor
    public QuanLySinhVienController(
        ILogger<QuanLySinhVienController> logger,
        QuanLySinhVienDbContext context,
        JwtHelper jwtHelper)
    {
        _logger = logger;
        _context = context;
        _jwtHelper = jwtHelper;
    }

    // GET: Teacher/QuanLySinhVien/Index
    public IActionResult Index()
    {
        var accessToken = HttpContext.Request.Cookies["AccsessToken"];
        var jwtToken = _jwtHelper.DecodeToken(accessToken);
        string idUser = jwtToken.Claims.FirstOrDefault(c => c.Type == "idUser")?.Value;

        List<SinhVien> sinhViens = (from gv in _context.GiaoViens
                                    join lhp in _context.LopHocPhans on gv.IdGiaoVien equals lhp.IdGiaoVien
                                    join sv_lhp in _context.SinhVienLopHocPhans on lhp.IdLopHocPhan equals sv_lhp.IdLopHocPhan
                                    join sv in _context.SinhViens on sv_lhp.IdSinhVien equals sv.IdSinhVien
                                    where gv.IdGiaoVien == idUser
                                    select sv
                                    ).ToList();

        return View(sinhViens);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
