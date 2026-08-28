If you're coming from the **ADO.NET CRUD repository** you were just building, EF Core does the same database work with much less SQL. Instead of manually creating `SqlConnection`, `SqlCommand`, `SqlDataReader`, etc., you work with C# objects and a `DbContext`.

The basic flow is:

```text
MVC Controller
      ↓
   DbContext
      ↓
 Entity Framework Core
      ↓
   SQL Server
```

## 1. Install EF Core packages

For SQL Server:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

You mainly need:

* `Microsoft.EntityFrameworkCore.SqlServer` → SQL Server provider
* `Microsoft.EntityFrameworkCore.Tools` → migrations/tools

---

## 2. Create your Model

You can use the same `Student` model:

```csharp
namespace MyApp.Models;

public class Student
{
    public int Id { get; set; }
    public int Roll { get; set; }
    public string Name { get; set; } = "";
    public string Faculty { get; set; } = "";
}
```

EF Core can use this class to represent a database table.

---

## 3. Create a DbContext

Create:

```text
Data/
    AppDbContext.cs
```

```csharp
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
}
```

The important part is:

```csharp
public DbSet<Student> Students { get; set; }
```

This essentially tells EF Core:

> "I have a collection of `Student` entities that corresponds to the Students table."

---

# 4. Add your connection string

In `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=StudentDB;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
  }
}
```

Use your actual SQL Server credentials.

---

# 5. Register DbContext

In `Program.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using MyApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
```

This is **Dependency Injection** again.

ASP.NET Core will now know how to create an `AppDbContext` whenever something asks for one.

---

# 6. Create the database using migrations

If you're starting with an empty database and want EF Core to create the tables:

```bash
dotnet ef migrations add InitialCreate
```

Then:

```bash
dotnet ef database update
```

EF Core generates the SQL required to create the database schema.

So instead of manually writing:

```sql
CREATE TABLE Students (...)
```

you let EF generate it.

---

# 7. Use EF Core in your Controller

Now your controller becomes much simpler.

```csharp
using Microsoft.AspNetCore.Mvc;
using MyApp.Data;
using MyApp.Models;

public class StudentController : Controller
{
    private readonly AppDbContext _context;

    public StudentController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var students = _context.Students.ToList();

        return View(students);
    }
}
```

Compare that with your ADO.NET version.

### ADO.NET

```csharp
using SqlConnection connection = new(connectionString);

connection.Open();

string sql = "SELECT * FROM Students";

using SqlCommand command = new(sql, connection);
using SqlDataReader reader = command.ExecuteReader();

List<Student> students = new();

while (reader.Read())
{
    // manually create Student objects
}
```

### EF Core

```csharp
var students = _context.Students.ToList();
```

That's the whole SELECT.

---

# 8. CRUD becomes very simple

### CREATE

```csharp
[HttpPost]
public IActionResult Create(Student student)
{
    _context.Students.Add(student);
    _context.SaveChanges();

    return RedirectToAction("Index");
}
```

EF Core generates an `INSERT` roughly equivalent to:

```sql
INSERT INTO Students (...)
VALUES (...)
```

---

### READ

All students:

```csharp
var students = _context.Students.ToList();
```

One student:

```csharp
var student = _context.Students.Find(id);
```

Or:

```csharp
var student = _context.Students
    .FirstOrDefault(s => s.Id == id);
```

---

### UPDATE

```csharp
[HttpPost]
public IActionResult Update(Student student)
{
    _context.Students.Update(student);
    _context.SaveChanges();

    return RedirectToAction("Index");
}
```

---

### DELETE

```csharp
public IActionResult Delete(int id)
{
    var student = _context.Students.Find(id);

    if (student != null)
    {
        _context.Students.Remove(student);
        _context.SaveChanges();
    }

    return RedirectToAction("Index");
}
```

So your CRUD becomes:

```text
CREATE → Add()
READ   → ToList() / Find()
UPDATE → Update()
DELETE → Remove()

             ↓

       SaveChanges()
```

---

# 9. What is actually happening?

This is the important conceptual difference from ADO.NET.

With ADO.NET, **you write SQL**:

```csharp
string sql = "SELECT * FROM Students WHERE Id = @id";
```

With EF Core, **you write C# expressions**:

```csharp
var student = _context.Students
    .FirstOrDefault(s => s.Id == id);
```

EF Core translates that into SQL for you.

Conceptually:

```text
C#:
_context.Students
    .FirstOrDefault(s => s.Id == id)

             ↓ EF Core

SQL:
SELECT TOP(1) *
FROM Students
WHERE Id = @id
```

You don't normally see the generated SQL, because EF is doing the tedious plumbing.

---

# 10. Where does the Repository fit?

You were just asking about repositories with ADO.NET.

You **can still use a repository with EF Core**:

```text
Controller
    ↓
StudentRepository
    ↓
AppDbContext
    ↓
EF Core
    ↓
SQL Server
```

For example:

```csharp
public class StudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Student> GetAll()
    {
        return _context.Students.ToList();
    }

    public Student? GetById(int id)
    {
        return _context.Students.Find(id);
    }

    public void Create(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
    }

    public void Update(Student student)
    {
        _context.Students.Update(student);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var student = _context.Students.Find(id);

        if (student != null)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}
```

Then the controller only deals with the repository.

However, **you don't necessarily need a repository with EF Core**. `DbContext` already acts as a unit-of-work/data-access abstraction, so adding a repository layer can sometimes just wrap EF Core methods for no useful reason.

For your learning path, I'd understand it in this order:

```text
ADO.NET
  ↓
You manually control SQL + connections
  ↓
EF Core
  ↓
DbContext abstracts that work
  ↓
LINQ generates SQL
  ↓
Migrations manage schema
```

Since you're currently learning both **ADO.NET and EF Core**, understanding this distinction is much more valuable than simply memorizing `Add()`, `Remove()`, and `SaveChanges()`.
