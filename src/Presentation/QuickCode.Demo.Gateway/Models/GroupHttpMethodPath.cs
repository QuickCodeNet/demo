using QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts;

namespace QuickCode.Demo.Gateway.Models;

public class GroupHttpMethodPath
{
    public string? PermissionGroupName { get; set; }
    public HttpMethodType HttpMethod { get; set; }
    public string Path { get; set; } = null!;
}