You're right. I taught you the **controller-based approach first**, when you specifically want to learn the **current ASP.NET Core approach**. That's on me. I checked the current Microsoft documentation, and for **new projects Microsoft recommends Minimal APIs**. Controller-based APIs are still supported, but are described as the alternative approach. ([Microsoft Learn][1])

So let's reset.

# ASP.NET Core Web APIs, the modern way: Minimal APIs

The central idea is ridiculously simple:

```csharp
app.MapGet(...)
app.MapPost(...)
app.MapPut(...)
app.MapDelete(...)
```

Instead of:

```text
Controller class
    ↓
[HttpGet]
[HttpPost]
...
```

you define endpoints directly on `app`.

---

## 1. Start with the smallest possible API

Create a project:

```bash
dotnet new web -n StudentApi
cd StudentApi
code .
```

Your `Program.cs` can literally be:

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

Run:

```bash
dotnet run
```

Then visit:

```text
http://localhost:5000/
```

and you get:

```text
Hello World!
```

That's already a Web API.

Microsoft's current Minimal API documentation uses essentially this same structure. ([Microsoft Learn][2])

---

# 2. What is `app.MapGet()`?

This:

```csharp
app.MapGet("/", () => "Hello World!");
```

means:

> When somebody sends a `GET` request to `/`, execute this code.

Break it apart:

```text
app
 │
 └── MapGet
       │
       ├── Route: "/"
       │
       └── Handler: () => "Hello World!"
```

So:

```http
GET /
```

causes:

```csharp
() => "Hello World!"
```

to execute.

The function is called a **route handler**.

---

# 3. Multiple endpoints

Now let's make an actual API:

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "API is running");

app.MapGet("/students", () =>
{
    return new[]
    {
        new { Id = 1, Name = "Ram", Age = 20 },
        new { Id = 2, Name = "Sita", Age = 21 },
        new { Id = 3, Name = "Hari", Age = 19 }
    };
});

app.Run();
```

Now:

```http
GET /
```

returns:

```text
API is running
```

while:

```http
GET /students
```

returns JSON:

```json
[
    {
        "id": 1,
        "name": "Ram",
        "age": 20
    },
    {
        "id": 2,
        "name": "Sita",
        "age": 21
    },
    {
        "id": 3,
        "name": "Hari",
        "age": 19
    }
]
```

ASP.NET Core automatically serializes the returned C# object into JSON.

That's one of the nice things about Minimal APIs. Less ceremonial chanting, more actual API.

---

# 4. `MapGet`, `MapPost`, etc.

These correspond directly to HTTP methods.

```csharp
app.MapGet("/students", ...);
```

means:

```text
GET /students
```

```csharp
app.MapPost("/students", ...);
```

means:

```text
POST /students
```

```csharp
app.MapPut("/students/{id}", ...);
```

means:

```text
PUT /students/5
```

```csharp
app.MapDelete("/students/{id}", ...);
```

means:

```text
DELETE /students/5
```

So the basic API structure is:

```csharp
app.MapGet(...);
app.MapPost(...);
app.MapPut(...);
app.MapDelete(...);
```

Microsoft's Minimal API tutorial follows this CRUD structure with GET, POST, PUT, PATCH, and DELETE endpoints. ([Microsoft Learn][3])

---

# 5. Route parameters

Suppose we want:

```text
GET /students/5
```

We can write:

```csharp
app.MapGet("/students/{id}", (int id) =>
{
    return $"Student ID: {id}";
});
```

The `{id}` is a **route parameter**.

Request:

```http
GET /students/5
```

causes:

```csharp
id = 5
```

So the response is:

```text
Student ID: 5
```

This is one of the key features of Minimal APIs: parameters in the route handler can be bound automatically from the request. ([Microsoft Learn][2])

---

# 6. Query parameters

Suppose the URL is:

```text
/students?age=20
```

You can write:

```csharp
app.MapGet("/students", (int age) =>
{
    return $"Students with age {age}";
});
```

Request:

```http
GET /students?age=20
```

ASP.NET Core gives:

```csharp
age = 20
```

to your handler.

So:

```text
/students/{id}
```

gets `id` from the **route**.

And:

```text
/students?age=20
```

gets `age` from the **query string**.

---

# 7. POST and request bodies

Now let's create a model:

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
```

Then:

```csharp
app.MapPost("/students", (Student student) =>
{
    return student;
});
```

The client sends:

```http
POST /students
Content-Type: application/json
```

with:

```json
{
    "id": 1,
    "name": "Ram",
    "age": 20
}
```

ASP.NET Core converts that JSON into:

```csharp
Student student
```

automatically.

So conceptually:

```text
JSON request
     ↓
ASP.NET Core
     ↓
Student object
     ↓
Your handler
```

This is **parameter binding**.

---

# 8. You can have multiple parameters

For example:

```csharp
app.MapGet("/students/{id}", (int id) =>
{
    return $"Student {id}";
});
```

The `id` comes from:

```text
/students/10
```

You can combine sources:

```csharp
app.MapGet("/students/{id}", (int id, string name) =>
{
    return $"ID = {id}, Name = {name}";
});
```

Request:

```text
/students/10?name=Ram
```

gives:

```text
id   = 10
name = Ram
```

Minimal APIs have fairly sophisticated parameter-binding rules, including route values, query strings, headers, services, and request bodies. ([Microsoft Learn][2])

---

# 9. Returning proper HTTP responses

You aren't restricted to returning strings or objects.

You can use:

```csharp
app.MapGet("/students/{id}", (int id) =>
{
    if (id <= 0)
        return Results.BadRequest();

    return Results.Ok(new
    {
        Id = id,
        Name = "Ram"
    });
});
```

