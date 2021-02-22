


Module loader


    Sub main()
        log($"{Application.ProductName} v{Application.ProductVersion}")

        Dim runLogin As Boolean = False
        For Each arg As String In Environment.GetCommandLineArgs
            If arg = "login" Then runLogin = True
        Next

        Try
            If Process.GetProcessesByName("ProxLock").Count > 2 Then Throw New Exception("Too many instances running!")

            Dim identity = System.Security.Principal.WindowsIdentity.GetCurrent()
            log($"Admin={IsUserAnAdmin.ToString} User={Environment.UserName} SYSTEM={identity.IsSystem.ToString} RunLogin={runLogin.ToString}")

            Application.EnableVisualStyles()

            If runLogin Then
                If identity.IsSystem Then
                    log("Showing Login Form")
                    Application.Run(frmLogin)
                Else
                    log("Calling Runner")
                    runner.Run()
                End If
            Else
                log("Showing Settings form")
                Application.Run(frmSettings)
            End If
        Catch ex As Exception
            log(ex.ToString)
        End Try

        log($"Stopping...")
    End Sub

End Module
