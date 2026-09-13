namespace WebApp12BySunil_Lab_32.Models;

public class UserListItem
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public List<string> Roles { get; set; } = new();
    public int ClaimCount { get; set; }
}

public class ManageUserVM
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public List<System.Security.Claims.Claim> Claims { get; set; } = new();
    public List<UserRoleItem> Roles { get; set; } = new();
}

public class UserRoleItem
{
    public string? RoleName { get; set; }
    public bool Selected { get; set; }
}