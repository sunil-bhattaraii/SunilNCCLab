using System;
using System.Collections.Generic;

namespace WebApp7BySunil_Lab_25;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Faculty { get; set; } = null!;

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
