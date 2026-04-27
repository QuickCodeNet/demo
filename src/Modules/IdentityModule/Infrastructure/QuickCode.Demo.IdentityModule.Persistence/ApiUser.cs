using Microsoft.AspNetCore.Identity;

namespace QuickCode.Demo.IdentityModule.Persistence;

public class ApiUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PermissionGroupName { get; set; }
}
