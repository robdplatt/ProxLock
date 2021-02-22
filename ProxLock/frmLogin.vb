Imports System.Runtime.InteropServices
Imports InTheHand.Net.Sockets

Public Class frmLogin

    Private _deviceIsInRange As Boolean
    Private Property deviceIsInRange As Boolean
        Get
            Return _deviceIsInRange
        End Get
        Set(value As Boolean)
            _deviceIsInRange = value

            If value Then
                If settings.AutoUnlock Then btnLogin.Enabled = True
            Else
                btnLogin.Enabled = False

                If settings.AutoUnlock Then
                    LockWorkStation()
                End If
            End If
        End Set
    End Property

    Private process As Process
    Private devices As New List(Of BluetoothDeviceInfo)

    Private settings As Settings

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles Me.Load
        Location = New Point(CInt((Screen.PrimaryScreen.Bounds.Width / 2) - (Width / 2)), CInt(Screen.PrimaryScreen.Bounds.Height / 3) * 2)
        settings = New Settings()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If process Is Nothing Then Exit Sub

        'TODO test connection on login?
        timerKeepOnTop.Stop()


        SetForegroundWindow(process.MainWindowHandle)

        SendKeys.Send(settings.Password)
        If settings.PasswordType = Settings.PasswordTypes.Password Then SendKeys.Send(Environment.NewLine)

        timerKeepOnTop.Start()
    End Sub

    Private Sub timerKeepOnTop_Tick(sender As Object, e As EventArgs) Handles timerKeepOnTop.Tick
        TopMost = True
    End Sub

    Private Sub timerFindProcess_Tick(sender As Object, e As EventArgs) Handles timerFindProcess.Tick
        timerFindProcess.Stop()
        timerFindProcess.Interval = 3000

        GetWindowsSignInProcessAsync()

        timerFindProcess.Start()
    End Sub

    Private Async Sub GetWindowsSignInProcessAsync()
        Await Task.Run(Sub()
                           process = Process.GetProcesses.FirstOrDefault(Function(x) x.MainWindowTitle = "Windows sign-in")
                       End Sub)
    End Sub

    Private Async Sub timerRefreshBTDevices_Tick(sender As Object, e As EventArgs) Handles timerRefreshBTDevices.Tick
        timerRefreshBTDevices.Stop()
        timerRefreshBTDevices.Interval = 30000

        Try
            Debug.WriteLine("Scanning for BlueTooth devices...")
            devices = Await GetBTDevicesAsync()
            Debug.WriteLine($"BT devices found: {devices.Count}")

            For Each device In devices
                Debug.WriteLine($"{device.DeviceName} {device.DeviceAddress} {device.Rssi} {device.LastSeen}")
            Next

        Catch ex As Exception
        End Try

        timerRefreshBTDevices.Start()
    End Sub

    Private Async Sub timerTestInRange_Tick(sender As Object, e As EventArgs) Handles timerTestInRange.Tick
        timerTestInRange.Stop()
        timerTestInRange.Interval = 5000

        Try
            If devices.Count = 0 Then Exit Try

            For Each deviceId In settings.RegisteredDevices
                Dim device = devices.FirstOrDefault(Function(x) x.DeviceAddress.ToString = deviceId)
                If device IsNot Nothing Then
                    Debug.WriteLine("Checking to see if device is in range...")
                    deviceIsInRange = Await TestDeviceInRangeAsync(device)
                    Debug.WriteLine($"Device in range: {deviceIsInRange.ToString}")
                End If

                If deviceIsInRange Then Exit For
            Next

        Catch ex As Exception
        End Try

        timerTestInRange.Start()
    End Sub
End Class
