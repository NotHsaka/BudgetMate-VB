Imports System.Security.Cryptography
Imports MySql.Data.MySqlClient

' Saves accounts in the MySQL database (budgetmate_app) that DatabaseConnection already points to.
' Passwords are never stored: only a salted PBKDF2 hash is.
Public NotInheritable Class UserStore

    Private Const Iterations As Integer = 210000
    Private Const SaltSize As Integer = 16
    Private Const HashSize As Integer = 32

    Private Const CreateTableSql As String =
        "CREATE TABLE IF NOT EXISTS users (" &
        "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY, " &
        "full_name VARCHAR(100) NOT NULL, " &
        "email VARCHAR(190) NOT NULL UNIQUE, " &
        "password_hash VARCHAR(255) NOT NULL, " &
        "created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP)"

    Private Sub New()
    End Sub

    ' Returns an empty string when the account was created, otherwise a message to show the user.
    Public Shared Function CreateUser(fullName As String, email As String, password As String) As String
        Dim passwordHash As String = HashPassword(password)
        Try
            Using connection As MySqlConnection = DatabaseConnection.GetConnection()
                connection.Open()

                Using create As New MySqlCommand(CreateTableSql, connection)
                    create.ExecuteNonQuery()
                End Using

                Using insert As New MySqlCommand(
                    "INSERT INTO users (full_name, email, password_hash) VALUES (@name, @email, @hash)", connection)
                    insert.Parameters.AddWithValue("@name", fullName)
                    insert.Parameters.AddWithValue("@email", email)
                    insert.Parameters.AddWithValue("@hash", passwordHash)
                    insert.ExecuteNonQuery()
                End Using
            End Using
            Return String.Empty

        Catch ex As MySqlException When ex.Number = 1062
            Return "An account with that email already exists."
        Catch ex As MySqlException When ex.Number = 1049
            Return "The database ""budgetmate_app"" doesn't exist yet. Create it in phpMyAdmin, then try again."
        Catch ex As MySqlException
            Return "Couldn't reach the database: " & ex.Message
        End Try
    End Function

    Private Shared Function HashPassword(password As String) As String
        Dim salt As Byte() = RandomNumberGenerator.GetBytes(SaltSize)
        Dim hash As Byte() = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize)
        Return String.Join("$", "pbkdf2-sha256", Iterations.ToString(), Convert.ToBase64String(salt), Convert.ToBase64String(hash))
    End Function

    ' Not used by sign-up; ready for a login form later.
    Public Shared Function VerifyPassword(password As String, stored As String) As Boolean
        Dim parts As String() = stored.Split("$"c)
        If parts.Length <> 4 OrElse parts(0) <> "pbkdf2-sha256" Then Return False
        Dim iterationCount As Integer
        If Not Integer.TryParse(parts(1), iterationCount) Then Return False
        Dim salt As Byte() = Convert.FromBase64String(parts(2))
        Dim expected As Byte() = Convert.FromBase64String(parts(3))
        Dim actual As Byte() = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterationCount, HashAlgorithmName.SHA256, expected.Length)
        Return CryptographicOperations.FixedTimeEquals(actual, expected)
    End Function

End Class
