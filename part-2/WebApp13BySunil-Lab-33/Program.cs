var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Prepare the demo database (Users table + sample rows).
await SecurityDb.InitializeAsync(builder.Configuration.GetConnectionString("DefaultConnection")!);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Friendly page whenever the anti-forgery check rejects a POST (HTTP 400) -
// used by the CSRF demonstration.
app.UseStatusCodePages(async ctx =>
{
    if (ctx.HttpContext.Response.StatusCode == StatusCodes.Status400BadRequest)
    {
        ctx.HttpContext.Response.ContentType = "text/html; charset=utf-8";
        await ctx.HttpContext.Response.WriteAsync(
            "<html><body><h2>400 - Bad Request</h2>" +
            "<p>The request was rejected because the anti-forgery (CSRF) token is " +
            "missing or invalid. The action was never executed.</p>" +
            "<a href='/Home/Csrf'>Back to CSRF demo</a></body></html>");
    }
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();