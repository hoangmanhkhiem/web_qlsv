
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
using qlsv.Middlewares;

namespace qlsv;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        BuilderAddAuth(builder);
        AddServices(builder);
        AddDatabase(builder);

        var app = builder.Build();

        app.UseDeveloperExceptionPage(); // TODO del it - Enable when code

        // Add Middleware
        AppAddMiddleware(app);

        // App initialization
        AppInit(app);

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // Configure Route
        Router.RouterConfig(app);

        // Enable Swagger in all environments
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty; // Set Swagger UI at apps root
        });

        app.Run();
    }

    // Add Services
    private static void AddServices(WebApplicationBuilder builder)
    {
        // Add Swagger services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerGen(options =>
        {
            // Add documentation
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "API Test",
                Version = "v1",
                Description = "Test Api QLSV",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "Ly Tran Vinh",
                    Email = "lytranvinh.work@gmail.com"
                }
            });
            // Add Bearer Token Support
            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Please enter JWT with Bearer into field",
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        // Add service helpper jwt
        builder.Services.AddScoped<JwtHelper>();

        // Add service helpper security
        builder.Services.AddScoped<SecurityHelper>();

        // Add service helpper calendar
        builder.Services.AddScoped<CalendarHelper>();
    }

    // Add Authentication
    private static void BuilderAddAuth(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        // Configure JWT authentication
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);
        var audience = jwtSettings["Audience"];
        var issuer = jwtSettings["Issuer"];

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
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
        });

        builder.Services.AddAuthorization();
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

        // Add View db context
        builder.Services.AddDbContext<ViewDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
            )
        );

        // Add Sessions db context 
        // TODO: Use redis
        builder.Services.AddDbContext<SessionDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
            )
        );
    }

    // App initialization
    private static void AppInit(WebApplication app)
    {
        // Initialize the database
        using (var serviceScope = app.Services.CreateScope())
        {
            var services = serviceScope.ServiceProvider;
            InitDbContext.Initialize(services);
        }
    }

    // App Add Middleware
    private static void AppAddMiddleware(WebApplication app)
    {
        app.UseMiddleware<CustomJwtMiddleware>();
    }
}