
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace qlsv.Middlewares;

/**
 * Đoạn mã tự động thêm "Bearer " vào Header Authorization khi dùng [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, ...)]
 */
public class CustomJwtMiddleware
{
    private readonly RequestDelegate _next;

    public CustomJwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

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

    // TODO: Handle refresh token
    private async Task HandleRefreshToken(HttpContext context)
    {
        await _next(context);
    }
}