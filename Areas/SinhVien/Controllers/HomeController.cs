using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlsv.Data;
using qlsv.Models;
using System.Linq;

namespace qlsv.Areas.SinhVien.Controllers
{
    [Area("SinhVien")]
    public class HomeController : Controller
    {
        private readonly QuanLySinhVienDbContext _context;

        // Inject DbContext để kết nối với cơ sở dữ liệu
        public HomeController(QuanLySinhVienDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return RedirectToAction("LichHoc");
        }


        // Phương thức lấy danh sách các lớp học phần và hiển thị
        public IActionResult DanhSachLopHocPhan()
        {
            var lopHocPhans = _context.LopHocPhans
                .Include(l => l.ThoiGianLopHocPhans)   // Bao gồm thời gian lớp học phần
                    .ThenInclude(t => t.IdThoiGianNavigation)  // Bao gồm chi tiết về thời gian
                .Include(l => l.IdGiaoVienNavigation)  // Bao gồm thông tin giáo viên
                .ToList();

            return View(lopHocPhans);
        }

        // Phương thức lấy danh sách điểm và hiển thị
        public IActionResult Diem()
        {
            var diem = _context.Diems
                .Include(d => d.IdLopHocPhanNavigation)  // Bao gồm thông tin lớp học phần
                .Include(d => d.IdSinhVienNavigation)   // Bao gồm thông tin sinh viên
                .ToList();

            return View(diem);
        }

        // Phương thức hiển thị lịch học (có thể bổ sung truy vấn sau)
        public IActionResult LichHoc()
        {
            var lichHoc = _context.ThoiGianLopHocPhans
                .Include(l => l.IdLopHocPhanNavigation)
                .Include(l => l.IdThoiGianNavigation)
                .ToList();

            return View(lichHoc);
        }

        // Phương thức hiển thị hồ sơ sinh viên
        public IActionResult Profile(string? mid)
        {
            if (mid == null)
            {
                return NotFound(); // Kiểm tra nếu ID sinh viên không tồn tại
            }

            var sinhvien = _context.SinhViens
                .Include(s => s.Diems) // Bao gồm danh sách điểm
                .Include(s => s.DiemDanhs) // Bao gồm danh sách điểm danh
                .Include(s => s.SinhVienLopHocPhans) // Bao gồm các lớp học phần mà sinh viên tham gia
                .FirstOrDefault(s => s.IdSinhVien == mid);

            if (sinhvien == null)
            {
                return NotFound(); // Kiểm tra nếu sinh viên không tồn tại
            }

            return View(sinhvien);
        }
    }
}
