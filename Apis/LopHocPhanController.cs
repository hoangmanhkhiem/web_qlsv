using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;

//
using qlsv.Data;
using qlsv.Models;
using Microsoft.EntityFrameworkCore;

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
            select new
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                IdLopHocPhan = lhp.IdLopHocPhan
            }
        ).ToListAsync();

        if (query == null || !query.Any()) // Ensure there's data
        {
            return NotFound("Không tìm thấy lớp học phần");
        }

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
            where svlhp.IdSinhVien == id
            select new
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                IdLopHocPhan = lhp.IdLopHocPhan
            }
        ).ToListAsync();

        if (query == null || !query.Any()) // Ensure there's data
        {
            return NotFound("Không tìm thấy lớp học phần");
        }

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
            where gv.IdGiaoVien == id
            select new
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                IdLopHocPhan = lhp.IdLopHocPhan
            }
        ).ToListAsync();

        if (query == null || !query.Any()) // Ensure there's data
        {
            return NotFound("Không tìm thấy lớp học phần");
        }

        // Directly return the JSON result
        return Ok(query);
    }
}

