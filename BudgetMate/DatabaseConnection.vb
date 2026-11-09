Imports MySql.Data.MySqlClient

Public Class DatabaseConnection

    Private Shared ReadOnly connectionString As String =
        "Server=localhost;Port=3306;Database=budgetmate_app;Uid=root;Pwd=;"

    Public Shared Function GetConnection() As MySqlConnection
        Return New MySqlConnection(ConnectionString)
    End Function

    Public Shared Function TestConnection() As String
        Try
            Using connection As MySqlConnection = GetConnection()
                connection.Open()
                Return "Successfully connected to the BudgetMate database!"
            End Using

        Catch ex As Exception
            Return "Connection failed: " & ex.Message
        End Try
    End Function

End Class