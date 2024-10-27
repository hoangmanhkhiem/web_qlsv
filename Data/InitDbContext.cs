
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

//
using qlsv.Models;
using qlsv.Helpers;

namespace qlsv.Data;

public class InitDbContext
{
    // Initialize database for Identity
    public static void Initialize(
        IServiceProvider serviceProvider
    )
    {
        using (var context = new IdentityDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<IdentityDbContext>>()))
        {
            context.Database.EnsureCreated();

            // Create user root account
            if (!context.Users.Any())
            {
                // User root
                var admin = new UserCustom
                {
                    UserName = "admin",
                    Email = "root@v.com",
                    EmailConfirmed = true,
                    ProfilePicture = new byte[] { 0 },
                    FirstName = "Ly Tran",
                    LastName = "Vinh",
                    Address = "HN",
                    Phone = "088888888"
                };

                var password = "123";
                var passwordHash = "i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY="; // SecurityHelper.Hash(password);
                admin.PasswordHash = passwordHash;

                // User basic
                var sv = new UserCustom
                {
                    UserName = "sv",
                    Email = "st1@v.com",
                    EmailConfirmed = true,
                    ProfilePicture = new byte[] { 0 },
                    FirstName = "Student",
                    LastName = "1",
                    Address = "HN",
                    Phone = "21342331"
                };

                passwordHash = "i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY="; // SecurityHelper.Hash(password);
                sv.PasswordHash = passwordHash;

                var gv = new UserCustom
                {
                    UserName = "gv",
                    Email = "lecturer@v.com",
                    EmailConfirmed = true,
                    ProfilePicture = new byte[] { 0 },
                    FirstName = "lecturer",
                    LastName = "1",
                    Address = "HN",
                    Phone = "21342331"
                };
                passwordHash = "i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY="; // SecurityHelper.Hash(password);
                gv.PasswordHash = passwordHash;

                // Save changes
                context.Users.AddRange(admin, sv, gv);
                context.SaveChanges();
            }

            // Create roles for Identity 
            if (!context.Roles.Any())
            {
                var rootRole = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };

                var adminRole = new IdentityRole
                {
                    Name = "SinhVien",
                    NormalizedName = "SINHVIEN"
                };

                var userRole = new IdentityRole
                {
                    Name = "GiaoVien",
                    NormalizedName = "GIAOVIEN"
                };

                context.Roles.AddRange(rootRole, adminRole, userRole);

                context.SaveChanges();
            }

