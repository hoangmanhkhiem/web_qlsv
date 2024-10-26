using System.Diagnostics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;
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
        string idUser = jwtToken.Claims.FirstOrDefault(c => c.Type == "idClaim")?.Value;

        List<SinhVien> sinhViens = (from gv in _context.GiaoViens
                                    join lhp in _context.LopHocPhans on gv.IdGiaoVien equals lhp.IdGiaoVien
                                    join sv_lhp in _context.SinhVienLopHocPhans on lhp.IdLopHocPhan equals sv_lhp.IdLopHocPhan
                                    join sv in _context.SinhViens on sv_lhp.IdSinhVien equals sv.IdSinhVien
                                    where gv.IdGiaoVien == idUser
                                    select new SinhVien{
                                        IdSinhVien = sv.IdSinhVien,
                                        HoTen = sv.HoTen,
                                        Lop = sv.Lop,
                                        NgaySinh = sv.NgaySinh,
                                        DiaChi = sv.DiaChi,
                                        ChuongTrinhHocs = sv.ChuongTrinhHocs, 
                                        Khoas = sv.Khoas
                                    }).ToList();

        return View(sinhViens);
    }

    // GET: Teacher/QuanLySinhVien/Details/
    public IActionResult Details(string idSv)
    {
        var accessToken = HttpContext.Request.Cookies["AccsessToken"];
        var jwtToken = _jwtHelper.DecodeToken(accessToken);
        string idGv = jwtToken.Claims.FirstOrDefault(c => c.Type == "idClaim")?.Value;
        if (string.IsNullOrEmpty(idGv))
        {
            return Unauthorized();
        }

        var diems = (
            from d in _context.Diems
            join sv in _context.SinhViens on d.IdSinhVien equals sv.IdSinhVien
            join lhp in _context.LopHocPhans on d.IdLopHocPhan equals lhp.IdLopHocPhan
            where d.IdSinhVien == idSv && lhp.IdGiaoVien == idGv
            select new Diem
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,
                SinhViens = sv,
                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet
            }
        ).ToList();
        ViewBag.TenSinhVien = _context.SinhViens.Find(idSv)?.HoTen;

        return View(diems);
    }

    // POST: Teacher/QuanLySinhVien/NhapDiem/
    [HttpPost]
    public IActionResult NhapDiem(List<NhapDiem> diems)
    {
        foreach (var diem in diems)
        {
            if (!_context.Diems.Any(d => d.IdDiem == diem.IdDiem))
            {
                return BadRequest(new JsonObject { { "message", "Dữ liệu không hợp lệ" } });
            }

            var diemToUpdate = _context.Diems.Find(diem.IdDiem);
            if (diemToUpdate == null) continue;

            decimal? diemQuaTrinh = ParseScore(diem.DiemQuaTrinh, "Điểm quá trình");
            decimal? diemKetThuc = ParseScore(diem.DiemKetThuc, "Điểm kết thúc");
            decimal? diemTongKet = ParseScore(diem.DiemTongKet, "Điểm tổng kết");

            if (diemQuaTrinh == null || diemKetThuc == null || diemTongKet == null)
            {
                return BadRequest(new JsonObject { { "message", "Điểm nhập vào không hợp lệ" } });
            }

            diemToUpdate.DiemQuaTrinh = diemQuaTrinh.Value;
            diemToUpdate.DiemKetThuc = diemKetThuc.Value;
            diemToUpdate.DiemTongKet = diemTongKet.Value;

            _context.Diems.Update(diemToUpdate);
        }

        _context.SaveChanges();
        return Ok(new JsonObject { { "message", "Cập nhật điểm thành công" } });
    }

    private decimal? ParseScore(string scoreString, string scoreName)
    {
        if (!decimal.TryParse(scoreString, out var score) || score < 0 || score > 10)
        {
            _logger.LogWarning("{ScoreName} không hợp lệ: {ScoreString}", scoreName, scoreString);
            return null;
        }
        return score;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
