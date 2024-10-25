
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//
using qlsv.Data;

namespace qlsv.Components;

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
                return View("SinhVien", 
                    await _context.SinhViens
                        .Include(sv => sv.ChuongTrinhHocs)
                        .Include(sv => sv.Khoas)
                        .ToListAsync());
            case "GIAOVIEN":
                return View("GIAOVIEN", 
                    await _context.GiaoViens
                        .Include(gv => gv.Khoas)
                        .ToListAsync());
            case "LOPHOCPHAN":
                return View("LopHocPhan", 
                    await _context.LopHocPhans
                        .Include(lhp => lhp.GiaoViens)
                        .Include(lhp => lhp.MonHocs)
                        .ToListAsync());
            case "CHUONGTRINHHOC":
                return View("ChuongTrinhHoc", 
                    await _context.ChuongTrinhHocs.ToListAsync());
            case "KHOA":
                return View("Khoa", 
                    await _context.Khoas.ToListAsync());
            case "MONHOC":
                return View("MonHoc", 
                    await _context.MonHocs
                        .Include(mh => mh.Khoas)
                        .ToListAsync());
            case "NGUYENVONG":
                return View("NguyenVong", 
                    await _context.DangKyNguyenVongs
                        .Include(mh => mh.SinhViens)
                        .Include(mh => mh.MonHocs) 
                        .ToListAsync());
            default:
                return View();
        }
    }
}