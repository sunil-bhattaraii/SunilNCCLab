namespace WebApp5BySunil_Lab_23.Models;

public class Student
{
  public int Roll { get; }
  public string Name { get; set; }
  public string Faculty { get; set; }

  public Student(int roll, string name, string faculty)
  {
    Roll = roll;
    Name = name;
    Faculty = faculty;
  }
}
