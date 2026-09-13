using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
  public static async Task InitializeAsync(IServiceProvider services)
  {
    var context = services.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();

    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("SeedData");

    // 1. Define the roles
    foreach (var role in new[] { "Admin", "User" })
    {
      if (!await roleManager.RoleExistsAsync(role))
        await roleManager.CreateAsync(new IdentityRole(role));
    }

    // 2. Admin user: "Admin" role + Department & Permission claims
    await CreateUserAsync(userManager, logger, "admin@example.com", "Admin@123", "Sunil Bhattarai", "Admin",
                          new List<(string Type, string Value)> { ("Department", "IT"), ("Permission", "ManageProducts") });

    // 3. Normal user: "User" role + Department claim only (unique UserName)
    await CreateUserAsync(userManager, logger, "user@example.com", "User@123", "User Sunil B", "User",
                          new List<(string Type, string Value)> { ("Department", "General") });
  }

  private static async Task CreateUserAsync(UserManager<AppUser> userManager, ILogger logger, string email, string password,
                                            string name, string role, List<(string Type, string Value)> claims)
  {
    if (await userManager.FindByEmailAsync(email) is not null)
      return;

    var user = new AppUser
    {
      UserName = name,
      Email = email,
      Department = claims.First(c => c.Type == "Department").Value
    };

    var result = await userManager.CreateAsync(user, password);
    if (result.Succeeded)
    {
      await userManager.AddToRoleAsync(user, role);

      foreach (var (type, value) in claims)
        await userManager.AddClaimAsync(user, new System.Security.Claims.Claim(type, value));
    }
    else
    {
      logger.LogWarning("Seeding user {Email} failed: {Errors}", email, string.Join("; ", result.Errors.Select(e => e.Description)));
    }
  }
}