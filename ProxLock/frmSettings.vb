Imports InTheHand.Net.Sockets

Public Class frmSettings
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = $"{Text} v{Application.ProductVersion}"

        GetBTDevices()

    End Sub

    Private Sub btnInstall_Click(sender As Object, e As EventArgs) Handles btnInstall.Click
        Installer.Install()
    End Sub

    Private Sub btnUninstall_Click(sender As Object, e As EventArgs) Handles btnUninstall.Click
        Installer.Uninstall()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        GetBTDevices()
    End Sub

    Private Async Sub GetBTDevices()
        btnRefresh.Enabled = False
        btnRefresh.Text = "Scanning..."

        lvAvailable.Items.Clear()

        Dim list As List(Of BluetoothDeviceInfo) = Await GetBTDevicesAsync()
        For Each device In list
            Dim item As New ListViewItem
            item.Tag = device
            item.Text = device.DeviceName
            item.SubItems.Add(device.DeviceAddress.ToString)
            item.SubItems.Add(device.Rssi.ToString)
            lvAvailable.Items.Add(item)
        Next

        lvAvailable.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)

        btnRefresh.Text = "Refresh"
        btnRefresh.Enabled = True
    End Sub

    Private Sub mnuAvailable_Click(sender As Object, e As EventArgs) Handles mnuAvailable.Click
        If lvAvailable.SelectedItems.Count = 0 Then Exit Sub
        Dim item As ListViewItem = lvAvailable.SelectedItems(0)
        Dim newItem As ListViewItem = CType(item.Clone, ListViewItem)
        lvRegistered.Items.Add(newItem)

        lvRegistered.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub
    Private Sub mnuRegistered_Click(sender As Object, e As EventArgs) Handles mnuRegistered.Click
        If lvRegistered.SelectedItems.Count = 0 Then Exit Sub
        lvRegistered.SelectedItems(0).Remove()
    End Sub

End Class