Imports System
Imports System.Windows.Forms

' How a sign-in / sign-up screen was left.
Public Enum AuthOutcome
    Closed        ' window closed with X: quit the app
    SignedIn      ' signed in or created an account: open the dashboard
    SwitchScreen  ' "Create an account" / "Sign in" link: show the other screen
    Guest         ' "Continue without an account": open the dashboard signed out
End Enum

' Runs the sign-in <-> sign-up screens until the person is in, skips, or closes the window.
Public NotInheritable Class AuthFlow

    Private Sub New()
    End Sub

    ' Returns True when the dashboard should open, False when the app should quit.
    Public Shared Function Run() As Boolean
        Dim showLogin As Boolean = True
        Dim email As String = String.Empty

        Do
            Dim outcome As AuthOutcome
            If showLogin Then
                Using form As New LoginForm(email)
                    form.ShowDialog()
                    outcome = form.Outcome
                    email = form.EnteredEmail
                End Using
            Else
                Using form As New SignUpForm(email)
                    form.ShowDialog()
                    outcome = form.Outcome
                    email = form.EnteredEmail
                End Using
            End If

            Select Case outcome
                Case AuthOutcome.SignedIn, AuthOutcome.Guest
                    Return True
                Case AuthOutcome.SwitchScreen
                    showLogin = Not showLogin   ' the typed email carries over to the other screen
                Case Else
                    Return False
            End Select
        Loop
    End Function

End Class
