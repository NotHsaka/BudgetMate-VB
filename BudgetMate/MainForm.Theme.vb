Imports System
Imports System.Drawing
Imports System.Windows.Forms

' Runtime palette application. It intentionally leaves custom icon artwork alone.
Partial Class MainForm
    Private Sub ToggleTheme_Click(sender As Object, e As EventArgs) Handles ToggleTheme.Click
        If _budget Is Nothing Then Return
        _budget.SetDarkTheme(Not Theme.IsDark)
        ApplyCurrentTheme()
    End Sub

    Private Sub ApplyCurrentTheme()
        Me.BackColor = Theme.ColBg
        Me.ForeColor = Theme.ColInk
        ApplyThemeToControls(Me.Controls)
        If _walletGrid IsNot Nothing Then
            _walletGrid.BackColor = Theme.ColSurface
            For Each card As Control In _walletGrid.Controls
                card.BackColor = Theme.ColSoft
                For Each child As Control In card.Controls
                    If child.Name = "" Then child.ForeColor = If(child.Font.Bold, Theme.ColInk, Theme.ColMuted)
                Next
            Next
        End If
        Win.ApplyTitleBar(Me)
        Me.Invalidate(True)
    End Sub

    Private Sub ApplyThemeToControls(items As Control.ControlCollection)
        For Each c As Control In items
            If TypeOf c Is CardPanel Then
                c.BackColor = Theme.ColSurface
                c.ForeColor = Theme.ColInk
            ElseIf TypeOf c Is Panel AndAlso Not c.Name.StartsWith("MBLColor", StringComparison.Ordinal) Then
                c.BackColor = Theme.ColBg
                c.ForeColor = Theme.ColInk
            ElseIf TypeOf c Is Label Then
                Dim nm As String = c.Name.ToLowerInvariant()
                If nm.Contains("desc") OrElse nm.Contains("heading") OrElse nm.Contains("month") OrElse nm.Contains("count") Then
                    c.ForeColor = Theme.ColMuted
                Else
                    c.ForeColor = Theme.ColInk
                End If
                c.BackColor = Color.Transparent
            ElseIf TypeOf c Is TextBox OrElse TypeOf c Is ComboBox Then
                c.BackColor = Theme.ColSoft
                c.ForeColor = Theme.ColInk
            ElseIf TypeOf c Is Button Then
                c.BackColor = Theme.ColRaised
                c.ForeColor = Theme.ColInk
                If TypeOf c Is Button Then
                    DirectCast(c, Button).FlatStyle = FlatStyle.Flat
                End If
            End If
            If c.HasChildren Then ApplyThemeToControls(c.Controls)
        Next
    End Sub
End Class