Now:

```csharp
Results.Ok(...)
```

means:

```text
200 OK
```

and:

```csharp
Results.BadRequest()
```

means:

```text
400 Bad Request
```

Other useful ones:

```csharp
Results.Ok()
Results.Created()
Results.NotFound()
Results.BadRequest()
Results.Unauthorized()
Results.Forbid()
Results.NoContent()
```

---

# 10. A real CRUD example

Let's put the concepts together.

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var students = new List<Student>
{
    new Student { Id = 1, Name = "Ram", Age = 20 },
    new Student { Id = 2, Name = "Sita", Age = 21 }
};

app.MapGet("/students", () =>
{
    return students;
});

app.MapGet("/students/{id}", (int id) =>
{
    var student = students.FirstOrDefault(s => s.Id == id);

    return student is not null
        ? Results.Ok(student)
        : Results.NotFound();
});

app.MapPost("/students", (Student student) =>
{
    students.Add(student);

    return Results.Created($"/students/{student.Id}", student);
});

app.MapPut("/students/{id}", (int id, Student updatedStudent) =>
{
    var student = students.FirstOrDefault(s => s.Id == id);

    if (student is null)
        return Results.NotFound();

    student.Name = updatedStudent.Name;
    student.Age = updatedStudent.Age;

    return Results.NoContent();
});

app.MapDelete("/students/{id}", (int id) =>
{
    var student = students.FirstOrDefault(s => s.Id == id);

    if (student is null)
        return Results.NotFound();

    students.Remove(student);

    return Results.NoContent();
});

app.Run();

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
```

That's a complete CRUD API.

No:

```text
Controllers/
    StudentsController.cs
```

No:

```csharp
[ApiController]
```

No:

```csharp
[HttpGet]
```

No:

```csharp
[Route(...)]
```

Just route handlers.

---

# 11. The architecture is still the same

This is important.

Minimal APIs don't eliminate the concepts we've already discussed.

Eventually you'll have:

```text
                  CLIENT
                    │
                    │ HTTP
                    ↓
              ASP.NET Core
                    │
                    ↓
              Route Handler
                    │
                    ↓
               Service Layer
                    │
                    ↓
                 EF Core
                    │
                    ↓
                SQL Server
```

For example:

```csharp
app.MapGet("/students", async (AppDbContext db) =>
{
    return await db.Students.ToListAsync();
});
```

Now:

```text
GET /students
      ↓
MapGet
      ↓
AppDbContext
      ↓
EF Core
      ↓
SQL Server
      ↓
List<Student>
      ↓
JSON
      ↓
Client
```

And **this is where your previous EF Core learning becomes useful**.

---

# 12. Dependency Injection in Minimal APIs

Here's where your recent DI lesson comes back.

Suppose you registered:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
```

Then you can simply put `AppDbContext` in your route handler:

```csharp
app.MapGet("/students", async (AppDbContext db) =>
{
    return await db.Students.ToListAsync();
});
```

You don't manually create it.

ASP.NET Core sees:

```csharp
AppDbContext db
```

and says, essentially:

> "I know this service. I'll give you one."

That's **dependency injection**.

And notice how much cleaner this is than the controller version.

---

# 13. Minimal API vs Controller API

The distinction is now much clearer:

### Modern Minimal API

```csharp
app.MapGet("/students", async (AppDbContext db) =>
{
    return await db.Students.ToListAsync();
});
```

### Controller API

```csharp
[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _db;

    public StudentsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        return Ok(await _db.Students.ToListAsync());
    }
}
```

Both ultimately accomplish:

```text
GET /students
       ↓
query database
       ↓
return JSON
```

But Minimal APIs remove a lot of ceremony.

And, importantly, Microsoft **does not say controllers are obsolete**. The current documentation explicitly presents them as an alternative, particularly useful when you need certain advanced MVC features or prefer that architecture. For **new projects**, however, Microsoft recommends Minimal APIs. ([Microsoft Learn][1])

---

# 14. One correction to your statement

You said:

> "the controller based one is old"

I'd phrase that slightly differently:

```text
Controller-based APIs
        ↓
Still supported
        ↓
Not obsolete
```

but:

```text
Minimal APIs
        ↓
Recommended for new projects
        ↓
Modern default
```

That's a meaningful distinction. Calling controllers "old" makes it sound like Microsoft is about to throw them into a .NET museum, which isn't what the documentation says.

---

# 15. What we should learn next

Since you're learning this **properly from the current .NET 10 approach**, I'd structure the lessons like this:

```text
1. Minimal API fundamentals
       ↓
2. MapGet / MapPost / MapPut / MapDelete
       ↓
3. Routing
       ↓
4. Route + Query + Body parameters
       ↓
5. Results and HTTP status codes
       ↓
6. Models and DTOs
       ↓
7. Dependency Injection
       ↓
8. EF Core + SQL Server
       ↓
9. CRUD API
       ↓
10. Validation
       ↓
11. Authentication / Authorization
       ↓
12. OpenAPI / Swagger
       ↓
13. Calling the API from frontend
```

And for your current level, **the next thing to really understand is routing and parameter binding**, because once you understand what this means:

```csharp
app.MapGet(
    "/students/{id}",
    (int id) => ...
);
```

you've understood a surprisingly large chunk of how Minimal APIs work.

[1]: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/apis?view=aspnetcore-6.0&utm_source=chatgpt.com "APIs overview | Microsoft Learn"
[2]: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-10.0&utm_source=chatgpt.com "Minimal APIs quick reference | Microsoft Learn"
[3]: https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-10.0&utm_source=chatgpt.com "Tutorial: Create a Minimal API with ASP.NET Core | Microsoft Learn"
