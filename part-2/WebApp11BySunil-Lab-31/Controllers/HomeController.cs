using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp11BySunil_Lab_31.Models;

namespace WebApp11BySunil_Lab_31.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        Console.WriteLine($"login {email}, {password}");
        return View();
    }

    [HttpPost]
    public IActionResult Register(string name, string email, string password, string confirmPassowrd)
    {
        Console.WriteLine($"register {name}, {email}, {password}");

        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
