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
    // TODO: More GET with other tables

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
    // TODO: More POST with other tables

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
    // TODO: More PUT with other tables

    // DELETE: Delete data in table diem
    [HttpDelete("diem")]
    public IActionResult DeleteDiem(string id)
    {
        return Ok();
    }

    /**
     * List Api Adv
     */

    /**
     * List Function Helper
     */
}

