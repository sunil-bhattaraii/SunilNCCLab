using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Allow letters including spaces in the UserName (e.g. "Sunil Bhattarai")
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
});

// Lab 31: authorization using Roles, Claims and Policies.
builder.Services.AddAuthorization(options =>
{
    // Require the user to hold the "Admin" role         (ROLE based)
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

    // Require the user to hold the claim Department=IT  (CLAIM based)
    options.AddPolicy("ITDepartment", policy => policy.RequireClaim("Department", "IT"));

    // Role + Claim combined inside a single POLICY      (POLICY based)
    options.AddPolicy("CanManageProducts", policy =>
        policy.RequireRole("Admin").RequireClaim("Permission", "ManageProducts"));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Home/Login";
    options.AccessDeniedPath = "/Home/Error";
});

var app = builder.Build();

// Create/update the database schema and seed roles, an Admin user, and claims.
using (var scope = app.Services.CreateScope())
{
    await SeedData.InitializeAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();  // determines WHO the user is
app.UseAuthorization();   // determines WHAT the user may access

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();