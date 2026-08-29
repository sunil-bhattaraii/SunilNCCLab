using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApp8BySunil_Lab_27.Models;

namespace WebApp8BySunil_Lab_27.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMemoryCache _cache;

    public HomeController(ILogger<HomeController> logger, IMemoryCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    [HttpGet]
    public IActionResult Index()
    {
        string? username = HttpContext.Session.GetString("Username");
        var info = TempData["Info"];

        ViewData["Info"] = info;

        if (username != null)
            return View("Session", username);
        else
            return View();
    }

    public string GetData()
    {
        string data = "This is a result from an expensive computation";

        bool cached = true;
        string? message = _cache.Get<string>("Data");

        if (message == null)
        {
            message = data;
            _cache.Set("Data", message);
            cached = false;
        }

        return (cached ? "Cached Data: " : "") + message;
    }

    [HttpPost]
    public IActionResult Index(string name)
    {
        HttpContext.Session.SetString("Username", name);

        var message = HttpContext.Items["message"];
        ViewData["Data"] = GetData();

        return View("Session", message);

    }

    [HttpGet]
    public IActionResult ClearSession()
    {
        HttpContext.Session.Clear();
        TempData["Info"] = "You Have Been Forgotten.";

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
