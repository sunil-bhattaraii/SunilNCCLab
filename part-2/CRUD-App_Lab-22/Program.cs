using Microsoft.Data.SqlClient;

string connectionString =
    "Server=localhost,1433;Database=CollegeDb;User Id=sa;Password=Admin@123;TrustServerCertificate=True;";

using SqlConnection connection = new SqlConnection(connectionString);

connection.Open();

void ViewStudents()
{
  string sql = "SELECT * FROM Students";

  using SqlCommand command = new SqlCommand(sql, connection);

  using SqlDataReader r = command.ExecuteReader();
  Console.WriteLine("roll name");

  while (r.Read())
  {
    var name = r["Name"];
    var roll = r["Roll"];

    Console.WriteLine($"{roll}.   {name}");
  }
}

void CreateStudent()
{
  Console.Write("Enter Student Details: \nName: ");
  string name = Console.ReadLine()!;

  string sql = $"INSERT INTO Students VALUES ('{name}');";
  using SqlCommand command = new SqlCommand(sql, connection);

  int n = command.ExecuteNonQuery();

  Console.WriteLine($"Student Added Successfully");
}

void UpdateStudent()
{
  Console.Write("Update Student\nRoll No.: ");
  int roll = int.Parse(Console.ReadLine()!);

  Console.Write("Updated Student Name: ");
  string newName = Console.ReadLine()!;

  string sql = $"Update Students SET Name = '{newName}' WHERE Roll = {roll} ;";
  using SqlCommand command = new SqlCommand(sql, connection);

  int n = command.ExecuteNonQuery();

  if (n == 0)
  {
    Console.WriteLine("The student with the given roll no doesnt exist");
  }
  else
  {
    Console.WriteLine("Student updated successfully");
  }
}

void DeleteStudent()
{
  Console.Write("Enter Roll No. to delete: ");
  int roll = int.Parse(Console.ReadLine()!);

  string sql = $"DELETE FROM Students where ROll = {roll}";

  SqlCommand command = new SqlCommand(sql, connection);
  int n = command.ExecuteNonQuery();

  if (n == 0)
  {
    Console.WriteLine("The student with the given roll no doesnt exist");
  }
  else
  {
    Console.WriteLine("Student Deleted successfully");
  }
}


Console.Write("Lab 22: CRUD App menu: \n\n1. Create a new Student\n2. View all Students\n3. Update Student Data\n4. Delete Student\n5. Any other key to exit");
bool WantsToExit = false;

while (true)
{

  Console.Write("\n\nEnter your choice: ");

  int ch = int.Parse(Console.ReadLine()!);

  switch (ch)
  {
    case 1:
      CreateStudent();
      break;

    case 2:
      ViewStudents();
      break;

    case 3:
      UpdateStudent();
      break;

    case 4:
      DeleteStudent();
      break;

    default:
      WantsToExit = true;
      break;
  }

  if (WantsToExit)
  {
    Console.WriteLine("\n\nProgram Terminated ...");
    break;
  }
}
