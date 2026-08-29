Absolutely. These three are important in ASP.NET MVC because they all let you **carry data on the client side**, but they work quite differently.

The easiest way to understand them is to imagine a user moving between pages:

```text
Browser
   │
   ├── Cookies       → stored by browser
   ├── Query String  → visible in URL
   └── Hidden Field  → stored inside HTML form
```

The big idea is:

> **Client-side state management means the data is kept on the client/browser and sent back to the server when appropriate.**

---

# 1. Cookies

A **cookie** is a small piece of data stored by the browser.

For example:

```text
Username = Linus
Theme = dark
Language = en
```

The browser stores it and can send it back to the server with subsequent requests.

### Creating a cookie

In an MVC controller:

```csharp
public IActionResult SetCookie()
{
    Response.Cookies.Append("Username", "Linus");

    return Content("Cookie created");
}
```

Now the browser has:

```text
Username = Linus
```

### Reading a cookie

```csharp
public IActionResult GetCookie()
{
    string? username = Request.Cookies["Username"];

    return Content(username ?? "Cookie not found");
}
```

You can also do:

```csharp
var username = Request.Cookies["Username"];
```

---

## Setting an expiration time

You can configure the cookie:

```csharp
Response.Cookies.Append(
    "Username",
    "Linus",
    new CookieOptions
    {
        Expires = DateTimeOffset.Now.AddDays(7)
    }
);
```

This means the cookie expires after 7 days.

---

## Deleting a cookie

```csharp
Response.Cookies.Delete("Username");
```

---

## Cookie flow

```text
First request
Browser ──────────→ Server

             Server creates cookie
                    ↓
Browser ←────────── Server
     stores:
     Username=Linus

Next request
Browser ──────────→ Server
     Cookie:
     Username=Linus
```

### Important

Cookies are **client-side**, but don't assume that means "safe."

The user can inspect and modify them.

For example, don't blindly trust:

```text
IsAdmin = true
```

stored in a normal cookie.

A malicious user can potentially modify it.

For authentication, ASP.NET Core provides proper authentication mechanisms rather than trusting arbitrary cookie values.

---

# 2. Query Strings

A **query string** is data placed directly in the URL.

For example:

```text
https://example.com/student?id=25
```

Here:

```text
?id=25
```

is the query string.

You can have multiple values:

```text
https://example.com/student?id=25&name=Linus
```

---

## Sending query string from a link

In Razor:

```cshtml
<a href="/Student/Details?id=25">
    View Student
</a>
```

The browser requests:

```text
/Student/Details?id=25
```

---

## Reading it in the controller

You can simply use a parameter:

```csharp
public IActionResult Details(int id)
{
    return Content($"Student ID: {id}");
}
```

ASP.NET MVC's model binding automatically gets:

```text
?id=25
```

and puts:

```csharp
id = 25
```

into your parameter.

You can also explicitly access the query string:

```csharp
public IActionResult Details()
{
    var id = Request.Query["id"];

    return Content($"Student ID: {id}");
}
```

---

## Multiple query parameters

URL:

```text
/student/search?name=Linus&age=20
```

Controller:

```csharp
public IActionResult Search(string name, int age)
{
    return Content($"Name: {name}, Age: {age}");
}
```

Very convenient.

---

# 3. Hidden Fields

A **hidden field** is an HTML `<input>` that isn't visible to the user on the page.

```html
<input type="hidden" name="StudentId" value="25">
```

The user doesn't see the field in the rendered page, but when the form is submitted, its value is sent to the server.

This is particularly useful in MVC forms.

---

## Example

Suppose you're editing a student.

Your Razor view:

```cshtml
<form asp-action="Update" method="post">

    <input type="hidden" name="Id" value="@Model.Id">

    <input type="text" name="Name" value="@Model.Name">

    <button type="submit">Update</button>

</form>
```

Suppose:

```text
Model.Id = 25
Model.Name = Linus
```

The browser effectively sends:

```text
Id=25
Name=Linus
```

to your controller.

Controller:

```csharp
[HttpPost]
public IActionResult Update(int id, string name)
{
    return Content($"Updating student {id}: {name}");
}
```

Result:

```text
Updating student 25: Linus
```

---

# Why use Hidden Fields?

Imagine you're displaying:

```text
Student ID: 25
Student Name: Linus
```

The user edits the name:

```text
Student Name: [Linus Bhattarai]
```

