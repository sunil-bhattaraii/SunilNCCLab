namespace WebApiBySunil_Lab_26.Models;

public class Student
{
  public int Id { get; set; }
  public string Name { get; set; } = "";
  public string Faculty { get; set; } = "";

  public Student()
  {
  }
  public Student(string name, string faculty)
  {
    //for creating objects in efcore
    Name = name;
    Faculty = faculty;
  }

  public Student(int id, string name, string faculty)
  {
    Id = id;
    Name = name;
    Faculty = faculty;
  }
}
