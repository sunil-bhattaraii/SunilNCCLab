using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
  public static async Task InitializeAsync(IServiceProvider services)
  {
    var context = services.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();

    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("SeedData");

    if (!await roleManager.RoleExistsAsync("Admin"))
      await roleManager.CreateAsync(new IdentityRole("Admin"));

    // Admin user that is granted the "Admin" role (accesses the Admin panel)
    await CreateUserAsync(userManager, logger, "admin@example.com", "Admin@123", "Sunil Admin", "Admin");

    // Normal user without any role (used to test that access is denied)
    await CreateUserAsync(userManager, logger, "user@example.com", "User@123", "User Sunil", role: null);
  }

  private static async Task CreateUserAsync(UserManager<IdentityUser> userManager, ILogger logger, string email,
                                            string password, string name, string? role)
  {
    if (await userManager.FindByEmailAsync(email) is not null)
      return;

    var user = new IdentityUser { UserName = name, Email = email };

    var result = await userManager.CreateAsync(user, password);
    if (result.Succeeded && role is not null)
      await userManager.AddToRoleAsync(user, role);
    else if (!result.Succeeded)
      logger.LogWarning("Seeding user {Email} failed: {Errors}", email,
                        string.Join("; ", result.Errors.Select(e => e.Description)));
  }
}