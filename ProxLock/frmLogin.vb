﻿Imports System.Runtime.InteropServices
Imports InTheHand.Net.Sockets
Imports Microsoft.Win32

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

                If settings.AutoUnlock And Not ComputerIsLocked Then
                    'LockWorkStation()
                End If
            End If
        End Set
    End Property

    Private ComputerIsLocked As Boolean

    Private process As Process
    Private devices As New List(Of BluetoothDeviceInfo)

    Private settings As Settings

    Private client As BluetoothClient

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles Me.Load
        Location = New Point(CInt((Screen.PrimaryScreen.Bounds.Width / 2) - (Width / 2)), CInt(Screen.PrimaryScreen.Bounds.Height / 3) * 2)
        settings = New Settings()
        AddHandler SystemEvents.SessionSwitch, AddressOf SessionSwitched
    End Sub

    Private Sub SessionSwitched(sender As Object, e As SessionSwitchEventArgs)
        If e.Reason = SessionSwitchReason.SessionLock Then
            ComputerIsLocked = True
        End If

        If e.Reason = SessionSwitchReason.SessionUnlock Then
            ComputerIsLocked = False
        End If
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
        If Not ComputerIsLocked Then Exit Sub
        TopMost = True
    End Sub

    Private Sub timerFindProcess_Tick(sender As Object, e As EventArgs) Handles timerFindProcess.Tick
        timerFindProcess.Stop()
        timerFindProcess.Interval = 10000

        GetWindowsSignInProcessAsync()

        timerFindProcess.Start()
    End Sub

    Private Async Sub GetWindowsSignInProcessAsync()
        Await Task.Run(Sub()
                           'process = Process.GetProcesses.FirstOrDefault(Function(x) x.MainWindowTitle = "Windows sign-in")
                           process = Process.GetProcessesByName("LogonUI").FirstOrDefault
                       End Sub)
    End Sub

    Private Async Sub timerRefreshBTDevices_Tick(sender As Object, e As EventArgs) Handles timerRefreshBTDevices.Tick
        timerRefreshBTDevices.Stop()
        timerRefreshBTDevices.Interval = 30000

        Try
            Debug.WriteLine("Scanning for Bluetooth devices...")
            devices = Await GetBTDevicesAsync()
            Debug.WriteLine($"  BT devices found: {devices.Count}")

            For Each device In devices
                Debug.WriteLine($"      {device.DeviceName} {device.DeviceAddress} {device.Rssi} {device.LastSeen}")
            Next

        Catch ex As Exception
        End Try

        timerRefreshBTDevices.Start()
    End Sub

    Private Async Sub timerTestInRange_Tick(sender As Object, e As EventArgs) Handles timerTestInRange.Tick
        'If Not ComputerIsLocked Then Exit Sub
        timerTestInRange.Stop()
        timerTestInRange.Interval = 1000



        Try
            If client?.GetStream?.Socket?.IsConnected Then
                Debug.WriteLine("Device connected")
                deviceIsInRange = True
            Else
                Debug.WriteLine("Device disconnected")
                deviceIsInRange = False

                For Each deviceId In settings.RegisteredDevices
                    Dim device = devices.FirstOrDefault(Function(x) x.DeviceAddress.ToString = deviceId)
                    If device IsNot Nothing Then
                        If client IsNot Nothing Then client.Dispose()
                        client = Await ConnectAsync(device)
                    End If
                    If client?.Connected Then Exit For
                Next
            End If


        Catch ex As Exception
        End Try

        'Try
        '    If devices.Count = 0 Then Exit Try

        '    For Each deviceId In settings.RegisteredDevices
        '        Dim device = devices.FirstOrDefault(Function(x) x.DeviceAddress.ToString = deviceId)
        '        If device IsNot Nothing Then
        '            Debug.WriteLine($"Checking to see if {device.DeviceName} {device.DeviceAddress} is in range...")
        '            deviceIsInRange = Await TestDeviceInRangeAsync(device)
        '            Debug.WriteLine($"  Device in range: {deviceIsInRange.ToString}")
        '        End If

        '        If deviceIsInRange Then Exit For
        '    Next

        'Catch ex As Exception
        'End Try

        timerTestInRange.Start()
    End Sub
End Class
