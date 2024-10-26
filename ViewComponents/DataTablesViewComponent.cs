
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
    public async Task<IViewComponentResult> InvokeAsync(string table, string? idUser, string? role, params object[] args)
    {
        switch (table.ToUpper())
        {
            case "SINHVIEN":
                if (idUser != null)
                {
                    return View("SinhVien",
                        await _context.SinhViens
                            .Include(sv => sv.ChuongTrinhHocs)
                            .Include(sv => sv.Khoas)
                            .Where(sv => sv.IdSinhVien == idUser)
                            .ToListAsync());
                }
                return View("SinhVien",
                    await _context.SinhViens
                        .Include(sv => sv.ChuongTrinhHocs)
                        .Include(sv => sv.Khoas)
                        .ToListAsync());
            case "GIAOVIEN":
                if (_context.GiaoViens != null)
                {
                    return View("GiaoViens",
                        await _context.GiaoViens
                            .Include(gv => gv.Khoas)
                            .Where(gv => gv.IdGiaoVien == idUser)
                            .ToListAsync());
                }
                return View("GIAOVIEN",
                    await _context.GiaoViens
                        .Include(gv => gv.Khoas)
                        .ToListAsync());
            case "LOPHOCPHAN":
                if (idUser == null)
                {
                    return View("LopHocPhan",
                    await _context.LopHocPhans
                        .Include(lhp => lhp.GiaoViens)
                        .Include(lhp => lhp.MonHocs)
                        .ToListAsync());
                }
                if (role.ToUpper() == "GIAOViEN")
                {
                    return View("LopHocPhan",
                    await _context.LopHocPhans
                        .Include(lhp => lhp.GiaoViens)
                        .Include(lhp => lhp.MonHocs)
                        .Where(lhp => lhp.GiaoViens.IdGiaoVien == idUser)
                        .ToListAsync());
                }
                return View("LopHocPhan",
                    await (from sv in _context.SinhViens
                           where sv.IdSinhVien == idUser
                           join sv_lhp in _context.SinhVienLopHocPhans on sv.IdSinhVien equals sv_lhp.IdSinhVien
                           join lhp in _context.LopHocPhans on sv_lhp.IdLopHocPhan equals lhp.IdLopHocPhan
                           select lhp)
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
                if (idUser == null)
                {
                    return View("NguyenVong",
                        await _context.DangKyNguyenVongs
                            .Include(mh => mh.SinhViens)
                            .Include(mh => mh.MonHocs)
                            .ToListAsync());
                }
                return View("NguyenVong",
                    await _context.DangKyNguyenVongs
                        .Include(mh => mh.SinhViens)
                        .Include(mh => mh.MonHocs)
                        .Where(mh => mh.SinhViens.IdSinhVien == idUser)
                        .ToListAsync());
            default:
                return View();
        }
    }
}