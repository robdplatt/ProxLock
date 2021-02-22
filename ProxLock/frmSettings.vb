Public Class frmSettings
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub



    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = $"{Text} v{Application.ProductVersion}"
    End Sub

    Private Sub btnInstall_1(sender As Object, e As EventArgs) Handles btnInstall.Click
        Installer.Install()
    End Sub

    Private Sub btnUninstall_Click(sender As Object, e As EventArgs) Handles btnUninstall.Click
        Installer.Uninstall()
    End Sub
End Class