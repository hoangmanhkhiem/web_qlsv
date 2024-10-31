
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using qlsv.Data;

//
using qlsv.Models;
using qlsv.Helpers;

namespace qlsv.Student.Controllers;

[Area("Student")]
public class QuanLyDiemController : Controller
{
    // Variable
    private readonly ILogger<QuanLyDiemController> _logger;
    private readonly QuanLySinhVienDbContext _context;
    private readonly JwtHelper _jwtHelper;

    // Constructor
    public QuanLyDiemController(
        ILogger<QuanLyDiemController> logger,
        QuanLySinhVienDbContext context,
        JwtHelper jwtHelper)
    {
        _logger = logger;
        _context = context;
        _jwtHelper = jwtHelper;
    }

    /**
     * GET: /Student/QuanLyDiem
     * Home Page
     */
    public IActionResult Index()
    {
        string idUser = GetIdUser();
        if (idUser == null)
        {
            return RedirectToAction("Index", "Login", new { area = "Identity" });
        }

        List<Diem> listDiem = (
            from diem in _context.Diems
            where diem.IdSinhVien == idUser
            join lopHocPhan in _context.LopHocPhans on diem.IdLopHocPhan equals lopHocPhan.IdLopHocPhan
            join sinhVien in _context.SinhViens on diem.IdSinhVien equals sinhVien.IdSinhVien
            select new Diem
            {
                IdDiem = diem.IdDiem,
                DiemQuaTrinh = diem.DiemQuaTrinh,
                DiemKetThuc = diem.DiemKetThuc,
                DiemTongKet = diem.DiemTongKet,
                LopHocPhans = lopHocPhan, 
                SinhViens = sinhVien,
                LanHoc = diem.LanHoc, 
            }
            ).ToList();

        return View(listDiem);
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
