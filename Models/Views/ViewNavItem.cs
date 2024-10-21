
using Microsoft.AspNetCore.Identity;

namespace qlsv.Models;

public class ViewNavItem: IdentityUser {
    public string? Id { get; set; }
    public string? Area { get; set; }
    public string? Controller { get; set; }
    public string? Action { get; set; }
    public byte[]? Icon { get; set; }
    public string? Content { get; set; }
    // TODO Add more properties
}