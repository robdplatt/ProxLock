Imports System.Runtime.InteropServices

Public Class frmLogin

    Private process As Process

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If process Is Nothing Then Exit Sub
        timerKeepOnTop.Stop()

        SetForegroundWindow(process.MainWindowHandle)

        SendKeys.Send("")
        SendKeys.Send(Environment.NewLine)

        timerKeepOnTop.Start()
    End Sub


    Private Sub timerKeepOnTop_Tick(sender As Object, e As EventArgs) Handles timerKeepOnTop.Tick
        TopMost = True
    End Sub

    Private Sub timerFindProcess_Tick(sender As Object, e As EventArgs) Handles timerFindProcess.Tick
        'Windows sign-in
        '*Untitled - Notepad
        process = Process.GetProcesses.FirstOrDefault(Function(x) x.MainWindowTitle = "Windows sign-in")
    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles Me.Load
        Location = New Point(CInt((Screen.PrimaryScreen.Bounds.Width / 2) - (Width / 2)), CInt(Screen.PrimaryScreen.Bounds.Height / 3) * 2)
    End Sub
End Class
