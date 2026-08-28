using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebApiBySunil_Lab_26.Data;
using WebApiBySunil_Lab_26.Models;

public class StudentDto
{
  public string Name { get; set; } = "";
  public string Faculty { get; set; } = "";
}
public static class StudentEndpoints
{
  public static void MapStudentEndpoints(this WebApplication app)
  {

    app.MapGet("/Students", async (AppDbContext db) =>
      {
        var students = await db.Students.ToListAsync();
        return students;
      });

    app.MapPost("/Students", async (StudentDto s, AppDbContext db) =>
    {
      var students = db.Students.Add(new Student(s.Name, s.Faculty));
      await db.SaveChangesAsync();

      return Results.Created($"/students/{s.Name}", s);
    });

    app.MapPatch("/Students", async (Student s, AppDbContext db) =>
    {
      var students = db.Students.Update(s);
      await db.SaveChangesAsync();

      return Results.Ok(s);
    });

    app.MapDelete("/Students", async (int id, AppDbContext db) =>
    {
      var student = db.Students.Find(id);

      if (student != null)
      {
        db.Students.Remove(student);
        await db.SaveChangesAsync();

        return Results.Ok();
      }

      return Results.NotFound();
    });
  }
}

/*
using Microsoft.AspNetCore.Mvc;
using WebApp6BySunil_Lab_24.Models;
using WebApp6BySunil_Lab_24.Data;
public class StudentsController : Controller
{
  private readonly AppDbContext _sdb;

  public StudentsController(AppDbContext sdb)
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

*/
