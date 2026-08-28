using Microsoft.AspNetCore.Mvc;
using WebApp5BySunil_Lab_23.Models;

public class StudentsController : Controller
{
  StudentDb _sdb;

  public StudentsController(StudentDb sdb)
  {
    _sdb = sdb;
  }
  public IActionResult Index()
  {
    return View(_sdb.GetAllStudents());
  }

  [HttpGet]
  public IActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public IActionResult CreateStudent(string Name, string Faculty)
  {
    _sdb.CreateStudent(new Student(0, Name, Faculty));
    return View("Success", "Student Created Successfully");
  }

  [HttpGet]
  public IActionResult Update(int id)
  {
    Console.WriteLine(id);
    return View(_sdb.GetStudent(id));
  }

  [HttpPost]
  public IActionResult UpdateStudent(int Roll, string Name, string Faculty)
  {
    _sdb.UpdateStudent(new Student(Roll, Name, Faculty));

    return View("Success", "Student Record Updates Successfully.");
  }

  [HttpGet]
  public IActionResult DeleteStudent(int id)
  {
    _sdb.DeleteStudent(id);
    return View("Success", "Student Deleted Successfully");
  }
}
