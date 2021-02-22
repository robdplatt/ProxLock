Imports System.Runtime.InteropServices
Module NativeMethods
    <DllImport("shell32.dll", EntryPoint:="IsUserAnAdmin")>
    Friend Function IsUserAnAdmin() As Boolean
    End Function

    <DllImport("User32.dll")>
    Friend Function SetForegroundWindow(ByVal point As IntPtr) As Integer
    End Function
End Module