            // Add user to roles
            if (!context.UserRoles.Any())
            {
                var adminUser = context.Users.FirstOrDefault(u => u.UserName == "admin");
                var sinhvienUser = context.Users.FirstOrDefault(u => u.UserName == "sv");
                var giaovienUser = context.Users.FirstOrDefault(u => u.UserName == "gv");

                var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
                var sinhvienRole = context.Roles.FirstOrDefault(r => r.Name == "SinhVien");
                var giaovienRole = context.Roles.FirstOrDefault(r => r.Name == "GiaoVien");

                context.UserRoles.AddRange(
                    new IdentityUserRole<string> { UserId = adminUser.Id, RoleId = adminRole.Id },
                    new IdentityUserRole<string> { UserId = sinhvienUser.Id, RoleId = sinhvienRole.Id },
                    new IdentityUserRole<string> { UserId = giaovienUser.Id, RoleId = giaovienRole.Id }
                );
                context.SaveChanges();
            }
        }

        // handle view db context
        using (var context = new ViewDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ViewDbContext>>()))
        {
            if (!context.NavBarPages.Any())
            {
                // Add navbar admin page
                context.NavBarPages.AddRange(
                    new ViewNavItem
                    {
                        LocationNavItem = "ADMIN",
                        Area = "ADMIN",
                        Controller = "QuanLyGiaoVien",
                        Action = "Index",
                        Content = "Quản lý giáo viên",
                        IconClass = "fas fa-chalkboard-teacher"
                    },
                    new ViewNavItem
                    {
                        LocationNavItem = "ADMIN",
                        Area = "ADMIN",
                        Controller = "QuanLySinhVien",
                        Action = "Index",
                        Content = "Quản lý sinh viên",
                        IconClass = "fas fa-user-graduate"
                    },
                    new ViewNavItem
                    {
                        LocationNavItem = "ADMIN",
                        Area = "ADMIN",
                        Controller = "QuanLyLopHocPhan",
                        Action = "Index",
                        Content = "Quản lý lớp học phần",
                        IconClass = "fas fa-book"
                    },
                    new ViewNavItem
                    {
                        LocationNavItem = "ADMIN",
                        Area = "ADMIN",
                        Controller = "QuanLyMonHoc",
                        Action = "Index",
                        Content = "Quản lý môn học",
                        IconClass = "fas fa-book-open"
                    },
                    new ViewNavItem
                    {
                        LocationNavItem = "ADMIN",
                        Area = "ADMIN",
                        Controller = "QuanLyKhoa",
                        Action = "Index",
                        Content = "Quản lý khoa",
                        IconClass = "fas fa-university"
                    },
                    new ViewNavItem
                    {
                        LocationNavItem = "ADMIN",
                        Area = "ADMIN",
                        Controller = "QuanLyChuongTrinhHoc",
                        Action = "Index",
                        Content = "Quản lý chương trình học",
                        IconClass = "fas fa-graduation-cap"
                    },
                    new ViewNavItem
                    {
                        LocationNavItem = "ADMIN",
                        Area = "ADMIN",
                        Controller = "QuanLyNguyenVong",
                        Action = "Index",
                        Content = "Quản lý nguyện vọng",
                        IconClass = "fas fa-graduation-cap"
                    }
                );

                // Add navbar giao vien page
                context.NavBarPages.AddRange(
                    new ViewNavItem
                    {
                        LocationNavItem = "TEACHER",
                        Area = "TEACHER",
                        Controller = "Home",
                        Action = "Index",
                        Content = "Lịch giảng dạy",
                        IconClass = "fas fa-calendar"
                    },
                    new ViewNavItem
                    {
                        LocationNavItem = "TEACHER",
                        Area = "TEACHER",
                        Controller = "QuanLyLopHocPhan",
                        Action = "Index",
                        Content = "Danh Sách Lớp Học Phần",
                        IconClass = "fas fa-book"
                    },
                    new ViewNavItem
                    {
                        LocationNavItem = "TEACHER",
                        Area = "TEACHER",
                        Controller = "QuanLySinhVien",
                        Action = "Index",
                        Content = "Danh Sách Sinh Viên",
                        IconClass = "fas fa-user-graduate"
                    }
                );
            
                // Add navbar sinh vien page
                context.NavBarPages.AddRange(
                    new ViewNavItem
                    {
                        LocationNavItem = "STUDENT",
                        Area = "STUDENT",
                        Controller = "Home",
                        Action = "Index",
                        Content = "Lịch học",
                        IconClass = "fas fa-calendar"
                    },
                    new ViewNavItem
                    {
                        LocationNavItem = "STUDENT",
                        Area = "STUDENT",
                        Controller = "QuanLyDiem",
                        Action = "Index",
                        Content = "Quản lý điểm",
                        IconClass = "fas fa-graduation-cap"
                    },
                    new ViewNavItem
                    {
                        LocationNavItem = "STUDENT",
                        Area = "STUDENT",
                        Controller = "QuanLyLopHocPhan",
                        Action = "Index",
                        Content = "Danh sách lớp học phần",
                        IconClass = "fas fa-book"
                    },
                    new ViewNavItem
                    {
                        LocationNavItem = "STUDENT",
                        Area = "STUDENT",
                        Controller = "DangKyNguyenVong",
                        Action = "Index",
                        Content = "Đăng ký học nguyện vọng",
                        IconClass = "fas fa-graduation-cap"
                    }
                );
            }
            context.SaveChanges();
        }
    }
}