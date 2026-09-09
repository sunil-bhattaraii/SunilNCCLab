using Microsoft.EntityFrameworkCore;
namespace WebApp10BySunil_Lab_30.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
  {
  }

}
