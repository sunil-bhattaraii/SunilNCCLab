using Microsoft.AspNetCore.Mvc;
using WebApp7BySunil_Lab_25;
public class StudentsController : Controller
{
  private readonly EfcoreDbContext _sdb;

  public StudentsController(EfcoreDbContext sdb)
  {
    _sdb = sdb;
  }
  public IActionResult Index()
  {
    return View(_sdb.Students.ToList());
  }

  [HttpGet]
  public IActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public IActionResult CreateStudent(string Name, string Faculty)
  {
    _sdb.Students.Add(new Student(Name, Faculty));
    _sdb.SaveChanges();

    return View("Success", "Student Created Successfully");
  }

  [HttpGet]
  public IActionResult Update(int id)
  {
    var student = _sdb.Students.Find(id);
    return View(student);
  }

  [HttpPost]
  public IActionResult UpdateStudent(int Roll, string Name, string Faculty)
  {
    _sdb.Students.Update(new Student(Roll, Name, Faculty));
    _sdb.SaveChanges();

    return View("Success", "Student Record Updates Successfully.");
  }

  [HttpGet]
  public IActionResult DeleteStudent(int id)
  {
    var student = _sdb.Students.Find(id);

    if (student != null)
    {
      _sdb.Students.Remove(student);
      _sdb.SaveChanges();
    }
    return View("Success", "Student Deleted Successfully");
  }
}
