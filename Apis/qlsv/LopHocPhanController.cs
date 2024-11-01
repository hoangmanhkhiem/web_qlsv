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
public class LopHocPhanController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;
    // Constructor
    public LopHocPhanController(
        QuanLySinhVienDbContext context)
    {
        _context = context;
    }

    /** 
     * GET: api/lophocphan/
     * Get tat ca lop hoc phan
     */
    [HttpGet]
    public async Task<IActionResult> GetLopHocPhans()
    {
        // Query
        var query = await (
            from lhp in _context.LopHocPhans
            join gv in _context.GiaoViens on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mh in _context.MonHocs on lhp.IdMonHoc equals mh.IdMonHoc
            select new
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mh.TenMonHoc,
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdGiaoVien = gv.IdGiaoVien,
                IdMonHoc = mh.IdMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /** 
     * GET: api/lophocphan/sinhvien/{id}
     * Get lop hoc phan cua sinh vien tu id
     */
    [HttpGet("sinhvien/{id}")]
    public async Task<IActionResult> GetLopHocPhanBySinhVien(string id)
    {
        // Query
        var query = await (
            from svlhp in _context.SinhVienLopHocPhans
            join lhp in _context.LopHocPhans on svlhp.IdLopHocPhan equals lhp.IdLopHocPhan
            join gv in _context.GiaoViens on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mon in _context.MonHocs on lhp.IdMonHoc equals mon.IdMonHoc
            where svlhp.IdSinhVien == id
            select new
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mon.TenMonHoc,
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdGiaoVien = gv.IdGiaoVien,
                IdMonHoc = mon.IdMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /** 
     * GET: api/lophocphan/{id}
     * Get lop hoc phan cua giao vien tu id
     */
    [HttpGet("giaovien/{id}")]
    public async Task<IActionResult> GetLopHocPhanByGiaoVien(string id)
    {
        // Query
        var query = await (
            from lhp in _context.LopHocPhans
            join gv in _context.GiaoViens on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mon in _context.MonHocs on lhp.IdMonHoc equals mon.IdMonHoc
            where gv.IdGiaoVien == id
            select new
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mon.TenMonHoc,
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdGiaoVien = gv.IdGiaoVien,
                IdMonHoc = mon.IdMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /**
     * GET: api/lophocphan/{IdLopHocPhan}
     * Get lop hoc phan by id
     */
    [HttpGet("{IdLopHocPhan}")]
    public async Task<IActionResult> GetLopHocPhan(string IdLopHocPhan)
    {
        // Query
        var query = await (
            from lhp in _context.LopHocPhans
            join gv in _context.GiaoViens on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mon in _context.MonHocs on lhp.IdMonHoc equals mon.IdMonHoc
            where lhp.IdLopHocPhan == IdLopHocPhan
            select new
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mon.TenMonHoc,
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdGiaoVien = gv.IdGiaoVien,
                IdMonHoc = mon.IdMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc
            }
        ).FirstOrDefaultAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /**
     * DELETE: api/lophocphan/{IdLopHocPhan}
     * Delete lop hoc phan by IdLopHocPhan
     */
    [HttpDelete("{IdLopHocPhan}")]
    public async Task<IActionResult> DeleteLopHocPhan(string IdLopHocPhan)
    {

        // find lhp
        var qr = await (
            from lhp in _context.LopHocPhans
            where lhp.IdLopHocPhan == IdLopHocPhan
            select lhp
        ).FirstOrDefaultAsync();

        if (qr == null)
        {
            return NotFound("Không tìm thấy lớp học phần");
        }

        _context.LopHocPhans.Remove(qr);
        _context.SaveChanges();

        return Ok();
    }


    /**
     * POST: api/lophocphan/
     * Create lop hoc phan
     */
    [HttpPost]
    public async Task<IActionResult> CreateLopHocPhan([FromBody] LopHocPhanDto lopHocPhan)
    {
        // if id lop hoc phan is null -> generate new id
        if (lopHocPhan.IdLopHocPhan == null)
        {
            lopHocPhan.IdLopHocPhan = Guid.NewGuid().ToString();
        }

        // Create new lop hoc phan
        var newLopHocPhan = new LopHocPhan
        {
            IdLopHocPhan = lopHocPhan.IdLopHocPhan,
            TenHocPhan = lopHocPhan.TenLopHocPhan,
            IdGiaoVien = lopHocPhan.IdGiaoVien,
            IdMonHoc = lopHocPhan.IdMonHoc,
            ThoiGianBatDau = lopHocPhan.ThoiGianBatDau,
            ThoiGianKetThuc = lopHocPhan.ThoiGianKetThuc,
        };

        // Check giao vien, mon hoc
        var mon = await _context.MonHocs.FindAsync(lopHocPhan.IdMonHoc);
        if (mon == null)
        {
            return BadRequest("Không tìm thấy môn học");
        }
        var gv = await _context.GiaoViens.FindAsync(lopHocPhan.IdGiaoVien);
        if (gv == null)
        {
            return BadRequest("Không tìm thấy giáo viên");
        }

        // Add new lop hoc phan
        _context.LopHocPhans.Add(newLopHocPhan);
        _context.SaveChanges();

        return Ok(new
        {
            statusCode = 200,
            messages = "Tạo lớp học phần thành công",
            data = new
            {
                IdLopHocPhan = newLopHocPhan.IdLopHocPhan,
                TenLopHocPhan = newLopHocPhan.TenHocPhan,
                IdGiaoVien = newLopHocPhan.IdGiaoVien,
                IdMonHoc = newLopHocPhan.IdMonHoc,
                ThoiGianBatDau = newLopHocPhan.ThoiGianBatDau,
                ThoiGianKetThuc = newLopHocPhan.ThoiGianKetThuc,
            }
        });
    }

    /**
     * PUT: api/lophocphan/{IdLopHocPhan}
     * Update lop hoc phan by IdLopHocPhan
     */
    [HttpPut("{IdLopHocPhan}")]
    public async Task<IActionResult> UpdateLopHocPhan(
        string IdLopHocPhan, 
        [FromBody] LopHocPhanDto lopHocPhan)
    {
        // find lhp
        var qr = await (
            from lhp in _context.LopHocPhans
            where lhp.IdLopHocPhan == IdLopHocPhan
            select lhp
        ).FirstOrDefaultAsync();

        if (qr == null)
        {
            return NotFound("Không tìm thấy lớp học phần");
        }

        // Update lop hoc phan
        qr.TenHocPhan = lopHocPhan.TenLopHocPhan;
        qr.IdGiaoVien = lopHocPhan.IdGiaoVien;
        qr.IdMonHoc = lopHocPhan.IdMonHoc;
        qr.ThoiGianBatDau = lopHocPhan.ThoiGianBatDau;
        qr.ThoiGianKetThuc = lopHocPhan.ThoiGianKetThuc;

        // Check giao vien, mon hoc
        var mon = await _context.MonHocs.FindAsync(lopHocPhan.IdMonHoc);
        if (mon == null)
        {
            return BadRequest("Không tìm thấy môn học");
        }
        var gv = await _context.GiaoViens.FindAsync(lopHocPhan.IdGiaoVien);
        if (gv == null)
        {
            return BadRequest("Không tìm thấy giáo viên");
        }

        // Update
        _context.LopHocPhans.Update(qr);
        _context.SaveChanges();

        return Ok(new
        {
            statusCode = 200,
            messages = "Cạp nhật thành công",
            data = new
            {
                IdLopHocPhan = qr.IdLopHocPhan,
                TenLopHocPhan = qr.TenHocPhan,
                IdGiaoVien = qr.IdGiaoVien,
                IdMonHoc = qr.IdMonHoc,
                ThoiGianBatDau = qr.ThoiGianBatDau,
                ThoiGianKetThuc = qr.ThoiGianKetThuc
            }
        });
    }

    /**
     * GET: api/monhoc/{idLopHocPhan}/lophocphan/
     * Get lop hoc phan from id mon hoc
     */
    [HttpGet("{idLopHocPhan}/lophocphan")]
    public async Task<IActionResult> GetLopHocPhanFromMonHoc(string idLopHocPhan)
    {
        // query
        var qr = await (
            from lhp in _context.LopHocPhans
            where lhp.IdLopHocPhan == idLopHocPhan
            join gv in _context.GiaoViens on lhp.IdGiaoVien equals gv.IdGiaoVien
            select new {
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdGiaoVien = gv.IdGiaoVien,
                IdMonHoc = lhp.IdMonHoc,
                TenHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc
            }
        ).ToListAsync();

        return Ok(qr);
    }

}