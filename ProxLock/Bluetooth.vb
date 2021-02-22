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

    Friend Async Function GetBTDevicesInRangeAsync() As Task(Of List(Of BluetoothDeviceInfo))
        Dim list As New List(Of BluetoothDeviceInfo)
        Await Task.Run(Sub()
                           Using client As New BluetoothClient()
                               For Each device As BluetoothDeviceInfo In client.DiscoverDevicesInRange
                                   list.Add(device)
                               Next
                           End Using
                       End Sub)
        Return list
    End Function

End Module
