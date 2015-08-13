<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormConfigMail
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
        Me.TBoxSMTPServer = New System.Windows.Forms.TextBox()
        Me.TBoxLogin = New System.Windows.Forms.TextBox()
        Me.TBoxPassword = New System.Windows.Forms.TextBox()
        Me.BtConfigMailNow = New System.Windows.Forms.Button()
        Me.LbSMTPMail = New System.Windows.Forms.Label()
        Me.LbMailAddress = New System.Windows.Forms.Label()
        Me.LbPassword = New System.Windows.Forms.Label()
        Me.TBoxPort = New System.Windows.Forms.TextBox()
        Me.LbPort = New System.Windows.Forms.Label()
        Me.CBoxSSL = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'TBoxSMTPServer
        '
        Me.TBoxSMTPServer.Location = New System.Drawing.Point(88, 12)
        Me.TBoxSMTPServer.Name = "TBoxSMTPServer"
        Me.TBoxSMTPServer.Size = New System.Drawing.Size(224, 20)
        Me.TBoxSMTPServer.TabIndex = 0
        '
        'TBoxLogin
        '
        Me.TBoxLogin.Location = New System.Drawing.Point(88, 87)
        Me.TBoxLogin.Name = "TBoxLogin"
        Me.TBoxLogin.Size = New System.Drawing.Size(224, 20)
        Me.TBoxLogin.TabIndex = 3
        '
        'TBoxPassword
        '
        Me.TBoxPassword.Location = New System.Drawing.Point(88, 113)
        Me.TBoxPassword.Name = "TBoxPassword"
        Me.TBoxPassword.Size = New System.Drawing.Size(224, 20)
        Me.TBoxPassword.TabIndex = 4
        '
        'BtConfigMailNow
        '
        Me.BtConfigMailNow.Location = New System.Drawing.Point(180, 143)
        Me.BtConfigMailNow.Name = "BtConfigMailNow"
        Me.BtConfigMailNow.Size = New System.Drawing.Size(130, 30)
        Me.BtConfigMailNow.TabIndex = 5
        Me.BtConfigMailNow.Text = "Acceptar"
        Me.BtConfigMailNow.UseVisualStyleBackColor = True
        '
        'LbSMTPMail
        '
        Me.LbSMTPMail.AutoSize = True
        Me.LbSMTPMail.Location = New System.Drawing.Point(3, 15)
        Me.LbSMTPMail.Name = "LbSMTPMail"
        Me.LbSMTPMail.Size = New System.Drawing.Size(79, 13)
        Me.LbSMTPMail.TabIndex = 21
        Me.LbSMTPMail.Text = "Servidor SMTP"
        '
        'LbMailAddress
        '
        Me.LbMailAddress.AutoSize = True
        Me.LbMailAddress.Location = New System.Drawing.Point(49, 90)
        Me.LbMailAddress.Name = "LbMailAddress"
        Me.LbMailAddress.Size = New System.Drawing.Size(33, 13)
        Me.LbMailAddress.TabIndex = 22
        Me.LbMailAddress.Text = "Login"
        '
        'LbPassword
        '
        Me.LbPassword.AutoSize = True
        Me.LbPassword.Location = New System.Drawing.Point(29, 116)
        Me.LbPassword.Name = "LbPassword"
        Me.LbPassword.Size = New System.Drawing.Size(53, 13)
        Me.LbPassword.TabIndex = 23
        Me.LbPassword.Text = "Password"
        '
        'TBoxPort
        '
        Me.TBoxPort.Location = New System.Drawing.Point(88, 61)
        Me.TBoxPort.Name = "TBoxPort"
        Me.TBoxPort.Size = New System.Drawing.Size(224, 20)
        Me.TBoxPort.TabIndex = 2
        '
        'LbPort
        '
        Me.LbPort.AutoSize = True
        Me.LbPort.Location = New System.Drawing.Point(56, 64)
        Me.LbPort.Name = "LbPort"
        Me.LbPort.Size = New System.Drawing.Size(26, 13)
        Me.LbPort.TabIndex = 25
        Me.LbPort.Text = "Port"
        '
        'CBoxSSL
        '
        Me.CBoxSSL.AutoSize = True
        Me.CBoxSSL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CBoxSSL.Location = New System.Drawing.Point(21, 38)
        Me.CBoxSSL.Name = "CBoxSSL"
        Me.CBoxSSL.Size = New System.Drawing.Size(81, 17)
        Me.CBoxSSL.TabIndex = 1
        Me.CBoxSSL.Text = "SSL activat"
        Me.CBoxSSL.UseVisualStyleBackColor = True
        '
        'FormConfigMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(322, 185)
        Me.Controls.Add(Me.CBoxSSL)
        Me.Controls.Add(Me.LbPort)
        Me.Controls.Add(Me.TBoxPort)
        Me.Controls.Add(Me.LbPassword)
        Me.Controls.Add(Me.LbMailAddress)
        Me.Controls.Add(Me.LbSMTPMail)
        Me.Controls.Add(Me.BtConfigMailNow)
        Me.Controls.Add(Me.TBoxPassword)
        Me.Controls.Add(Me.TBoxLogin)
        Me.Controls.Add(Me.TBoxSMTPServer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(338, 223)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(338, 223)
        Me.Name = "FormConfigMail"
        Me.Text = "Configuració mail"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TBoxSMTPServer As System.Windows.Forms.TextBox
    Friend WithEvents TBoxLogin As System.Windows.Forms.TextBox
    Friend WithEvents TBoxPassword As System.Windows.Forms.TextBox
    Friend WithEvents BtConfigMailNow As System.Windows.Forms.Button
    Friend WithEvents LbSMTPMail As System.Windows.Forms.Label
    Friend WithEvents LbMailAddress As System.Windows.Forms.Label
    Friend WithEvents LbPassword As System.Windows.Forms.Label
    Friend WithEvents TBoxPort As System.Windows.Forms.TextBox
    Friend WithEvents LbPort As System.Windows.Forms.Label
    Friend WithEvents CBoxSSL As System.Windows.Forms.CheckBox
End Class
