
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

        List<Diem> listDiem = _context.Diems
            .Where(d => d.IdSinhVien == idUser && d.DiemTongKet < 7) // Lọc điểm dưới 7 trước
            .AsEnumerable() // Chuyển dữ liệu sang client để xử lý nhóm
            .GroupBy(d => d.IdLopHocPhan) // Nhóm theo lớp học phần
            .Select(g => g.OrderByDescending(d => d.LanHoc).FirstOrDefault()) // Chọn lần học cuối cùng
            .Where(diemCuoi => !_context.DangKyNguyenVongs
                .Any(nv => nv.IdSinhVien == idUser && nv.IdMonHoc == diemCuoi.LopHocPhans.IdMonHoc)) // Loại bỏ nếu đã có trong nguyện vọng
            .Select(diemCuoi => new Diem
            {
                IdDiem = diemCuoi.IdDiem,
                DiemQuaTrinh = diemCuoi.DiemQuaTrinh,
                DiemKetThuc = diemCuoi.DiemKetThuc,
                DiemTongKet = diemCuoi.DiemTongKet,
                LopHocPhans = diemCuoi.LopHocPhans,
                SinhViens = diemCuoi.SinhViens
            })
            .ToList();

        ViewBag.IdUser = idUser;
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
