using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//
using qlsv.Data;
using qlsv.Models;
using Microsoft.AspNetCore.Authorization;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class QuanLySinhVienApiController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _dbContext;

    // Constructor
    public QuanLySinhVienApiController(
        QuanLySinhVienDbContext quanLySinhVienDbContext)
    {
        _dbContext = quanLySinhVienDbContext;
    }
    /**
     * List Api Basic With All Tables
     */

    // GET: Get all data table diem
    [HttpGet("diem")]
    public IActionResult GetDiem()
    {
        return Ok(_dbContext.Diems.ToList());
    }

    // GET: Get all data table giao vien
    [HttpGet("giaovien")]
    public IActionResult GetGiaoVien()
    {
        return Ok(_dbContext.GiaoViens.ToList());
    }

    // GET: Get all data table sinh vien
    [HttpGet("sinhvien")]
    public IActionResult GetSinhVien()
    {
        return Ok(_dbContext.SinhViens.ToList());
    }

    // GET: Get all data table thoi gian
    [HttpGet("thoigian")]
    public IActionResult GetThoiGian()
    {
        return Ok(_dbContext.ThoiGians.ToList());
    }

    // GET: Get all data table lop hoc phan
    [HttpGet("lophocphan")]
    public IActionResult GetLopHocPhan()
    {
        return Ok(_dbContext.LopHocPhans.ToList());
    }

    // Get: Get all data table diem danh
    [HttpDelete("diemDanh")]
    public IActionResult GetDiemDanh()
    {
        return Ok(_dbContext.DiemDanhs.ToList());
    }

    // GET: Get all data table sinh vien lop hoc phan
    [HttpGet("sinhVienLopHocPhan")]
    public IActionResult GetSinhVienLopHocPhan()
    {
        return Ok(_dbContext.SinhVienLopHocPhans.ToList());
    }

    // GET: Get all data table thoi gian lop hoc phan
    [HttpGet("thoiGianLopHocPhan")]
    public IActionResult GetThoiGianLopHocPhan()
    {
        return Ok(_dbContext.ThoiGianLopHocPhans.ToList());
    }

    // POST: Add new data to table diem
    [HttpPost("diem")]
    public IActionResult PostDiem(Diem diem)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Diems.Add(diem);
            _dbContext.SaveChanges();
            return Ok(diem);
        }
        return BadRequest();
    }

    // POST: POST new data to table giao vien

    // POST: POST new data to table sinh vien

    // POST: POST new data to table thoi gian

    // POST: POST new data to table lop hoc phan

    // POST: POST new data to table diem danh

    // POST: POST new data to table sinh vien lop hoc phan

    // POST: POST new data to table thoi gian lop hoc phan

    // PUT: Update data in table diem
    [HttpPut("diem")]
    public IActionResult PutDiem(Diem diem)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Diems.Update(diem);
            _dbContext.SaveChanges();
            return Ok(diem);
        }
        return BadRequest();
    }

    // PUT: Upgrade data to table giao vien
    // PUT: Upgrade data to table sinh vien
    // PUT: Upgrade data to table thoi gian
    // PUT: Upgrade data to table lop hoc phan
    // PUT: Upgrade data to table diem danh
    // PUT: Upgrade data to table sinh vien lop hoc phan
    // PUT: Upgrade data to table thoi gian lop hoc phan

    // DELETE: Delete data in table diem
    [HttpDelete("deleteDiem/{id}")]
    public IActionResult DeleteDiem(string id)
    {   
        var diem = _dbContext.Diems.FirstOrDefault(u => u.IdDiem == id);
        if (diem == null)
        {
            return NotFound("Diem not found");
        }

        _dbContext.Diems.Remove(diem);
        _dbContext.SaveChanges();
        return Ok($"Diem {diem.IdDiem} deleted");
    }

    // DELETE: DELETE data to from table with id 
    // DELETE: DELETE data to from table with id 
    // DELETE: DELETE data to from table with id 
    // DELETE: DELETE data to from table with id 
    // DELETE: DELETE data to from table with id 
    // DELETE: DELETE data to from table with id  
    // DELETE: DELETE data to from table with id

    /**
     * List Api Adv
     */

    /**
     * List Function Helper
     */
}

