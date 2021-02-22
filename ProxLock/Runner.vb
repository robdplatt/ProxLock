
Public Module Runner

    Friend Sub Run()
        Dim psexec As String = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "psexec.exe")
        If Not My.Computer.FileSystem.FileExists(psexec) Then My.Computer.FileSystem.WriteAllBytes(psexec, My.Resources.PsExec, False)
        Using p As New Process
            p.StartInfo.UseShellExecute = True
            p.StartInfo.CreateNoWindow = True
            p.StartInfo.FileName = psexec
            p.StartInfo.Arguments = $"-s -x -d {Application.ExecutablePath} login"
            log($"Starting: {p.StartInfo.FileName} {p.StartInfo.Arguments}")
            If Not IsUserAnAdmin() Then
                p.StartInfo.Verb = "runas"
                log(".. as Admin")
            End If

            p.Start()
        End Using
    End Sub

End Module
