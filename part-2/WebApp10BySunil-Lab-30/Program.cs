using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp10BySunil_Lab_30.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Add MVC Controllers with Views
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

// 3. Add Identity with default cookie routing options
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 4. Configure Application Cookie paths
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Where unauthorized users are redirected
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 5. Middleware ordering matters!
app.UseAuthentication(); // Determines WHO the user is based on cookies
app.UseAuthorization();  // Determines WHAT the user is allowed to look at

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
