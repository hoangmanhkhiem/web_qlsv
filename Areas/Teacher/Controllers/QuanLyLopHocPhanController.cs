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
public class QuanLyLopHocPhanController : Controller
{
    private readonly ILogger<QuanLyLopHocPhanController> _logger;
    private readonly QuanLySinhVienDbContext _context;
    private readonly JwtHelper _jwtHelper;

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
                          
        if (string.IsNullOrEmpty(accessToken))
        {
            return Unauthorized();
        }

        var jwtToken = _jwtHelper.DecodeToken(accessToken);
        string? idUser = jwtToken.Claims.FirstOrDefault(c => c.Type == "idClaim")?.Value;

        if (string.IsNullOrEmpty(idUser))
        {
            return Unauthorized();
        }

        var lopHocPhans = _context.LopHocPhans
            .Include(lhp => lhp.MonHocs)
            .Include(lhp => lhp.GiaoViens)
            .Where(lhp => lhp.IdGiaoVien == idUser)
            .ToList();

        return View(lopHocPhans);
    }
    
    // GET: Teacher/QuanLyLopHocPhan/Details/
    public IActionResult Details(string idLhp)
    {
        var diems = _context.Diems
            .Where(d => d.IdLopHocPhan == idLhp)
            .Include(d => d.SinhViens)
            .Select(d => new Diem
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,
                SinhViens = d.SinhViens,
                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet 
            })
            .ToList();
        ViewBag.LopHocPhan = _context.LopHocPhans.FirstOrDefault(d => d.IdLopHocPhan == idLhp)
            ?.TenHocPhan;

        return View(diems);
    }

    // POST: Teacher/QuanLyLopHocPhan/NhapDiem/
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
