Imports System.Net.Sockets
Imports System.Runtime.CompilerServices
Imports InTheHand.Net.Bluetooth
Imports InTheHand.Net.Sockets

Module Bluetooth

    Friend Async Function GetBTDevicesAsync() As Task(Of List(Of BluetoothDeviceInfo))
        Dim list As New List(Of BluetoothDeviceInfo)
        Await Task.Run(Sub()
                           Using client As New BluetoothClient()
                               For Each device As BluetoothDeviceInfo In client.DiscoverDevices()
                                   list.Add(device)
                               Next
                           End Using
                       End Sub)
        Return list
    End Function

    'Friend Async Function TestDeviceInRangeAsync(device As BluetoothDeviceInfo) As Task(Of Boolean)
    '    Dim deviceIsInRange As Boolean = False
    '    Await Task.Run(Sub()
    '                       Try
    '                           Dim records As ServiceRecord() = device.GetServiceRecords(New Guid("bec9d149-e711-4899-8fae-bfe6d25db0d6"))
    '                           deviceIsInRange = True
    '                       Catch ex As Net.Sockets.SocketException
    '                       End Try
    '                   End Sub)
    '    Return deviceIsInRange
    'End Function

    Friend Async Function ConnectAsync(device As BluetoothDeviceInfo) As Task(Of BluetoothClient)
        Debug.WriteLine($"Connecting to {device.DeviceName} {device.DeviceAddress} ...")
        Return Await Task.Run(Function()
                                  Try
                                      Dim client As New BluetoothClient
                                      client.BeginConnect(device.DeviceAddress, device.InstalledServices(0), AddressOf ConnectCallback, client)
                                      Debug.WriteLine("   Connected successfully")
                                      Return client
                                  Catch ex As Exception
                                      Debug.WriteLine("   Failed to connect")
                                  End Try
                                  Return Nothing
                              End Function)
    End Function

    Private Sub ConnectCallback(ar As IAsyncResult)
        Dim client As BluetoothClient = CType(ar.AsyncState, BluetoothClient)
        client.EndConnect(ar)
    End Sub

    <Extension()>
    Function IsConnected(ByVal socket As Socket) As Boolean
        Try
            Return Not (socket.Poll(1, SelectMode.SelectRead) AndAlso socket.Available = 0)
        Catch ex As SocketException
            Return False
        End Try
    End Function

End Module
