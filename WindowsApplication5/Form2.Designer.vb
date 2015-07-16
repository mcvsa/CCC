<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigAlerts
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
        Me.CBoxSMS = New System.Windows.Forms.CheckBox()
        Me.CBoxMail = New System.Windows.Forms.CheckBox()
        Me.BtTestMail = New System.Windows.Forms.Button()
        Me.BtAddMail = New System.Windows.Forms.Button()
        Me.BtConfigMail = New System.Windows.Forms.Button()
        Me.BtNewPhone = New System.Windows.Forms.Button()
        Me.BtTestSMS = New System.Windows.Forms.Button()
        Me.BtRemovePhone = New System.Windows.Forms.Button()
        Me.BtRemoveMail = New System.Windows.Forms.Button()
        Me.LBoxMails = New System.Windows.Forms.ListBox()
        Me.LBoxSMS = New System.Windows.Forms.ListBox()
        Me.SerialPortTest = New System.IO.Ports.SerialPort(Me.components)
        Me.CBoxSerialPort = New System.Windows.Forms.ComboBox()
        Me.GBoxSMSAlarms = New System.Windows.Forms.GroupBox()
        Me.GBoxMailAlarms = New System.Windows.Forms.GroupBox()
        Me.GBoxSMSAlarms.SuspendLayout()
        Me.GBoxMailAlarms.SuspendLayout()
        Me.SuspendLayout()
        '
        'CBoxSMS
        '
        Me.CBoxSMS.AutoSize = True
        Me.CBoxSMS.Location = New System.Drawing.Point(13, 28)
        Me.CBoxSMS.Name = "CBoxSMS"
        Me.CBoxSMS.Size = New System.Drawing.Size(119, 17)
        Me.CBoxSMS.TabIndex = 0
        Me.CBoxSMS.Text = "Activar alertes SMS"
        Me.CBoxSMS.UseVisualStyleBackColor = True
        '
        'CBoxMail
        '
        Me.CBoxMail.AutoSize = True
        Me.CBoxMail.Location = New System.Drawing.Point(13, 28)
        Me.CBoxMail.Name = "CBoxMail"
        Me.CBoxMail.Size = New System.Drawing.Size(115, 17)
        Me.CBoxMail.TabIndex = 1
        Me.CBoxMail.Text = "Activar alertes Mail"
        Me.CBoxMail.UseVisualStyleBackColor = True
        '
        'BtTestMail
        '
        Me.BtTestMail.Location = New System.Drawing.Point(13, 51)
        Me.BtTestMail.Name = "BtTestMail"
        Me.BtTestMail.Size = New System.Drawing.Size(130, 30)
        Me.BtTestMail.TabIndex = 14
        Me.BtTestMail.Text = "Test enviament correu"
        Me.BtTestMail.UseVisualStyleBackColor = True
        '
        'BtAddMail
        '
        Me.BtAddMail.Location = New System.Drawing.Point(13, 103)
        Me.BtAddMail.Name = "BtAddMail"
        Me.BtAddMail.Size = New System.Drawing.Size(130, 30)
        Me.BtAddMail.TabIndex = 16
        Me.BtAddMail.Text = "Afegir Mail"
        Me.BtAddMail.UseVisualStyleBackColor = True
        '
        'BtConfigMail
        '
        Me.BtConfigMail.Location = New System.Drawing.Point(149, 51)
        Me.BtConfigMail.Name = "BtConfigMail"
        Me.BtConfigMail.Size = New System.Drawing.Size(130, 30)
        Me.BtConfigMail.TabIndex = 17
        Me.BtConfigMail.Text = "Config. servidor correu"
        Me.BtConfigMail.UseVisualStyleBackColor = True
        '
        'BtNewPhone
        '
        Me.BtNewPhone.Location = New System.Drawing.Point(13, 103)
        Me.BtNewPhone.Name = "BtNewPhone"
        Me.BtNewPhone.Size = New System.Drawing.Size(130, 30)
        Me.BtNewPhone.TabIndex = 18
        Me.BtNewPhone.Text = "Afegir Telèfon"
        Me.BtNewPhone.UseVisualStyleBackColor = True
        '
        'BtTestSMS
        '
        Me.BtTestSMS.Location = New System.Drawing.Point(13, 51)
        Me.BtTestSMS.Name = "BtTestSMS"
        Me.BtTestSMS.Size = New System.Drawing.Size(130, 30)
        Me.BtTestSMS.TabIndex = 15
        Me.BtTestSMS.Text = "Test enviament SMS"
        Me.BtTestSMS.UseVisualStyleBackColor = True
        '
        'BtRemovePhone
        '
        Me.BtRemovePhone.Location = New System.Drawing.Point(149, 103)
        Me.BtRemovePhone.Name = "BtRemovePhone"
        Me.BtRemovePhone.Size = New System.Drawing.Size(130, 30)
        Me.BtRemovePhone.TabIndex = 19
        Me.BtRemovePhone.Text = "Esborrar Telèfon"
        Me.BtRemovePhone.UseVisualStyleBackColor = True
        '
        'BtRemoveMail
        '
        Me.BtRemoveMail.Location = New System.Drawing.Point(149, 103)
        Me.BtRemoveMail.Name = "BtRemoveMail"
        Me.BtRemoveMail.Size = New System.Drawing.Size(130, 30)
        Me.BtRemoveMail.TabIndex = 20
        Me.BtRemoveMail.Text = "Esborrar Mail"
        Me.BtRemoveMail.UseVisualStyleBackColor = True
        '
        'LBoxMails
        '
        Me.LBoxMails.FormattingEnabled = True
        Me.LBoxMails.Location = New System.Drawing.Point(13, 142)
        Me.LBoxMails.Name = "LBoxMails"
        Me.LBoxMails.Size = New System.Drawing.Size(266, 134)
        Me.LBoxMails.TabIndex = 22
        '
        'LBoxSMS
        '
        Me.LBoxSMS.FormattingEnabled = True
        Me.LBoxSMS.Location = New System.Drawing.Point(13, 142)
        Me.LBoxSMS.Name = "LBoxSMS"
        Me.LBoxSMS.Size = New System.Drawing.Size(266, 134)
        Me.LBoxSMS.TabIndex = 23
        '
        'CBoxSerialPort
        '
        Me.CBoxSerialPort.FormattingEnabled = True
        Me.CBoxSerialPort.Location = New System.Drawing.Point(149, 57)
        Me.CBoxSerialPort.Name = "CBoxSerialPort"
        Me.CBoxSerialPort.Size = New System.Drawing.Size(130, 21)
        Me.CBoxSerialPort.TabIndex = 24
        '
        'GBoxSMSAlarms
        '
        Me.GBoxSMSAlarms.Controls.Add(Me.CBoxSerialPort)
        Me.GBoxSMSAlarms.Controls.Add(Me.LBoxSMS)
        Me.GBoxSMSAlarms.Controls.Add(Me.BtRemovePhone)
        Me.GBoxSMSAlarms.Controls.Add(Me.BtNewPhone)
        Me.GBoxSMSAlarms.Controls.Add(Me.BtTestSMS)
        Me.GBoxSMSAlarms.Controls.Add(Me.CBoxSMS)
        Me.GBoxSMSAlarms.Location = New System.Drawing.Point(4, 4)
        Me.GBoxSMSAlarms.Name = "GBoxSMSAlarms"
        Me.GBoxSMSAlarms.Size = New System.Drawing.Size(292, 289)
        Me.GBoxSMSAlarms.TabIndex = 25
        Me.GBoxSMSAlarms.TabStop = False
        Me.GBoxSMSAlarms.Text = "Alarmes SMS"
        '
        'GBoxMailAlarms
        '
        Me.GBoxMailAlarms.Controls.Add(Me.LBoxMails)
        Me.GBoxMailAlarms.Controls.Add(Me.BtRemoveMail)
        Me.GBoxMailAlarms.Controls.Add(Me.BtConfigMail)
        Me.GBoxMailAlarms.Controls.Add(Me.BtAddMail)
        Me.GBoxMailAlarms.Controls.Add(Me.BtTestMail)
        Me.GBoxMailAlarms.Controls.Add(Me.CBoxMail)
        Me.GBoxMailAlarms.Location = New System.Drawing.Point(306, 4)
        Me.GBoxMailAlarms.Name = "GBoxMailAlarms"
        Me.GBoxMailAlarms.Size = New System.Drawing.Size(292, 289)
        Me.GBoxMailAlarms.TabIndex = 26
        Me.GBoxMailAlarms.TabStop = False
        Me.GBoxMailAlarms.Text = "Alarmes Mail"
        '
        'ConfigAlerts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 295)
        Me.Controls.Add(Me.GBoxMailAlarms)
        Me.Controls.Add(Me.GBoxSMSAlarms)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(620, 333)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(620, 333)
        Me.Name = "ConfigAlerts"
        Me.Text = "Configuració Alertes"
        Me.GBoxSMSAlarms.ResumeLayout(False)
        Me.GBoxSMSAlarms.PerformLayout()
        Me.GBoxMailAlarms.ResumeLayout(False)
        Me.GBoxMailAlarms.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CBoxSMS As System.Windows.Forms.CheckBox
    Friend WithEvents CBoxMail As System.Windows.Forms.CheckBox
    Friend WithEvents BtTestMail As System.Windows.Forms.Button
    Friend WithEvents BtAddMail As System.Windows.Forms.Button
    Friend WithEvents BtConfigMail As System.Windows.Forms.Button
    Friend WithEvents BtNewPhone As System.Windows.Forms.Button
    Friend WithEvents BtTestSMS As System.Windows.Forms.Button
    Friend WithEvents BtRemovePhone As System.Windows.Forms.Button
    Friend WithEvents BtRemoveMail As System.Windows.Forms.Button
    Friend WithEvents LBoxMails As System.Windows.Forms.ListBox
    Friend WithEvents LBoxSMS As System.Windows.Forms.ListBox
    Friend WithEvents SerialPortTest As System.IO.Ports.SerialPort
    Friend WithEvents CBoxSerialPort As System.Windows.Forms.ComboBox
    Friend WithEvents GBoxSMSAlarms As System.Windows.Forms.GroupBox
    Friend WithEvents GBoxMailAlarms As System.Windows.Forms.GroupBox
End Class
