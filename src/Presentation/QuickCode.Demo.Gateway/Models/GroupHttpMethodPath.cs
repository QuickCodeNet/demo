namespace QuickCode.Demo.Gateway.Models;

public class GroupHttpMethodPath
{
    public int? PermissionGroupId { get; set; }
    public string HttpMethod { get; set; } = null!;
    public string Path { get; set; } = null!;
}