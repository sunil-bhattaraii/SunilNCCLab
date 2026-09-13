using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{
  public string Department { get; set; } = "General";

}
