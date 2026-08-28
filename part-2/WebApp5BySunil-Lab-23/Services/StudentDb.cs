using Microsoft.Data.SqlClient;
using WebApp5BySunil_Lab_23.Models;

public class StudentDb
{
  private readonly string connectionString = "Server=localhost,1433;Database=StudentDb;User Id=sa;Password=Admin@123;TrustServerCertificate=True";

  SqlConnection GetConnection()
  {
    return new SqlConnection(connectionString);
  }

  private bool ExecuteQuery(string sql)
  {
    using SqlConnection connection = GetConnection();
    connection.Open();

    using SqlCommand command = new SqlCommand(sql, connection);

    int res = command.ExecuteNonQuery();

    return res > 0;
  }

  public List<Student>? GetAllStudents()
  {
    List<Student> s = new List<Student> { };
    string sql = "SELECT * FROM Students;";

    using SqlConnection connection = GetConnection();
    connection.Open();

    using SqlCommand command = new SqlCommand(sql, connection);
    using SqlDataReader r = command.ExecuteReader();

    while (r.Read())
    {
      s.Add(new Student((int)r["Roll"], (string)r["Name"], (string)r["Faculty"]));
    }

    return s;
  }

  public Student? GetStudent(int roll)
  {
    string sql = $"SELECT * FROM Students where Roll = {roll};";

    List<Student> s = new List<Student> { };

    using SqlConnection connection = GetConnection();
    connection.Open();

    using SqlCommand command = new SqlCommand(sql, connection);
    using SqlDataReader r = command.ExecuteReader();

    while (r.Read())
    {
      s.Add(new Student((int)r["Roll"], (string)r["Name"], (string)r["Faculty"]));
    }

    return s[0];
  }

  public bool CreateStudent(Student s)
  {
    string sql = $"INSERT INTO Students VALUES ('{s.Name}', '{s.Faculty}');";

    return ExecuteQuery(sql);
  }

  public bool UpdateStudent(Student s)
  {
    string sql = $"Update Students SET Name = '{s.Name}', Faculty = '{s.Faculty}' where Roll = {s.Roll};";
    return ExecuteQuery(sql);
  }

  public bool DeleteStudent(int roll)
  {
    string sql = $"DELETE FROM Students where Roll = {roll};";
    return ExecuteQuery(sql);
  }
}
