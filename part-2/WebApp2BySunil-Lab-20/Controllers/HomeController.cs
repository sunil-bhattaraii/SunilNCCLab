using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp2BySunil_Lab_20.Models;

namespace WebApp2BySunil_Lab_20.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly SingletonDep _sd1, _sd2;
    private readonly ScopedDep _sc1, _sc2;
    private readonly TransientDep _td1, _td2;

    private static int count = 0;

    public HomeController(ILogger<HomeController> logger, SingletonDep sd1, SingletonDep sd2, ScopedDep sc1, ScopedDep sc2, TransientDep td1, TransientDep td2)
    {
        _logger = logger;

        _sd1 = sd1;
        _sd2 = sd2;
        _sc1 = sc1;
        _sc2 = sc2;
        _td1 = td1;
        _td2 = td2;
    }

    private void LoadCounters()
    {
        ViewData["Sd1"] = _sd1.GetId();
        ViewData["Sd2"] = _sd2.GetId();
        ViewData["Sc1"] = _sc1.GetId();
        ViewData["Sc2"] = _sc2.GetId();
        ViewData["Td1"] = _td1.GetId();
        ViewData["Td2"] = _td2.GetId();
        ViewData["Counter"] = ++count;
    }

    [HttpGet]
    public IActionResult Index()
    {
        LoadCounters();
        return View();
    }


    [HttpPost]
    public IActionResult Index(string? s)
    {
        LoadCounters();
        return View();
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
