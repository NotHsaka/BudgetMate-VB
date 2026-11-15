Imports System
Imports System.Drawing
Imports System.Windows.Forms

' =====================================================================
' Log out / Sign in button in the top bar.
'   * Signed in : "Log out" asks to confirm, then returns to the sign-in screen.
'   * Guest     : "Sign in" goes straight back to the sign-in screen.
' Program.Main reads LoggedOut to decide whether to show the sign-in screen again or quit.
' =====================================================================
Partial Class MainForm

    Private _btnAccount As Button

    ' True when the window was closed because the person logged out (not because they quit).
    Public Property LoggedOut As Boolean

    Private Sub InitAccountButton()
        _btnAccount = New Button()
        _btnAccount.Name = "AccountButton"
        _btnAccount.Text = If(UserSession.IsSignedIn, "Log out", "Sign in")
        _btnAccount.Font = AddTransaction.Font
        _btnAccount.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _btnAccount.FlatStyle = FlatStyle.Flat
        _btnAccount.TabStop = False
        _btnAccount.Size = New Size(CInt(90 * _scaleK), AddTransaction.Height)

        ' Sits just left of the theme toggle, level with "+ Add Transaction".
        Dim gap As Integer = CInt(16 * _scaleK)
        _btnAccount.Location = New Point(ToggleTheme.Left - gap - _btnAccount.Width, AddTransaction.Top)

        Dim tip As New ToolTip()
        tip.SetToolTip(_btnAccount,
            If(UserSession.IsSignedIn,
               "Signed in as " & UserSession.FullName & " (" & UserSession.Email & ")",
               "You're using PesoMint without an account"))

        AddHandler _btnAccount.Click, AddressOf AccountButton_Click
        pnlHeader.Controls.Add(_btnAccount)
    End Sub

    Private Sub AccountButton_Click(sender As Object, e As EventArgs)
        If UserSession.IsSignedIn Then
            Dim answer As DialogResult = MessageBox.Show(Me,
                "Log out of " & UserSession.Email & "?",
                Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If answer <> DialogResult.Yes Then Return
        End If

        ' Everything is saved as you go, so there is nothing to flush here.
        UserSession.SignOut()
        LoggedOut = True
        Close()
    End Sub

End Class
