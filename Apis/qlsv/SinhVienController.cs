using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;

//
using qlsv.Helpers;
using qlsv.Data;
using qlsv.Models;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SinhVienController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public SinhVienController(
        QuanLySinhVienDbContext quanLySinhVienDbContext)
    {
        _context = quanLySinhVienDbContext;
    }

    /**
     * GET: api/sinhvien/
     * get all sinh vien
     */
    [HttpGet]
    public async Task<IActionResult> GetSinhViens()
    {
        var sinhViens = (
            from sv in _context.SinhViens
            join khoa in _context.Khoas on sv.IdKhoa equals khoa.IdKhoa
            join cch in _context.ChuongTrinhHocs on sv.IdChuongTrinhHoc equals cch.IdChuongTrinhHoc
            select new {
                // List id
                IdSinhVien = sv.IdSinhVien,
                IdKhoa = sv.IdKhoa,
                IdChuongTrinhHoc = sv.IdChuongTrinhHoc,
                // list value
                TenSinhVien = sv.HoTen,
                Lop = sv.Lop,
                NgaySinh = sv.NgaySinh.HasValue ? sv.NgaySinh.Value.ToString("dd/MM/yyyy") : null,
                DiaChi = sv.DiaChi,
                TenKhoa = khoa.TenKhoa,
                TenChuongTrinhHoc = cch.TenChuongTrinhHoc,
            }
        ).ToList();
        return Ok(sinhViens);
    }

    /**
     * GET: api/sinhvien/{id}/
     * get sinh vien by id
     */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSinhVienById(string id)
    {
        var sinhVien = (
            from sv in _context.SinhViens
            where sv.IdSinhVien == id
            join khoa in _context.Khoas on sv.IdKhoa equals khoa.IdKhoa
            join cch in _context.ChuongTrinhHocs on sv.IdChuongTrinhHoc equals cch.IdChuongTrinhHoc
            select new {
                // List id
                IdSinhVien = sv.IdSinhVien,
                IdKhoa = sv.IdKhoa,
                IdChuongTrinhHoc = sv.IdChuongTrinhHoc,
                // list value
                TenSinhVien = sv.HoTen,
                Lop = sv.Lop,
                NgaySinh = sv.NgaySinh.HasValue ? sv.NgaySinh.Value.ToString("dd/MM/yyyy") : null,
                DiaChi = sv.DiaChi,
                TenKhoa = khoa.TenKhoa,
                TenChuongTrinhHoc = cch.TenChuongTrinhHoc,
            }
        ).FirstOrDefault();
        return Ok(sinhVien);
    }

    /**
     * PUT: api/sinhvien/{id}/
     * sua thong tin sinh vien
     */
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSinhVien(string id, [FromBody] SinhVien sinhVien)
    {
        // Find the existing sinh vien
        var existingSinhVien = await _context.SinhViens.FindAsync(id);
        if (existingSinhVien == null)
        {
            return NotFound("Không tìm thấy sinh viên");
        }
        // Update the existing sinh vien
        existingSinhVien.HoTen = sinhVien.HoTen;
        existingSinhVien.Lop = sinhVien.Lop;
        existingSinhVien.NgaySinh = sinhVien.NgaySinh;
        existingSinhVien.DiaChi = sinhVien.DiaChi;
        existingSinhVien.IdKhoa = sinhVien.IdKhoa;
        existingSinhVien.IdChuongTrinhHoc = sinhVien.IdChuongTrinhHoc;
        // Save the changes
        await _context.SaveChangesAsync();
        return Ok("Sửa thông tin sinh viên thành công");
    }

    /**
     * POST: api/sinhvien/
     * them sinh vien
     */
    [HttpPost]
    public async Task<IActionResult> CreateSinhVien([FromBody] SinhVien sinhVien)
    {
        _context.SinhViens.Add(sinhVien);
        await _context.SaveChangesAsync();
        return Ok("Thêm sinh viên thành công");
    }

    // DELETE: api/sinhvien/{id}
    // delete sinh vien by id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSinhVien(string id)
    {
        var sinhVien = await _context.SinhViens.FindAsync(id);
        if (sinhVien == null)
        {
            return NotFound("Không tìm thấy sinh viên");
        }
        _context.SinhViens.Remove(sinhVien);
        await _context.SaveChangesAsync();
        return Ok("Xóa sinh viên thành công");
    }

    // TODO
}

