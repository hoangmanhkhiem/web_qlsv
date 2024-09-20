
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
//
using qlsv.Data;
using qlsv.Models;

namespace qlsv;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        AddDatabase(builder);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        Router.RouterConfig(app);

        app.Run();
    }

    // Add Database
    private static void AddDatabase(WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
            )
        );

        // Add Identity db context 
        builder.Services.AddDbContext<IdentityDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
            )
        );

        // Register Identity services
        builder.Services.AddIdentity<UserCustom, IdentityRole>()
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();
    }
}

