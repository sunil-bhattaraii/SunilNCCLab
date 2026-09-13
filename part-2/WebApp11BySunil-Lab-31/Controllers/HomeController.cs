using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp11BySunil_Lab_31.Models;

namespace WebApp11BySunil_Lab_31.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public HomeController(ILogger<HomeController> logger,
                          UserManager<AppUser> userManager,
                          SignInManager<AppUser> signInManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // GET: /Home/Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Home/Register   -- creates the user, assigns a ROLE and adds CLAIMS to it
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string name, string email, string password,
                                              string confirmPassword, string role, string department)
    {
        if (password != confirmPassword)
            ModelState.AddModelError("", "Passwords do not match.");

        if (!ModelState.IsValid)
            return View();

        var user = new AppUser { UserName = name, Email = email, Department = department };
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            // Role-based authorization uses these
            await _userManager.AddToRoleAsync(user, role);

            // Claim-based authorization uses these
            await _userManager.AddClaimAsync(user, new Claim("Department", department));

            // Admin role implies the "ManageProducts" permission claim used by the policy
            if (role == "Admin")
                await _userManager.AddClaimAsync(user, new Claim("Permission", "ManageProducts"));

            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);

        return View();
    }

    // GET: /Home/Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Home/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is not null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", "Invalid login attempt.");
        return View();
    }

    // POST: /Home/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}