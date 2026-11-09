Imports System.Windows.Forms

Public Class DatabaseTest

    Public Shared Sub TestConnection()

        Dim result As String = DatabaseConnection.TestConnection()

        If result.StartsWith("Successfully") Then

            MessageBox.Show(
                result,
                "Database Connection",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

        Else

            MessageBox.Show(
                result,
                "Connection Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End If

    End Sub

End Class