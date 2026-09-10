using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp10BySunil_Lab_30.Controllers;

// [AllowAnonymous] ensures that unauthenticated visitors can access
// the registration and login forms to create an identity.
public class AccountController : Controller
{
  private readonly UserManager<IdentityUser> _userManager;
  private readonly SignInManager<IdentityUser> _signInManager;

  // Dependency Injection fetches Identity services configured in Program.cs
  public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
  {
    _userManager = userManager;
    _signInManager = signInManager;
  }

  // GET: /Account/Register
  [HttpGet]
  public IActionResult Register()
  {
    return View();
  }

  // POST: /Account/Register
  [HttpPost]
  public async Task<IActionResult> Register(string name, string email, string password)
  {
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
      ModelState.AddModelError("", "Email and Password are required.");
      return View();
    }

    var user = new IdentityUser { UserName = name, Email = email };

    // Creates the user in the SQLite database and handles password hashing automatically
    var result = await _userManager.CreateAsync(user, password);

    if (result.Succeeded)
    {
      // Automatically sign the user in using a temporary session cookie upon successful signup
      await _signInManager.SignInAsync(user, isPersistent: false);
      return RedirectToAction("Index", "Home");
    }

    // If registration fails, append Identity validation errors (e.g., password too weak) to the UI
    foreach (var error in result.Errors)
    {
      ModelState.AddModelError("", error.Description);
    }

    return View();
  }

  // GET: /Account/Login
  [HttpGet]
  public IActionResult Login()
  {
    return View();
  }

  // POST: /Account/Login
  [HttpPost]
  public async Task<IActionResult> Login(string email, string password)
  {
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
      ModelState.AddModelError("", "Email and Password are required.");
      return View();
    }

    // Validates credentials against the database records
    // Parameters: username, password, isPersistent (remember me), lockoutOnFailure
    var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

    if (result.Succeeded)
    {
      return RedirectToAction("Index", "Home");
    }

    ModelState.AddModelError("", "Invalid Login Attempt. Please try again.");
    return View();
  }

  // POST: /Account/Logout
  [HttpPost]
  [ValidateAntiForgeryToken] // Protects state-changing operations from CSRF attacks
  public async Task<IActionResult> Logout()
  {
    // Instructs the browser to delete the local authentication cookie
    await _signInManager.SignOutAsync();
    return RedirectToAction("Index", "Home");
  }

  [Authorize]
  public IActionResult Dash()
  {
    return View();
  }
}
