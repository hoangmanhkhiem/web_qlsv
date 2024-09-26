
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
                var root = new UserCustom
                {
                    UserName = "root",
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
                root.PasswordHash = passwordHash;

                // User basic
                var basic = new UserCustom
                {
                    UserName = "st1",
                    Email = "st1@v.com",
                    EmailConfirmed = true,
                    ProfilePicture = new byte[] { 0 },
                    FirstName = "Student",
                    LastName = "1",
                    Address = "HN",
                    Phone = "21342331"
                };

                passwordHash = "i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY="; // SecurityHelper.Hash(password);
                basic.PasswordHash = passwordHash;

                var admin = new UserCustom
                {
                    UserName = "lec",
                    Email = "lecturer@v.com",
                    EmailConfirmed = true,
                    ProfilePicture = new byte[] { 0 },
                    FirstName = "lecturer",
                    LastName = "1",
                    Address = "HN",
                    Phone = "21342331"
                };

                // Save changes
                context.Users.AddRange(root, basic, admin);
                context.SaveChanges();
            }

            // Create roles for Identity 
            if (!context.Roles.Any())
            {
                var rootRole = new IdentityRole
                {
                    Name = "Root",
                    NormalizedName = "ROOT"
                };

                var adminRole = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };

                var userRole = new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                };

                context.Roles.AddRange(rootRole, adminRole, userRole);

                context.SaveChanges();
            }

            // Add user to roles
            if (!context.UserRoles.Any())
            {
                var rootUser = context.Users.FirstOrDefault(u => u.UserName == "root");
                var adminUser = context.Users.FirstOrDefault(u => u.UserName == "st1");
                var basicUser = context.Users.FirstOrDefault(u => u.UserName == "lec");

                var rootRole = context.Roles.FirstOrDefault(r => r.Name == "Root");
                var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
                var userRole = context.Roles.FirstOrDefault(r => r.Name == "User");

                context.UserRoles.AddRange(
                    new IdentityUserRole<string> { UserId = rootUser.Id, RoleId = rootRole.Id },
                    new IdentityUserRole<string> { UserId = adminUser.Id, RoleId = adminRole.Id },
                    new IdentityUserRole<string> { UserId = basicUser.Id, RoleId = userRole.Id }
                );
                context.SaveChanges();
            }
        }
    }
}