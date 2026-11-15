Imports System.Net.Mail
Imports System.Threading.Tasks
Imports System.Windows.Forms

Public Class LoginForm

    Public ReadOnly Property Outcome As AuthOutcome = AuthOutcome.Closed
    Public ReadOnly Property EnteredEmail As String = String.Empty

    Public Sub New(Optional prefillEmail As String = "")
        InitializeComponent()
        txtEmail.Text = prefillEmail
        _EnteredEmail = prefillEmail
    End Sub

    Private Sub LoginForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ' Start typing in whichever box still needs something.
        If txtEmail.Text.Length > 0 Then txtPassword.Focus() Else txtEmail.Focus()
    End Sub

    Private Async Sub btnSignIn_Click(sender As Object, e As EventArgs) Handles btnSignIn.Click
        Dim email As String = txtEmail.Text.Trim().ToLowerInvariant()
        Dim password As String = txtPassword.Text
        _EnteredEmail = email

        If email.Length = 0 OrElse Not IsValidEmail(email) Then
            ShowError("Please enter the email you signed up with.")
            txtEmail.Focus()
            Return
        End If
        If password.Length = 0 Then
            ShowError("Please enter your password.")
            txtPassword.Focus()
            Return
        End If

        SetBusy(True)
        ShowError(String.Empty)
        Dim fullName As String = String.Empty
        Dim failure As String
        Try
            failure = Await Task.Run(Function() UserStore.Authenticate(email, password, fullName))
        Catch ex As Exception
            failure = "Something went wrong: " & ex.Message
        End Try
        If IsDisposed Then Return
        SetBusy(False)

        If failure.Length > 0 Then
            ShowError(failure)
            txtPassword.SelectAll()
            txtPassword.Focus()
            Return
        End If

        UserSession.SignIn(fullName, email)
        _Outcome = AuthOutcome.SignedIn
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Shared Function IsValidEmail(value As String) As Boolean
        Try
            Dim address As New MailAddress(value)
            Return address.Address = value AndAlso address.Host.Contains(".")
        Catch ex As FormatException
            Return False
        End Try
    End Function

    Private Sub ShowError(message As String)
        lblError.Text = message
    End Sub

    Private Sub SetBusy(busy As Boolean)
        btnSignIn.Enabled = Not busy
        btnSignIn.Text = If(busy, "Signing in...", "Sign in")
        UseWaitCursor = busy
    End Sub

    Private Sub chkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        txtPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
    End Sub

    Private Sub AnyField_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged, txtPassword.TextChanged
        If btnSignIn.Enabled Then lblError.Text = String.Empty
    End Sub

    Private Sub linkCreate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkCreate.LinkClicked
        _EnteredEmail = txtEmail.Text.Trim().ToLowerInvariant()
        _Outcome = AuthOutcome.SwitchScreen
        DialogResult = DialogResult.Retry
        Close()
    End Sub

    Private Sub linkSkip_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkSkip.LinkClicked
        UserSession.SignOut()
        _Outcome = AuthOutcome.Guest
        DialogResult = DialogResult.Ignore
        Close()
    End Sub

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
