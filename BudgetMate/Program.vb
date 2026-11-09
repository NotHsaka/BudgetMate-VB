Imports System
Imports System.Windows.Forms

Public Module Program

    <STAThread>
    Public Sub Main()
        Application.SetHighDpiMode(HighDpiMode.SystemAware)
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        ' Temporary hook: show the sign-up form first. Close it or click "Skip for now" to go straight to the app.
        ' (Delete these three lines to start directly on the dashboard again.)
        Using signUp As New SignUpForm()
            signUp.ShowDialog()
        End Using

        Application.Run(New MainForm())
    End Sub

End Module
