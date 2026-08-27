# Lab Works (Net Centric Computing)

**Remaining Lab tasks (Net Centric Computing) /Lab Works Part -2**

**[Follow all the instructions including Content Page format provided in Part-1]**

**[For each web applications. The output web page must contain your name, section and roll no. The outputs must include browser window with address bar.]**

**[For the lab tasks below, please write the source codes for Controllers, Models, Other required classes and the Razor view pages with corresponding names. Mention about the configuration used (write only the statements used). You must mention about the packages installed and namespaces used in your code. You must attach the screenshots after rendering in browser)]**

## 18. Exploring the .NET Core CLI and project structure.

### a) Use dotnet new, dotnet build, dotnet run, dotnet test, and dotnet publish on a sample console/web project; document each command and its output.

### b) Create a minimal ASP.NET Core app and use a tool (e.g., browser dev tools/Postman) to capture the raw HTTP request and response message format (headers, status line, body).

## 19. Create a ASP.Net Core MVC project including your name in application name

Include the following pages and features:

* A Razor view page that displays:

  * current date and time
  * your name and roll no.
  * multiplication table of your_roll_no + 1 (if your roll no. is 5, table of 6 should be displayed)
* Link this page in navbar with menu "MyRazorPage".
* A Model class “Student” that has some properties like StdID, Name, Address, Faculty etc. and annotate them with necessary validation attributes.
* Another Razor page to design a form that can be used to set values to the model object. (use built-in tag helpers and validation attributes while creating the form and model, server side validation should be performed)
* Link this page in navbar with menu "Create Student Record".
* Another Razor page to display the detail of a student.
* A Controller containing action methods to render above Razor view pages.
* When submit button is pressed, redirect to another Razor page and display the details of the student if the model validation is successful then show the error message next to the form fields in red color if model validation fails. [include screenshots of form submission with valid and invalid cases in report]

## 20. Create a project to illustrate dependency injection in ASP.Net Core.

Test the differences between AddScoped, AddSingleton and AddTransient Methods.

## 21. Create a ASP.Net core app and illustrate how to parse json data (demonstrate rea/write with json file and without file.)

## 22. Create a Console application in C# to demonstrate insert, read, update and delete operations in database.

Mention the table structure in theory and include the necessary screenshots of database values.

## 23. Create a CRUD application using ASP.Net Core MVC template using ADO.Net.

## 24. Create a CRUD application using ASP.Net Core MVC template using Entity Framework Core code first approach.

## 25. Create an ASP.Net Core application to demonstrate Entity Framework Core database first approach.

## 26. Create a simple Web API using ASP.Net Core and Entity Framework Core.

Show the API testing steps using both Postman and Swagger.

## 27. Create an ASP.Net application to demonstrate the server-side state management using Session state, HttpContext.Item, TempData and Memory Cache.

## 28. Create an ASP.Net application to demonstrate the client-side state management using Cookies, Query Strings and Hidden field.

## 29. Client side development in ASP.Net core

### a) Create a sign up form and perform validation using jQuery.

**[The page should display your name and RollNo at the top of the form]**

### b) Create the same simple registration form using Angular with basic client-side validation (required fields, valid email format).

### c) Create the same simple registration form using React, with equivalent basic validation.

### d) Create an Angular application having a navbar and a footer. The navbar should have two menus- home and calculator. The footer should display ©YourName, Current Date (year). The home page should display your own photo. When calculator menu is clicked a form should be displayed and the form should contain:

* two text fields to accept two numbers,
* a dropdown list with options add, subtract, and multiply,
* a button showing "Compute",
* a label displaying the computed result (after pressing the button) based on the option chosen

### e) Create similar application as in (d) using React.

## 30. Create an ASP.Net Core application to demonstrate authentication using [Authorize] and [AllowAnonymous] using Identity Framework Core.

[State Major Steps for Setup also]

## 31. using Identity Framework Core, implement authorization in ASP.Net Core Application using Roles, Claims, and Policies — e.g., define a role (such as "Admin") authorized to create, update, and delete a resource.

## 32. Create an Admin panel in ASP.Net core to manage users and roles.

Admin can add/edit/delete roles and claims form GUI. Admin can assign/revoke and update roles and claims to users through GUI. Test the authorization in this application. [You can print all parts of report for this task]

## 33. Securing ASP.Net Core App

* Demonstrate a Cross-Site Scripting (XSS) vulnerability and show how ASP.NET Core's output encoding defends against it.
* Demonstrate a SQL Injection vulnerability scenario (e.g., via unparameterized ADO.NET query) and show the parameterized-query fix that prevents it.
* Explain and demonstrate CSRF protection (anti-forgery tokens) in a form submission.
* Explain and demonstrate an Open Redirect vulnerability and its prevention.

## 34. Hosting an ASP.Net Core Application

### a) Demonstrate the application hosting using Docker in your local machine. Include necessary steps in steps with screenshots. [ Use any of the above applications for hosting].

### b) Host the application created in Lab 23 in any free hosting platform /server and show all the steps with screenshots.
