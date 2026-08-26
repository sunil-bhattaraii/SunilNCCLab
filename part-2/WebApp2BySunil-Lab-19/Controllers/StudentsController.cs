using Microsoft.AspNetCore.Mvc;
using WebApp2BySunil_Lab_19.Models;
namespace WebApp2BySunil_Lab_19.Controllers;

public class StudentsController : Controller
{
  [HttpGet]
  public IActionResult Index()
  {
    return View();
  }

  [HttpGet]
  public IActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public IActionResult Create(Student student)
  {
    if (ModelState.IsValid)
    {
      return RedirectToAction("Details", student);
    }

    return View(student);
  }

  [HttpGet]
  public IActionResult Details(Student student)
  {
    return View(student);
  }
}
