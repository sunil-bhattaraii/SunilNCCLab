using Microsoft.Data.SqlClient;

public static class SecurityDb
{
  // Creates the Users table (if missing) and inserts sample rows.
  public static async Task InitializeAsync(string connectionString)
  {
    // Ensure the demo database itself exists (analogous to EF migrations).
    var connBuilder = new SqlConnectionStringBuilder(connectionString);
    connBuilder.InitialCatalog = "master";
    await using (var masterConn = new SqlConnection(connBuilder.ConnectionString))
    {
      await masterConn.OpenAsync();
      await using var create = new SqlCommand(
        "IF DB_ID(N'SecurityDb') IS NULL CREATE DATABASE SecurityDb;", masterConn);
      await create.ExecuteNonQueryAsync();
    }

    const string setup = """
      IF OBJECT_ID(N'Users', N'U') IS NULL
      BEGIN
          CREATE TABLE Users
          (
              Id       INT IDENTITY(1,1) PRIMARY KEY,
              Name     NVARCHAR(50),
              Password NVARCHAR(50),
              Role     NVARCHAR(20)
          );

          INSERT INTO Users (Name, Password, Role) VALUES
              (N'Ram',  N'ram123',  N'Admin'),
              (N'Sita', N'sita123', N'User'),
              (N'Hari', N'hari123', N'User'),
              (N'Gita', N'gita123', N'User');
      END
      """;

    await using var conn = new SqlConnection(connectionString);
    await conn.OpenAsync();
    await using var cmd = new SqlCommand(setup, conn);
    await cmd.ExecuteNonQueryAsync();
  }
}