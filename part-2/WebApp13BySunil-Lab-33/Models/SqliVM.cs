namespace WebApp13BySunil_Lab_33.Models;

public class UserRow
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Password { get; set; } = "";
    public string Role { get; set; } = "";
}

/// <summary>Result of one SQL injection demo run ("vuln" or "safe").</summary>
public class SqliVM
{
    public string Mode { get; set; } = "";
    public string Input { get; set; } = "";
    public string Sql { get; set; } = "";
    public List<UserRow> Rows { get; set; } = new();
}