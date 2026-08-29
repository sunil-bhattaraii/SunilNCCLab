using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp9BySunil_Lab_28.Models;

namespace WebApp9BySunil_Lab_28.Controllers;

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
        string? username = Request.Cookies["Username"];

        if (username != null)
            return View("Session");
        else
            return View();
    }

    [HttpPost]
    public IActionResult RegisterUser(string hidden, string name)
    {
        Console.WriteLine($"The hidden value was: {hidden}");

        Response.Cookies.Append("Username", name, new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddHours(24),
        });

        return RedirectToAction("Index", new { Registered = "True" });

    }

    [HttpGet]
    public IActionResult ClearSession()
    {
        Response.Cookies.Delete("Username");
        TempData["Info"] = "You Have Been Forgotten.";

        return RedirectToAction("Index", new { info = "You Have Been Forgotten" });
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
