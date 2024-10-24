using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlsv.Data;  // Thay đổi đường dẫn phù hợp với dự án của bạn
using qlsv.Models;
using System.Linq;

namespace qlsv.Controllers
{
    public class LopHocPhanController : Controller
    {
        private readonly QuanLySinhVienDbContext _context;

        // Inject DbContext để kết nối với database
        public LopHocPhanController(QuanLySinhVienDbContext context)
        {
            _context = context;
        }

        // Action method để lấy danh sách lớp học phần và hiển thị ra view
        public IActionResult DanhSachLopHocPhan()
        {
            // Truy vấn lấy danh sách lớp học phần từ database
            var lopHocPhans = _context.LopHocPhans
                .Include(l => l.ThoiGianLopHocPhans)  // Bao gồm thời gian lớp học phần
                .ThenInclude(t => t.IdThoiGianNavigation)  // Bao gồm thông tin chi tiết về thời gian
                .Include(l => l.IdGiaoVienNavigation)  // Bao gồm thông tin giảng viên
                .ToList();

            // Trả về view và truyền danh sách lớp học phần vào view
            return View(lopHocPhans);
        }

        // Action method để lấy danh sách điểm và hiển thị ra view
        public IActionResult Diem()
        {
            // Truy vấn lấy danh sách điểm từ database
            var diem = _context.Diems
                .Include(d => d.IdLopHocPhanNavigation)  // Bao gồm thông tin lớp học phần
                .ToList();

            // Trả về view và truyền danh sách điểm vào view
            return View(diem);
        }
    }
}
