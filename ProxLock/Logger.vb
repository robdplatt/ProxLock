Module Logger
    Friend Sub log(logItem As String)
#If DEBUG Then
        logItem = $"{Now.ToString("yyyy-MM-dd hh:mm:ss")} {Process.GetCurrentProcess.Id.ToString} {logItem}"
        'My.Computer.FileSystem.WriteAllText("c:\windows\temp\ProxLock.log", logItem & Environment.NewLine, True)
#End If
    End Sub
End Module
