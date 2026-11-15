Imports System

' Who is signed in right now (Nothing/empty when the person chose "continue without an account").
Public NotInheritable Class UserSession

    Private Sub New()
    End Sub

    Public Shared Property FullName As String = String.Empty
    Public Shared Property Email As String = String.Empty

    Public Shared ReadOnly Property IsSignedIn As Boolean
        Get
            Return Email.Length > 0
        End Get
    End Property

    Public Shared Sub SignIn(fullName As String, email As String)
        UserSession.FullName = fullName
        UserSession.Email = email
    End Sub

    Public Shared Sub SignOut()
        FullName = String.Empty
        Email = String.Empty
    End Sub

End Class
