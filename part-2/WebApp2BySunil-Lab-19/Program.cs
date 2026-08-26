// Creates a WebApplicationBuilder.
// It sets up configuration, logging, dependency injection (DI),
// command-line arguments, and other services needed by the app.
var builder = WebApplication.CreateBuilder(args);


// Add services to the dependency injection (DI) container.
// AddControllersWithViews() enables:
//   - MVC Controllers
//   - Razor Views
//   - Model binding, validation, etc.
builder.Services.AddControllersWithViews();


// Builds the application using the services and configuration
// registered in the builder.
var app = builder.Build();


// ---------------- HTTP REQUEST PIPELINE ----------------
// Middleware below determines how incoming HTTP requests
// are processed.


if (!app.Environment.IsDevelopment())
{
    // If the app is NOT running in Development mode,
    // show a user-friendly error page instead of exposing
    // detailed exception information.
    //
    // /Home/Error means the HomeController's Error action
    // will handle the error.
    app.UseExceptionHandler("/Home/Error");


    // Enables HTTP Strict Transport Security (HSTS).
    // It tells browsers to use HTTPS when communicating
    // with this website.
    //
    // The default HSTS duration is 30 days.
    app.UseHsts();
}


// Redirects HTTP requests to HTTPS.
// Example:
// http://example.com  ->  https://example.com
app.UseHttpsRedirection();


// Enables ASP.NET Core's routing system.
// Routing determines which controller/action should
// handle an incoming URL.
app.UseRouting();


// Enables authorization middleware.
// It checks whether the current user is allowed to access
// resources protected by authorization rules such as [Authorize].
app.UseAuthorization();


// Maps static files such as:
// CSS, JavaScript, images, fonts, etc.
//
// This is the newer endpoint-based way of exposing
// static assets in this project template.
app.MapStaticAssets();


// Defines the default MVC route.
//
// Example:
// /                    -> HomeController.Index()
// /Home                -> HomeController.Index()
// /Home/About          -> HomeController.About()
// /Home/Details/5      -> HomeController.Details(5)
//
// {id?} means the id parameter is optional.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")

    // Allows static assets to be associated with this
    // MVC endpoint as well.
    .WithStaticAssets();


// Starts the application and begins listening for
// incoming HTTP requests.
//
// The program stays running here until the application
// is stopped.
app.Run();
