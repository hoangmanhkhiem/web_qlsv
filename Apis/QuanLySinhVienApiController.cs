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
    [HttpGet("listDiem")]
    public IActionResult GetDiem()
    {
        return Ok(_dbContext.Diems.ToList());
    }

    // GET: Get all data table giao vien
    [HttpGet("listGiaovien")]
    public IActionResult GetGiaoVien()
    {
        return Ok(_dbContext.GiaoViens.ToList());
    }

    // GET: Get all data table sinh vien
    [HttpGet("listSinhvien")]
    public IActionResult GetSinhVien()
    {
        return Ok(_dbContext.SinhViens.ToList());
    }

    // GET: Get all data table thoi gian
    [HttpGet("listThoigian")]
    public IActionResult GetThoiGian()
    {
        return Ok(_dbContext.ThoiGians.ToList());
    }

    // GET: Get all data table lop hoc phan
    [HttpGet("listLophocphan")]
    public IActionResult GetLopHocPhan()
    {
        return Ok(_dbContext.LopHocPhans.ToList());
    }

    // GET: Get all data table diem danh
    [HttpGet("listDiemDanh")]
    public IActionResult GetDiemDanh()
    {
        return Ok(_dbContext.DiemDanhs.ToList());
    }

    // GET: Get all data table sinh vien lop hoc phan
    [HttpGet("listSinhVienLopHocPhan")]
    public IActionResult GetSinhVienLopHocPhan()
    {
        return Ok(_dbContext.SinhVienLopHocPhans.ToList());
    }

    // GET: Get all data table thoi gian lop hoc phan
    [HttpGet("listThoiGianLopHocPhan")]
    public IActionResult GetThoiGianLopHocPhan()
    {
        return Ok(_dbContext.ThoiGianLopHocPhans.ToList());
    }

    // POST: Add new data to table diem
    [HttpPost("createDiem")]
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
    [HttpPost("createGiaoVien")]
    public IActionResult PostGiaoVien(GiaoVien giaoVien)
    {
        if (ModelState.IsValid)
        {
            _dbContext.GiaoViens.Add(giaoVien);
            _dbContext.SaveChanges();
            return Ok(giaoVien);
        }
        return BadRequest();
    }

    // POST: POST new data to table sinh vien
    [HttpPost("createSinhVien")]
    public IActionResult PostSinhVien(SinhVien sinhVien)
    {
        if (ModelState.IsValid)
        {
            _dbContext.SinhViens.Add(sinhVien);
            _dbContext.SaveChanges();
            return Ok(sinhVien);
        }
        return BadRequest();
    }

    // POST: POST new data to table thoi gian
    [HttpPost("createThoiGian")]
    public IActionResult PostThoiGian(ThoiGian thoiGian)
    {
        if (ModelState.IsValid)
        {
            _dbContext.ThoiGians.Add(thoiGian);
            _dbContext.SaveChanges();
            return Ok(thoiGian);
        }
        return BadRequest();
    }

    // POST: POST new data to table lop hoc phan
    [HttpPost("createLopHocPhan")]
    public IActionResult PostLopHocPhan(LopHocPhan lopHocPhan)
    {
        if (ModelState.IsValid)
        {
            _dbContext.LopHocPhans.Add(lopHocPhan);
            _dbContext.SaveChanges();
            return Ok(lopHocPhan);
        }
        return BadRequest();
    }

    // POST: POST new data to table diem danh
    [HttpPost("createDiemDanh")]
    public IActionResult PostDiemDanh(DiemDanh diemDanh)
    {
        if (ModelState.IsValid)
        {
            _dbContext.DiemDanhs.Add(diemDanh);
            _dbContext.SaveChanges();
            return Ok(diemDanh);
        }
        return BadRequest();
    }

    // POST: POST new data to table sinh vien lop hoc phan
    [HttpPost("createSinhVienLopHocPhan")]
    public IActionResult PostSinhVienLopHocPhan(SinhVienLopHocPhan sinhVienLopHocPhan)
    {
        if (ModelState.IsValid)
        {
            _dbContext.SinhVienLopHocPhans.Add(sinhVienLopHocPhan);
            _dbContext.SaveChanges();
            return Ok(sinhVienLopHocPhan);
        }
        return BadRequest();
    }

    // POST: POST new data to table thoi gian lop hoc phan
    [HttpPost("createThoiGianLopHocPhan")]
    public IActionResult PostThoiGianLopHocPhan(ThoiGianLopHocPhan thoiGianLopHocPhan)
    {
        if (ModelState.IsValid)
        {
            _dbContext.ThoiGianLopHocPhans.Add(thoiGianLopHocPhan);
            _dbContext.SaveChanges();
            return Ok(thoiGianLopHocPhan);
        }
        return BadRequest();
    }

    // PUT: Update data in table diem
    [HttpPut("upgrateDiem")]
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
    [HttpPut("upgrateGiaoVien")]
    public IActionResult PutGiaoVien(GiaoVien giaoVien)
    {
        if (ModelState.IsValid)
        {
            _dbContext.GiaoViens.Update(giaoVien);
            _dbContext.SaveChanges();
            return Ok(giaoVien);
        }
        return BadRequest();
    }

    // PUT: Upgrade data to table sinh vien
    [HttpPut("upgrateSinhVien")]
    public IActionResult PutSinhVien(SinhVien sinhVien)
    {
        if (ModelState.IsValid)
        {
            _dbContext.SinhViens.Update(sinhVien);
            _dbContext.SaveChanges();
            return Ok(sinhVien);
        }
        return BadRequest();
    }

    // PUT: Upgrade data to table thoi gian
    [HttpPut("upgrateThoiGian")]
    public IActionResult PutThoiGian(ThoiGian thoiGian)
    {
        if (ModelState.IsValid)
        {
            _dbContext.ThoiGians.Update(thoiGian);
            _dbContext.SaveChanges();
            return Ok(thoiGian);
        }
        return BadRequest();
    }

    // PUT: Upgrade data to table lop hoc phan
    [HttpPut("upgrateLopHocPhan")]
    public IActionResult PutLopHocPhan(LopHocPhan lopHocPhan)
    {
        if (ModelState.IsValid)
        {
            _dbContext.LopHocPhans.Update(lopHocPhan);
            _dbContext.SaveChanges();
            return Ok(lopHocPhan);
        }
        return BadRequest();
    }

    // PUT: Upgrade data to table diem danh
    [HttpPut("upgrateDiemDanh")]
    public IActionResult PutDiemDanh(DiemDanh diemDanh)
    {
        if (ModelState.IsValid)
        {
            _dbContext.DiemDanhs.Update(diemDanh);
            _dbContext.SaveChanges();
            return Ok(diemDanh);
        }
        return BadRequest();
    }

    // PUT: Upgrade data to table sinh vien lop hoc phan
    [HttpPut("upgrateSinhVienLopHocPhan")]
    public IActionResult PutSinhVienLopHocPhan(SinhVienLopHocPhan sinhVienLopHocPhan)
    {
        if (ModelState.IsValid)
        {
            _dbContext.SinhVienLopHocPhans.Update(sinhVienLopHocPhan);
            _dbContext.SaveChanges();
            return Ok(sinhVienLopHocPhan);
        }
        return BadRequest();
    }

    // PUT: Upgrade data to table thoi gian lop hoc phan
    [HttpPut("upgrateThoiGianLopHocPhan")]
    public IActionResult PutThoiGianLopHocPhan(ThoiGianLopHocPhan thoiGianLopHocPhan)
    {
        if (ModelState.IsValid)
        {
            _dbContext.ThoiGianLopHocPhans.Update(thoiGianLopHocPhan);
            _dbContext.SaveChanges();
            return Ok(thoiGianLopHocPhan);
        }
        return BadRequest();
    }

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
    [HttpDelete("deleteGiaoVien/{id}")]
    public IActionResult DeleteGiaoVien(string id)
    {
        var giaoVien = _dbContext.GiaoViens.FirstOrDefault(u => u.IdGiaoVien == id);
        if (giaoVien == null)
        {
            return NotFound("GiaoVien not found");
        }

        _dbContext.GiaoViens.Remove(giaoVien);
        _dbContext.SaveChanges();
        return Ok($"GiaoVien {giaoVien.IdGiaoVien} deleted");
    }

    // DELETE: DELETE data to from table with id 
    [HttpDelete("deleteSinhVien/{id}")]
    public IActionResult DeleteSinhVien(string id)
    {
        var sinhVien = _dbContext.SinhViens.FirstOrDefault(u => u.IdSinhVien == id);
        if (sinhVien == null)
        {
            return NotFound("SinhVien not found");
        }

        _dbContext.SinhViens.Remove(sinhVien);
        _dbContext.SaveChanges();
        return Ok($"SinhVien {sinhVien.IdSinhVien} deleted");
    }

    // DELETE: DELETE data to from table with id 
    [HttpDelete("deleteThoiGian/{id}")]
    public IActionResult DeleteThoiGian(string id)
    {
        var thoiGian = _dbContext.ThoiGians.FirstOrDefault(u => u.IdThoiGian == id);
        if (thoiGian == null)
        {
            return NotFound("ThoiGian not found");
        }

        _dbContext.ThoiGians.Remove(thoiGian);
        _dbContext.SaveChanges();
        return Ok($"ThoiGian {thoiGian.IdThoiGian} deleted");
    }

    // DELETE: DELETE data to from table with id 
    [HttpDelete("deleteLopHocPhan/{id}")]
    public IActionResult DeleteLopHocPhan(string id)
    {
        var lopHocPhan = _dbContext.LopHocPhans.FirstOrDefault(u => u.IdHocPhan == id);
        if (lopHocPhan == null)
        {
            return NotFound("LopHocPhan not found");
        }

        _dbContext.LopHocPhans.Remove(lopHocPhan);
        _dbContext.SaveChanges();
        return Ok($"LopHocPhan {lopHocPhan.IdHocPhan} deleted");
    }
    // DELETE: DELETE data to from table with id 
    [HttpDelete("deleteDiemDanh/{id}")]
    public IActionResult DeleteDiemDanh(string id)
    {
        var diemDanh = _dbContext.DiemDanhs.FirstOrDefault(u => u.IdDiemDanh == id);
        if (diemDanh == null)
        {
            return NotFound("DiemDanh not found");
        }

        _dbContext.DiemDanhs.Remove(diemDanh);
        _dbContext.SaveChanges();
        return Ok($"DiemDanh {diemDanh.IdDiemDanh} deleted");
    }

    // DELETE: DELETE data to from table with id  
    [HttpDelete("deleteSinhVienLopHocPhan/{id}")]
    public IActionResult DeleteSinhVienLopHocPhan(string id)
    {
        var sinhVienLopHocPhan = _dbContext.SinhVienLopHocPhans.FirstOrDefault(u => u.IdSinhVienLopHocPhan == id);
        if (sinhVienLopHocPhan == null)
        {
            return NotFound("SinhVienLopHocPhan not found");
        }

        _dbContext.SinhVienLopHocPhans.Remove(sinhVienLopHocPhan);
        _dbContext.SaveChanges();
        return Ok($"SinhVienLopHocPhan {sinhVienLopHocPhan.IdSinhVienLopHocPhan} deleted");
    }

    // DELETE: DELETE data to from table with id
    [HttpDelete("deleteThoiGianLopHocPhan/{id}")]
    public IActionResult DeleteThoiGianLopHocPhan(string id)
    {
        var thoiGianLopHocPhan = _dbContext.ThoiGianLopHocPhans.FirstOrDefault(u => u.IdThoiGianLopHocPhan == id);
        if (thoiGianLopHocPhan == null)
        {
            return NotFound("ThoiGianLopHocPhan not found");
        }

        _dbContext.ThoiGianLopHocPhans.Remove(thoiGianLopHocPhan);
        _dbContext.SaveChanges();
        return Ok($"ThoiGianLopHocPhan {thoiGianLopHocPhan.IdThoiGianLopHocPhan} deleted");
    }

    /**
     * List Api Adv
     */

    /**
     * List Function Helper
     */
}

