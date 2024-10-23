
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//
using qlsv.Data;

namespace qlsvm.Components;

public class DataTablesViewComponent : ViewComponent
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public DataTablesViewComponent(
        QuanLySinhVienDbContext quanLySinhVienDbContext)
    {
        _context = quanLySinhVienDbContext;
    }

    // Hanlders async
    public async Task<IViewComponentResult> InvokeAsync(string table, params object[] args)
    {
        switch (table.ToUpper())
        {
            case "SINHVIEN":
                return View("SinhVien", await _context.SinhViens.ToListAsync());
            case "GIAOVIEN":
                return View("GiaoVien", await _context.GiaoViens.ToListAsync());
            case "LOPHOCPHAN":
                return View("LopHocPhan");
            case "CHUONGTRINHHOC":
                return View("ChuongTrinhHoc");
            case "KHOA":
                return View("Khoa");
            case "MonHoc":
                return View("MonHoc");
            default:
                return View();
        }
    }
}