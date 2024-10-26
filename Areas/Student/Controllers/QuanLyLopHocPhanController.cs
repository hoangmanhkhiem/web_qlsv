
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using qlsv.Data;

//
using qlsv.Models;
using qlsv.Helpers;

namespace qlsv.Student.Controllers;

[Area("Student")]
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

    /**
     * GET: /Student/QuanLyLopHocPhan
     * Home Page
     */
    public IActionResult Index()
    {   
        string idUser = GetIdUser();
        if (idUser == null)
        {
            return RedirectToAction("Index", "Login", new { area = "Identity"});
        }

        List<LopHocPhan> listLopHocPhan = (
            from sv in _context.SinhViens
            where sv.IdSinhVien == idUser
            join sv_lhp in _context.SinhVienLopHocPhans on sv.IdSinhVien equals sv_lhp.IdSinhVien
            join lhp in _context.LopHocPhans on sv_lhp.IdLopHocPhan equals lhp.IdLopHocPhan
            select lhp
        ).ToList();
        return View(listLopHocPhan);
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

        return jwtToken.Claims.FirstOrDefault(c => c.Type == "idUser")?.Value;
    }
}
