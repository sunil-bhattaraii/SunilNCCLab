using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApp13BySunil_Lab_33.Models;

namespace WebApp13BySunil_Lab_33.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly string _connString;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // ---------------- XSS ----------------
    public IActionResult Xss()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Xss(string comment)
    {
        return View(model: comment ?? "");
    }

    // ---------------- SQL Injection ----------------
    public IActionResult Sqli()
    {
        return View(new SqliVM());
    }

    // mode = "vuln" (unparameterized query) or "safe" (parameterized query)
    [HttpPost]
    public async Task<IActionResult> Sqli(string mode, string input)
    {
        var vm = new SqliVM { Mode = mode, Input = input ?? "" };

        await using var conn = new SqlConnection(_connString);
        await conn.OpenAsync();

        if (mode == "vuln")
        {
            // VULNERABLE: user input is concatenated straight into the SQL text
            vm.Sql = $"SELECT Id, Name, Password, Role FROM Users WHERE Name = '{vm.Input}'";
            await using var cmd = new SqlCommand(vm.Sql, conn);
            vm.Rows = await ReadRowsAsync(cmd);
        }
        else
        {
            // SAFE: user input is bound as a parameter, never embedded in SQL text
            vm.Sql = "SELECT Id, Name, Password, Role FROM Users WHERE Name = @name";
            await using var cmd = new SqlCommand(vm.Sql, conn);
            cmd.Parameters.Add(new SqlParameter("@name", vm.Input));
            vm.Rows = await ReadRowsAsync(cmd);
        }

        return View(vm);
    }

    private static async Task<List<UserRow>> ReadRowsAsync(SqlCommand cmd)
    {
        var rows = new List<UserRow>();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            rows.Add(new UserRow
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Password = reader.GetString(2),
                Role = reader.GetString(3)
            });
        }
        return rows;
    }

    // ---------------- CSRF ----------------
    public IActionResult Csrf()
    {
        return View();
    }

    // This POST is protected by an anti-forgery token (attribute).
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Transfer(string account, string amount)
    {
        TempData["CsrfSuccess"] = $"Transferred Rs. {amount} to account {account}.";
        return RedirectToAction(nameof(Csrf));
    }

    // ---------------- Open Redirect ----------------
    public IActionResult OpenRedirect()
    {
        return View();
    }

    // VULNERABLE: redirects to whatever URL the user supplied (external allowed)
    [HttpPost]
    public IActionResult RedirectVulnerable(string url)
    {
        return Redirect(url);
    }

    // SAFE: only local URLs (starting with "/" or "~/") are allowed
    [HttpPost]
    public IActionResult RedirectSafe(string url)
    {
        if (string.IsNullOrWhiteSpace(url) || !Url.IsLocalUrl(url))
        {
            ViewBag.Error = $"Open redirect blocked: '{url}' is not a local URL.";
            return View("OpenRedirect");
        }

        return LocalRedirect(url);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}