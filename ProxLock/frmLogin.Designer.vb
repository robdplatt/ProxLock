<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.timerKeepOnTop = New System.Windows.Forms.Timer(Me.components)
        Me.timerFindProcess = New System.Windows.Forms.Timer(Me.components)
        Me.timerRefreshBTDevices = New System.Windows.Forms.Timer(Me.components)
        Me.timerTestInRange = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'btnLogin
        '
        Me.btnLogin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLogin.Enabled = False
        Me.btnLogin.Location = New System.Drawing.Point(0, 0)
        Me.btnLogin.Margin = New System.Windows.Forms.Padding(1)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(181, 48)
        Me.btnLogin.TabIndex = 0
        Me.btnLogin.Text = "Auto Sign In"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'timerKeepOnTop
        '
        Me.timerKeepOnTop.Enabled = True
        '
        'timerFindProcess
        '
        Me.timerFindProcess.Enabled = True
        Me.timerFindProcess.Interval = 1000
        '
        'timerRefreshBTDevices
        '
        Me.timerRefreshBTDevices.Enabled = True
        Me.timerRefreshBTDevices.Interval = 1000
        '
        'timerTestInRange
        '
        Me.timerTestInRange.Enabled = True
        Me.timerTestInRange.Interval = 1000
        '
        'frmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(181, 48)
        Me.Controls.Add(Me.btnLogin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(1)
        Me.Name = "frmLogin"
        Me.Text = "Login"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnLogin As Button
    Friend WithEvents timerKeepOnTop As Timer
    Friend WithEvents timerFindProcess As Timer
    Friend WithEvents timerRefreshBTDevices As Timer
    Friend WithEvents timerTestInRange As Timer
End Class
