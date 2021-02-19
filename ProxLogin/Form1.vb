Imports System.Runtime.InteropServices

Public Class Form1

    <DllImport("user32", EntryPoint:="SendMessageA", CharSet:=CharSet.Ansi, SetLastError:=True, ExactSpelling:=True)>
    Public Shared Function SendMessageString(ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, <MarshalAs(UnmanagedType.VBByRefStr)> ByRef lParam As String) As Integer
    End Function
    Private Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As IntPtr
    Private Const WM_SETTEXT As Integer = &HC
    Private Const WM_CLICK As Integer = &HF5
    <DllImport("User32.Dll")>
    Public Shared Function PostMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Int32
    End Function

    <DllImport("user32.dll", EntryPoint:="FindWindowEx")>
    Public Shared Function FindWindowEx(ByVal hwndParent As IntPtr, ByVal hwndChildAfter As IntPtr, ByVal lpszClass As String, ByVal lpszWindow As String) As IntPtr
    End Function

    <DllImport("User32.dll")>
    Private Shared Function SetForegroundWindow(ByVal point As IntPtr) As Integer

    End Function

    'Private Shared ReadOnly HWND_TOPMOST As IntPtr = New IntPtr(-1)
    'Private Const SWP_NOSIZE As UInt32 = &H1
    'Private Const SWP_NOMOVE As UInt32 = &H2
    'Private Const TOPMOST_FLAGS As UInt32 = SWP_NOMOVE Or SWP_NOSIZE


    '<DllImport("user32.dll")>
    'Public Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInteger) As Boolean
    'End Function

    Private process As Process
    'Private childWindows As List(Of IntPtr)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'SetWindowPos(Me.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS)


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles timerKeepOnTop.Tick
        TopMost = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Clear()

        'For Each p As Process In Process.GetProcesses
        '    TextBox1.AppendText($"{p.MainWindowHandle} {p.MainWindowTitle} {Environment.NewLine}")
        'Next

        'Windows sign-in
        'Untitled - Notepad


        TextBox1.Text = $"{process.MainWindowHandle} {process.MainWindowTitle} {Environment.NewLine}"

        'childWindows = New WindowHandleInfo(process.MainWindowHandle).GetAllChildHandles
        'For Each window In childWindows
        '    TextBox1.AppendText($"{window.ToInt32} {Environment.NewLine}")
        'Next

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If process Is Nothing Then Exit Sub

        timerKeepOnTop.Stop()
        'SendMessageString(process.MainWindowHandle, WM_SETTEXT, 0, "1146")
        'SendMessage(process.MainWindowHandle, &H9, 0, Nothing)

        SetForegroundWindow(process.MainWindowHandle)
        'Threading.Thread.Sleep(500)
        SendKeys.Send("1146")
        SendKeys.Send(Environment.NewLine)


        'SetForegroundWindow(process.MainWindowHandle)
        'Threading.Thread.Sleep(500)
        'SendKeys.Send("4")
        'SetForegroundWindow(process.MainWindowHandle)
        'Threading.Thread.Sleep(500)
        'SendKeys.Send("1")
        'SetForegroundWindow(process.MainWindowHandle)
        'Threading.Thread.Sleep(500)
        'SendKeys.Send("1")
        'My.Computer.Keyboard.SendKeys("1146")
        timerKeepOnTop.Start()
    End Sub

    Private Sub timerFindWindowHandle_Tick(sender As Object, e As EventArgs) Handles timerFindWindowHandle.Tick
        'Windows sign-in
        '*Untitled - Notepad
        process = Process.GetProcesses.FirstOrDefault(Function(x) x.MainWindowTitle = "Windows sign-in")
    End Sub
End Class
