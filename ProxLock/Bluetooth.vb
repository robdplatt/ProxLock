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

    Friend Async Function TestDeviceInRangeAsync(device As BluetoothDeviceInfo) As Task(Of Boolean)
        Dim deviceIsInRange As Boolean = False
        Await Task.Run(Sub()
                           Try
                               Dim records As ServiceRecord() = device.GetServiceRecords(New Guid("bec9d149-e711-4899-8fae-bfe6d25db0d6"))
                               deviceIsInRange = True
                           Catch ex As Net.Sockets.SocketException
                           End Try
                       End Sub)
        Return deviceIsInRange
    End Function

End Module
