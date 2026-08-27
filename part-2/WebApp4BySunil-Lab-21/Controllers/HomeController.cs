using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp4BySunil_Lab_21.Models;

namespace WebApp4BySunil_Lab_21.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {

        return View();
    }

    [HttpPost]
    public IActionResult UpdateFile(string fileText)
    {
        System.IO.File.WriteAllText("public/data.json", fileText);
        return View("Index");
    }

    [HttpPost]
    public IActionResult AddStudent(string name, int age)
    {
        Console.WriteLine($"{name}, {age}");
        Student NewStudent = new Student(name, age);

        string fileText = System.IO.File.ReadAllText("public/data.json");
        List<Student> students = JsonSerializer.Deserialize<List<Student>>(fileText)!;

        students.Add(NewStudent);

        fileText = JsonSerializer.Serialize(students);
        System.IO.File.WriteAllText("public/data.json", fileText);

        return View("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
