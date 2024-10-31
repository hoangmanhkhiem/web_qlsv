using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

//
using qlsv.Data;
using qlsv.Models;
using qlsv.Dto;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiemController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;
    // Constructor
    public DiemController(
        QuanLySinhVienDbContext context)
    {
        _context = context;
    }

    /** 
     * GET: api/diem/
     * Get tat ca diem
     */
    [HttpGet]
    public async Task<IActionResult> GetDiems()
    {
        // Query
        var query = await (
            from d in _context.Diems
            join mon in _context.MonHocs
                on d.IdLopHocPhan equals mon.IdMonHoc
            select new
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,
                IdMon = mon.IdMonHoc,

                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet,
                LanHoc = d.LanHoc,
                TenMonHoc = mon.TenMonHoc,
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /** 
     * GET: api/diem/{id}
     * Get diem tu id
     */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDiemById(string id)
    {
        // Query
        var query = await (
            from d in _context.Diems
            join mon in _context.MonHocs
                on d.IdLopHocPhan equals mon.IdMonHoc
            where d.IdDiem == id
            select new
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,
                IdMon = mon.IdMonHoc,

                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet,
                LanHoc = d.LanHoc,
                TenMonHoc = mon.TenMonHoc,
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /**
     * GET: api/diem/{idSinhVien}/sinhvien
     * Get diem cua sinh vien
     */
    [HttpGet("{idSinhVien}/sinhvien")]
    public async Task<IActionResult> GetDiemFromSinhVien(string idSinhVien)
    {
        // Query
        var query = await (
            from d in _context.Diems
            where d.IdSinhVien == idSinhVien
            join mon in _context.MonHocs
                on d.IdLopHocPhan equals mon.IdMonHoc
            select new
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,
                IdMon = mon.IdMonHoc,

                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet,
                LanHoc = d.LanHoc,
                TenMonHoc = mon.TenMonHoc,
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /**
     * GET: api/diem/{idSinhVien}/sinhvien/dangkinguyenvong
     * Trả Về Danh Sách Điểm Của Sinh Viên Với Lần Thi Cuối Cùng, Có Điểm Tổng Kết >= 7, Chưa Có Trong Nguyện Vọng
     */
    [HttpGet("{idSinhVien}/sinhvien/dangkinguyenvong")]
    public async Task<IActionResult> GetDiemDangKyNguyenVong(string idSinhVien)
    {
        // Lấy các môn học có điểm tổng kết >= 7 và lần học là lớn nhất
        var query = await (
            from diem in _context.Diems
            join lopHocPhan in _context.LopHocPhans
                on diem.IdLopHocPhan equals lopHocPhan.IdLopHocPhan
            join monhoc in _context.MonHocs
                on lopHocPhan.IdMonHoc equals monhoc.IdMonHoc
            where diem.IdSinhVien == idSinhVien && diem.DiemTongKet <= 7
            join latestDiem in (
                from d in _context.Diems
                where d.IdSinhVien == idSinhVien && d.DiemTongKet <= 7
                group d by d.IdLopHocPhan into g
                select new
                {
                    IdLopHocPhan = g.Key,
                    MaxLanHoc = g.Max(x => x.LanHoc)
                }
            ) on new { diem.IdLopHocPhan, diem.LanHoc } equals new { latestDiem.IdLopHocPhan, LanHoc = latestDiem.MaxLanHoc }
            select new
            {
                IdDiem = diem.IdDiem,
                IdSinhVien = diem.IdSinhVien,
                IdLopHocPhan = diem.IdLopHocPhan,
                IdMonHoc = monhoc.IdMonHoc,
                TenLopHocPhan = lopHocPhan.TenHocPhan,  
                DiemQuaTrinh = diem.DiemQuaTrinh,
                DiemKetThuc = diem.DiemKetThuc,
                DiemTongKet = diem.DiemTongKet,
                LanHoc = diem.LanHoc,
                TenMonHoc = monhoc.TenMonHoc,
            }
        ).ToListAsync();

        // lay cac mon hoc da dang ky cua sinh vien
        var queryDangKy = await (
            from dk in _context.DangKyNguyenVongs
            where dk.IdSinhVien == idSinhVien
            select dk.IdMonHoc
        ).ToListAsync();

        // Loại bỏ các môn học đã đăng ký
        query = query.Where(x => !queryDangKy.Contains(x.IdMonHoc)).ToList();

        // Directly return the JSON result
        return Ok(query);
    }

    /**
     * DELETE: api/diem/{id}
     * Delete diem by id 
     */
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiem(string id)
    {
        // Query
        var query = await (
            from d in _context.Diems
            where d.IdDiem == id
            select d
        ).FirstOrDefaultAsync();

        if (query == null) // Ensure there's data
        {
            return NotFound("Không tìm thấy điểm");
        }

        // Remove the data
        _context.Diems.Remove(query);
        await _context.SaveChangesAsync();

        // Directly return the JSON result
        return Ok("Xóa điểm thành công");
    }

    /**
     * POST: api/diem/
     * Add new diem
     */
    [HttpPost]
    public async Task<IActionResult> AddDiem(DiemDto diem)
    {
        // Handle if dont have Id diem
        if (diem.IdDiem == null)
        {
            diem.IdDiem = Guid.NewGuid().ToString();
        }

        // Check id lop hoc phan, id sinh vien
        var checkLopHocPhan = await _context.LopHocPhans.FindAsync(diem.IdLopHocPhan);
        var checkSinhVien = await _context.SinhViens.FindAsync(diem.IdSinhVien);
        if (checkLopHocPhan == null)
        {
            return BadRequest("Lớp học phần không tồn tại");
        }
        if (checkSinhVien == null)
        {
            return BadRequest("Sinh viên không tồn tại");
        }

        // Create new diem
        var newDiem = new Diem
        {
            IdDiem = diem.IdDiem,
            IdLopHocPhan = diem.IdLopHocPhan,
            IdSinhVien = diem.IdSinhVien,
            DiemQuaTrinh = diem.DiemQuaTrinh,
            DiemKetThuc = diem.DiemKetThuc,
            DiemTongKet = diem.DiemTongKet,
            LanHoc = diem.LanHoc,
        };

        _context.Diems.Add(newDiem);
        await _context.SaveChangesAsync();

        return Ok();
    }

    /**
     * PUT: api/diem/{IdDiem}
     * Update diem by id
     */
    [HttpPut("{IdDiem}")]
    public async Task<IActionResult> EditDiem(string IdDiem, DiemDto diem)
    {

        if (diem.IdDiem != IdDiem)
        {
            return BadRequest("Id không khớp");
        }

        // Check id lop hoc phan, id sinh vien
        var checkLopHocPhan = await _context.LopHocPhans.FindAsync(diem.IdLopHocPhan);
        var checkSinhVien = await _context.SinhViens.FindAsync(diem.IdSinhVien);
        if (checkLopHocPhan == null)
        {
            return BadRequest("Lớp học phần không tồn tại");
        }
        if (checkSinhVien == null)
        {
            return BadRequest("Sinh viên không tồn tại");
        }

        // Query
        var query = await (
            from d in _context.Diems
            where d.IdDiem == IdDiem
            select d
        ).FirstOrDefaultAsync();

        if (query == null) // Ensure there's data
        {
            return NotFound("Không tìm thấy điểm");
        }

        // Update the data
        query.IdLopHocPhan = diem.IdLopHocPhan;
        query.IdSinhVien = diem.IdSinhVien;
        query.DiemQuaTrinh = diem.DiemQuaTrinh;
        query.DiemKetThuc = diem.DiemKetThuc;
        query.DiemTongKet = diem.DiemTongKet;
        query.LanHoc = diem.LanHoc;

        await _context.SaveChangesAsync();

        return Ok("Cập nhật điểm thành công");
    }

}

