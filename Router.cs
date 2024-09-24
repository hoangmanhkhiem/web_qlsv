
namespace qlsv;

public class Router
{
    public static void RouterConfig(WebApplication app)
    {

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        // Map API controller routes

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // This is required to map API controller routes
        });

        // Route for area
        app.MapAreaControllerRoute(
            name: "Identity",
            areaName: "Identity",
            pattern: "Identity/{controller=Login}/{action=Index}/{id?}"
        );

        // Route for basic
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Redirect Route
        app.MapControllerRoute(
            name: "Redirect login",
            pattern: "{controller=Login}/{action=Index}/{id?}",
            defaults: new { area = "Identity", controller = "Login", action = "Index" }
        );
    }
}