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
public class NguyenVongController : ControllerBase
{
    // Variables
    private readonly qlsv.Data.IdentityDbContext _context;
    private readonly QuanLySinhVienDbContext _quanLySinhVienDbContext;

    // Constructor
    public NguyenVongController(
        qlsv.Data.IdentityDbContext context,
        QuanLySinhVienDbContext quanLySinhVienDbContext)
    {
        _context = context;
        _quanLySinhVienDbContext = quanLySinhVienDbContext;
    }
    // TODO
}

