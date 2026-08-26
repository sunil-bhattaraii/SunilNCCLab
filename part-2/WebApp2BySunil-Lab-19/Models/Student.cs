using System.ComponentModel.DataAnnotations;
namespace WebApp2BySunil_Lab_19.Models;

public class Student
{
  [Required(ErrorMessage = "Student ID is required")]
  public int StdID { get; set; } = 0;

  [Required(ErrorMessage = "Name is required")]
  [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
  public string Name { get; set; } = "";

  [Required(ErrorMessage = "Address is required")]
  public string Address { get; set; } = "";

  [Required(ErrorMessage = "Faculty is required")]
  public string Faculty { get; set; } = "";

  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Enter a valid email address")]
  public string Email { get; set; } = "";
}
