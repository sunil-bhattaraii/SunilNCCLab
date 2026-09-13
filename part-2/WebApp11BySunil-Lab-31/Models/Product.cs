namespace WebApp11BySunil_Lab_31.Models;

public class Product
{
  public int Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public decimal Price { get; set; }

  public string? CreatedBy { get; set; }
}