
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
//
using qlsv.Helpers;

namespace qlsv.Middlewares;

/**
 * Đoạn mã tự động thêm "Bearer " vào Header Authorization khi dùng [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, ...)]
 */
public class CustomJwtMiddleware
{
    // Variable
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;


    // Constructor
    public CustomJwtMiddleware(
        RequestDelegate next,
        IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    // Handle the request
    public async Task Invoke(HttpContext context)
    {
        // Lấy token từ header Authorization
        var token = context.Request.Headers["Authorization"];

        // Chuyển đổi StringValues sang string
        if (token.Count > 0) // Kiểm tra xem có giá trị nào không
        {
            var tokenString = token.ToString(); // Chuyển đổi StringValues sang string

            // Nếu token không chứa "Bearer", gán token từ header khác
            if (!tokenString.StartsWith("Bearer "))
            {
                context.Request.Headers["Authorization"] = "Bearer " + tokenString;
            }
        }

        await _next(context);
    }

    // Handle refresh token logic
    // TODO
    private async Task HandleRefreshToken(HttpContext context)
    {
        // Example: Logic for handling token refresh (you can customize this)
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(token))
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var jwtHelper = scope.ServiceProvider.GetRequiredService<JwtHelper>();

                // Check if the token is valid or needs to be refreshed
                var isValidToken = jwtHelper.ValidateToken(token);

                if (isValidToken.Status == 401)
                {
                    // var Token =  jwtHelper.RefreshToken(token);
                    // context.Response.Headers.Add("Authorization", $"Bearer {newToken}");
                }
            }
        }

        await Task.CompletedTask;
    }
}