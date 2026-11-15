Imports System
Imports System.Windows.Forms

Public Module Program

    <STAThread>
    Public Sub Main()
        Application.SetHighDpiMode(HighDpiMode.SystemAware)
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        ' Sign in (or create an account) first. Closing that window quits; "Continue without an account" skips.
        ' "Log out" on the dashboard comes back here; closing the dashboard any other way quits.
        Do
            If Not AuthFlow.Run() Then Return

            Dim main As New MainForm()
            Application.Run(main)
            If Not main.LoggedOut Then Return
        Loop
    End Sub

End Module
