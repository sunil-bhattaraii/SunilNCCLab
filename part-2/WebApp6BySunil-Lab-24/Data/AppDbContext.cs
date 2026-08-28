using Microsoft.EntityFrameworkCore;
using WebApp6BySunil_Lab_24.Models;

namespace WebApp6BySunil_Lab_24.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
  {
  }

  public DbSet<Student> Students { get; set; }
}
