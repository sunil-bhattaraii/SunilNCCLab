using Microsoft.EntityFrameworkCore;
using WebApiBySunil_Lab_26.Models;

namespace WebApiBySunil_Lab_26.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
  {
  }

  public DbSet<Student> Students { get; set; }
}