When they submit the form, you still need to know:

```text
Which student should I update?
```

You can keep the ID in a hidden field:

```cshtml
<input type="hidden" name="Id" value="@Model.Id">
```

So:

```text
Visible:
Name = Linus Bhattarai

Hidden:
Id = 25
```

Then the server receives both.

---

# The Important Security Problem

This is extremely important:

**Hidden does NOT mean secure.**

A user can inspect the HTML:

```html
<input type="hidden" name="Id" value="25">
```

and change it to:

```html
<input type="hidden" name="Id" value="999">
```

So never blindly trust hidden fields.

For example, this is dangerous:

```csharp
[HttpPost]
public IActionResult Delete(int id)
{
    db.Students.Remove(
        db.Students.Find(id)!
    );

    db.SaveChanges();

    return RedirectToAction("Index");
}
```

The user could potentially modify the hidden `id`.

The server should verify that the operation is authorized.

---

# Comparing the Three

| Feature                  | Cookie                         | Query String                    | Hidden Field                     |
| ------------------------ | ------------------------------ | ------------------------------- | -------------------------------- |
| Stored in                | Browser                        | URL                             | HTML page                        |
| Visible to user          | Usually not directly           | **Yes**                         | Not visually                     |
| Sent automatically?      | Usually with matching requests | As part of URL                  | Only when form submits           |
| Survives page navigation | Yes, depending on expiration   | Only if URL is preserved/shared | Only while that form/page exists |
| Typical use              | Preferences, identifiers       | Filtering/search/pagination     | Form-related IDs/data            |
| Can user modify it?      | Yes                            | Yes                             | Yes                              |
| Good for sensitive data? | Not arbitrary data             | ❌                               | ❌                                |

---

# When Should You Use Each?

Think of the question you're asking.

### "I want this information stored by the browser."

Use **Cookie**.

```csharp
Response.Cookies.Append("Theme", "dark");
```

Example:

```text
Theme = dark
Language = en
```

---

### "I want this value in the URL."

Use **Query String**.

```text
/products?category=laptop
```

Good for:

* Search
* Filtering
* Pagination
* Sorting
* IDs for retrieving resources

For example:

```text
/products?page=2
/student/details?id=25
/products?category=laptop&sort=price
```

---

### "I need to send this value when this form is submitted."

Use a **Hidden Field**.

```cshtml
<input type="hidden" name="StudentId" value="@Model.Id">
```

Good for:

* IDs
* Form state
* Values needed during form submission

---

# A Practical MVC Example

Suppose you're creating a student management system.

You have:

```text
Student List
     ↓
Details
     ↓
Edit
     ↓
Submit
```

### Query String

Go from list to details:

```text
/Student/Details?id=25
```

The ID is in the URL.

---

### Hidden Field

Edit form:

```cshtml
<input type="hidden" name="Id" value="@Model.Id">
```

When the form submits:

```text
POST /Student/Update

Id=25
Name=Linus
```

---

### Cookie

Remember the user's preferred page size:

```csharp
Response.Cookies.Append("PageSize", "20");
```

Next time:

```csharp
var pageSize = Request.Cookies["PageSize"];
```

---

# Client-Side vs Server-Side State

Now connect this with what you learned earlier.

### Client-side

```text
Cookie
Query String
Hidden Field
        ↓
Data lives on/within the client request
```

### Server-side

```text
Session
HttpContext.Items
TempData
Memory Cache
        ↓
Data managed/stored by server
```

Although **TempData deserves a little nuance** because its backing mechanism can involve cookies or server-side session depending on configuration. For your MVC fundamentals, remember its purpose: **passing temporary data between requests**.

---

# The Exam-Friendly Version

If you need to explain these in an exam or lab:

**Cookie:**

> A cookie is a small piece of data stored on the client's browser. It can be used to maintain information such as user preferences and identifiers across requests.

**Query String:**

> A query string is data appended to a URL after `?`. It is commonly used to pass parameters such as IDs, search terms, filters, and page numbers.

**Hidden Field:**

> A hidden field is an HTML form field that is not displayed to the user but is submitted with the form. It is commonly used to preserve values such as record IDs during form submissions.

And the critical distinction:

```text
Cookie       → Browser storage
Query String → URL
Hidden Field → HTML form
```

That's the mental model worth memorizing. The rest is just ASP.NET giving those three extremely ordinary web concepts a larger vocabulary.
