Imports System.Runtime.InteropServices
Imports InTheHand.Net.Sockets

Public Class frmLogin

    Private process As Process
    Private devices As New List(Of BluetoothDeviceInfo)
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

        If devices.Count > 0 Then
            Dim found = devices.FirstOrDefault(Function(x) x.DeviceAddress.ToString = "C0BDC86C989E")
            If found IsNot Nothing Then
                btnLogin.Enabled = True
            Else
                btnLogin.Enabled = False
            End If
        Else
            btnLogin.Enabled = False
        End If

    End Sub

    Private Sub timerFindProcess_Tick(sender As Object, e As EventArgs) Handles timerFindProcess.Tick
        'Windows sign-in
        '*Untitled - Notepad
        process = Process.GetProcesses.FirstOrDefault(Function(x) x.MainWindowTitle = "Windows sign-in")
    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles Me.Load
        Location = New Point(CInt((Screen.PrimaryScreen.Bounds.Width / 2) - (Width / 2)), CInt(Screen.PrimaryScreen.Bounds.Height / 3) * 2)
    End Sub

    Private Async Sub timerRefreshBTDevices_Tick(sender As Object, e As EventArgs) Handles timerRefreshBTDevices.Tick
        timerRefreshBTDevices.Stop()

        devices = Await GetBTDevicesInRangeAsync()

        timerRefreshBTDevices.Start()
    End Sub
End Class
