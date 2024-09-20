
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
//
using qlsv.Data;
using qlsv.Models;
using qlsv.Helpers;

namespace qlsv;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        AddAuthentication(builder);
        AddServices(builder);
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

    // Add Services
    private static void AddServices(WebApplicationBuilder builder)
    {
        // Add service helpper 
        builder.Services.AddScoped<JwtHelper>();
    }

    // Add Authentication
    private static void AddAuthentication(WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

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
        builder.Services.AddDbContext<qlsv.Data.IdentityDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
            )
        );

        // Register Identity services
        builder.Services.AddIdentity<UserCustom, IdentityRole>()
            .AddEntityFrameworkStores<qlsv.Data.IdentityDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();
    }
}

