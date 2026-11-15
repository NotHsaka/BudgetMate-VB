Imports System.Linq
Imports System.Net.Mail
Imports System.Threading.Tasks
Imports System.Windows.Forms

Public Class SignUpForm

    Public ReadOnly Property CreatedEmail As String = String.Empty
    Public ReadOnly Property Outcome As AuthOutcome = AuthOutcome.Closed
    Public ReadOnly Property EnteredEmail As String = String.Empty

    Public Sub New(Optional prefillEmail As String = "")
        InitializeComponent()
        txtEmail.Text = prefillEmail
        _EnteredEmail = prefillEmail
    End Sub

    Private Async Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Dim fullName As String = txtFullName.Text.Trim()
        Dim email As String = txtEmail.Text.Trim().ToLowerInvariant()
        Dim password As String = txtPassword.Text
        _EnteredEmail = email

        Dim problem As String = CheckInputs(fullName, email, password, txtConfirm.Text)
        If problem.Length > 0 Then
            ShowError(problem)
            Return
        End If

        SetBusy(True)
        ShowError(String.Empty)
        Dim failure As String
        Try
            failure = Await Task.Run(Function() UserStore.CreateUser(fullName, email, password))
        Catch ex As Exception
            failure = "Something went wrong: " & ex.Message
        End Try
        If IsDisposed Then Return
        SetBusy(False)

        If failure = UserStore.EmailTakenMessage Then
            ' The account already exists, so offer the logical next step instead of a dead end.
            Dim answer As DialogResult = MessageBox.Show(Me,
                "There's already an account for " & email & "." & vbCrLf & vbCrLf & "Would you like to sign in instead?",
                "Account already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If answer = DialogResult.Yes Then
                GoToSignIn()
            Else
                ShowError(failure)
                txtEmail.Focus()
            End If
            Return
        End If
        If failure.Length > 0 Then
            ShowError(failure)
            Return
        End If

        ' A new account is signed in straight away.
        _CreatedEmail = email
        UserSession.SignIn(fullName, email)
        _Outcome = AuthOutcome.SignedIn
        DialogResult = DialogResult.OK
        Close()
    End Sub

    ' Returns "" when everything is fine, otherwise the first thing to fix (and focuses that box).
    Private Function CheckInputs(fullName As String, email As String, password As String, confirm As String) As String
        If fullName.Length = 0 Then
            txtFullName.Focus()
            Return "Please enter your full name."
        End If
        If Not IsValidEmail(email) Then
            txtEmail.Focus()
            Return "Please enter a valid email address."
        End If
        If password.Length < 8 Then
            txtPassword.Focus()
            Return "Your password needs at least 8 characters."
        End If
        If Not (password.Any(Function(c) Char.IsLetter(c)) AndAlso password.Any(Function(c) Char.IsDigit(c))) Then
            txtPassword.Focus()
            Return "Your password needs at least one letter and one number."
        End If
        If password <> confirm Then
            txtConfirm.Focus()
            Return "The passwords don't match."
        End If
        Return String.Empty
    End Function

    Private Shared Function IsValidEmail(value As String) As Boolean
        If value.Length = 0 Then Return False
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
        btnCreate.Enabled = Not busy
        btnCreate.Text = If(busy, "Creating account...", "Create account")
        UseWaitCursor = busy
    End Sub

    Private Sub chkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        txtPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
        txtConfirm.UseSystemPasswordChar = Not chkShowPassword.Checked
    End Sub

    ' Clear the red message as soon as the person starts fixing things
    Private Sub AnyField_TextChanged(sender As Object, e As EventArgs) Handles txtFullName.TextChanged,
        txtEmail.TextChanged, txtPassword.TextChanged, txtConfirm.TextChanged
        If btnCreate.Enabled Then lblError.Text = String.Empty
    End Sub

    Private Sub linkSkip_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkSkip.LinkClicked
        UserSession.SignOut()
        _Outcome = AuthOutcome.Guest
        DialogResult = DialogResult.Ignore
        Close()
    End Sub

    Private Sub linkSignIn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkSignIn.LinkClicked
        GoToSignIn()
    End Sub

    ' Back to the sign-in screen, carrying over whatever email was typed.
    Private Sub GoToSignIn()
        _EnteredEmail = txtEmail.Text.Trim().ToLowerInvariant()
        _Outcome = AuthOutcome.SwitchScreen
        DialogResult = DialogResult.Retry
        Close()
    End Sub

    Private Sub lblLogo_Click(sender As Object, e As EventArgs) Handles lblLogo.Click

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles LogoPBSI.Click

    End Sub

    Private Sub lblSubtitle_Click(sender As Object, e As EventArgs) Handles lblSubtitle.Click

    End Sub
End Class
