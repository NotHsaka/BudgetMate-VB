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
        "user_id VARCHAR(64) NOT NULL, " &
        "username VARCHAR(50) NOT NULL, " &
        "full_name VARCHAR(100) NOT NULL, " &
        "email VARCHAR(150) NOT NULL, " &
        "password_hash VARCHAR(255) NOT NULL, " &
        "created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, " &
        "updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, " &
        "PRIMARY KEY (user_id), " &
        "UNIQUE KEY uq_users_username (username), " &
        "UNIQUE KEY uq_users_email (email)" &
        ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

    ' Shown when sign-up hits an email that is already registered (the sign-up form offers to switch to sign-in).
    Public Const EmailTakenMessage As String = "An account with that email already exists."
    Public Const BadLoginMessage As String = "Incorrect email or password."

    ' A real-looking hash that is checked when the email is unknown, so a wrong email and a wrong
    ' password take about the same time and give the same answer.
    Private Shared ReadOnly DummyHash As String = HashPassword("not-a-real-password-1")

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
    "INSERT INTO users (user_id, username, full_name, email, password_hash) " &
    "VALUES (@id, @username, @name, @email, @hash)", connection)
                    insert.Parameters.AddWithValue("@id", Guid.NewGuid().ToString())
                    insert.Parameters.AddWithValue("@username", email.Trim().ToLower())
                    insert.Parameters.AddWithValue("@name", fullName)
                    insert.Parameters.AddWithValue("@email", email)
                    insert.Parameters.AddWithValue("@hash", passwordHash)
                    insert.ExecuteNonQuery()
                End Using
            End Using
            Return String.Empty

        Catch ex As MySqlException When ex.Number = 1062 AndAlso ex.Message.Contains("uq_users_email")
            Return EmailTakenMessage
        Catch ex As MySqlException When ex.Number = 1049
            Return "The database ""budgetmate_app"" doesn't exist yet. Create it in phpMyAdmin, then try again."
        Catch ex As MySqlException
            Return "Couldn't reach the database: " & ex.Message
        End Try
    End Function


    ' Checks an email + password. Returns an empty string and fills fullName/normalised email on success,
    ' otherwise a message to show the user.
    Public Shared Function Authenticate(email As String, password As String, ByRef fullName As String) As String
        fullName = String.Empty
        Try
            Using connection As MySqlConnection = DatabaseConnection.GetConnection()
                connection.Open()

                Dim storedHash As String = Nothing
                Using find As New MySqlCommand("SELECT full_name, password_hash FROM users WHERE email = @email LIMIT 1", connection)
                    find.Parameters.AddWithValue("@email", email)
                    Using reader As MySqlDataReader = find.ExecuteReader()
                        If reader.Read() Then
                            fullName = reader.GetString("full_name")
                            storedHash = reader.GetString("password_hash")
                        End If
                    End Using
                End Using

                If storedHash Is Nothing Then
                    VerifyPassword(password, DummyHash)
                    Return BadLoginMessage
                End If
                If Not VerifyPassword(password, storedHash) Then
                    fullName = String.Empty
                    Return BadLoginMessage
                End If
            End Using
            Return String.Empty

        Catch ex As MySqlException When ex.Number = 1146
            ' The users table is only created by the first sign-up.
            Return BadLoginMessage
        Catch ex As MySqlException When ex.Number = 1049
            Return "The database ""budgetmate_app"" doesn't exist yet. Create it in phpMyAdmin, then try again."
        Catch ex As MySqlException
            Return "Couldn't reach the database: " & ex.Message
        End Try
    End Function

    ' True when an account with this email exists. Used to pre-fill sign-in; false if the check can't be made.
    Public Shared Function EmailExists(email As String) As Boolean
        Try
            Using connection As MySqlConnection = DatabaseConnection.GetConnection()
                connection.Open()
                Using find As New MySqlCommand("SELECT 1 FROM users WHERE email = @email LIMIT 1", connection)
                    find.Parameters.AddWithValue("@email", email)
                    Return find.ExecuteScalar() IsNot Nothing
                End Using
            End Using
        Catch ex As MySqlException
            Return False
        End Try
    End Function

    Private Shared Function HashPassword(password As String) As String
        Dim salt As Byte() = RandomNumberGenerator.GetBytes(SaltSize)
        Dim hash As Byte() = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize)
        Return String.Join("$", "pbkdf2-sha256", Iterations.ToString(), Convert.ToBase64String(salt), Convert.ToBase64String(hash))
    End Function

    ' Used by Authenticate (sign-in).
    Public Shared Function VerifyPassword(password As String, stored As String) As Boolean
        Dim parts As String() = stored.Split("$"c)
        If parts.Length <> 4 OrElse parts(0) <> "pbkdf2-sha256" Then Return False
        Dim iterationCount As Integer
        If Not Integer.TryParse(parts(1), iterationCount) Then Return False
        Dim salt As Byte()
        Dim expected As Byte()
        Try
            salt = Convert.FromBase64String(parts(2))
            expected = Convert.FromBase64String(parts(3))
        Catch ex As FormatException
            Return False
        End Try
        Dim actual As Byte() = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterationCount, HashAlgorithmName.SHA256, expected.Length)
        Return CryptographicOperations.FixedTimeEquals(actual, expected)
    End Function

End Class
