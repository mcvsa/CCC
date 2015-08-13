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
        Me.BtTestMail = New System.Windows.Forms.Button()
        Me.BtConfigMail = New System.Windows.Forms.Button()
        Me.BtTestSMS = New System.Windows.Forms.Button()
        Me.SerialPortTest = New System.IO.Ports.SerialPort(Me.components)
        Me.CBoxSerialPort = New System.Windows.Forms.ComboBox()
        Me.GBoxUsers = New System.Windows.Forms.GroupBox()
        Me.BtAssignDevices = New System.Windows.Forms.Button()
        Me.BtRemoveUser = New System.Windows.Forms.Button()
        Me.BtModifyUser = New System.Windows.Forms.Button()
        Me.LBUsers = New System.Windows.Forms.ListBox()
        Me.BtAddUser = New System.Windows.Forms.Button()
        Me.CkLBCaptadors = New System.Windows.Forms.CheckedListBox()
        Me.BtSelectAll = New System.Windows.Forms.Button()
        Me.BtDeselectAll = New System.Windows.Forms.Button()
        Me.GBoxCaptadors = New System.Windows.Forms.GroupBox()
        Me.GBoxConfigAndTest = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BtClose = New System.Windows.Forms.Button()
        Me.GBoxUsers.SuspendLayout()
        Me.GBoxCaptadors.SuspendLayout()
        Me.GBoxConfigAndTest.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtTestMail
        '
        Me.BtTestMail.Location = New System.Drawing.Point(11, 70)
        Me.BtTestMail.Name = "BtTestMail"
        Me.BtTestMail.Size = New System.Drawing.Size(100, 30)
        Me.BtTestMail.TabIndex = 14
        Me.BtTestMail.Text = "Test mail"
        Me.BtTestMail.UseVisualStyleBackColor = True
        '
        'BtConfigMail
        '
        Me.BtConfigMail.Location = New System.Drawing.Point(11, 28)
        Me.BtConfigMail.Name = "BtConfigMail"
        Me.BtConfigMail.Size = New System.Drawing.Size(100, 30)
        Me.BtConfigMail.TabIndex = 17
        Me.BtConfigMail.Text = "Config. servidor"
        Me.BtConfigMail.UseVisualStyleBackColor = True
        '
        'BtTestSMS
        '
        Me.BtTestSMS.Location = New System.Drawing.Point(11, 62)
        Me.BtTestSMS.Name = "BtTestSMS"
        Me.BtTestSMS.Size = New System.Drawing.Size(100, 30)
        Me.BtTestSMS.TabIndex = 15
        Me.BtTestSMS.Text = "Test SMS"
        Me.BtTestSMS.UseVisualStyleBackColor = True
        '
        'CBoxSerialPort
        '
        Me.CBoxSerialPort.FormattingEnabled = True
        Me.CBoxSerialPort.Location = New System.Drawing.Point(11, 24)
        Me.CBoxSerialPort.Name = "CBoxSerialPort"
        Me.CBoxSerialPort.Size = New System.Drawing.Size(100, 21)
        Me.CBoxSerialPort.TabIndex = 24
        '
        'GBoxUsers
        '
        Me.GBoxUsers.Controls.Add(Me.BtAssignDevices)
        Me.GBoxUsers.Controls.Add(Me.BtRemoveUser)
        Me.GBoxUsers.Controls.Add(Me.BtModifyUser)
        Me.GBoxUsers.Controls.Add(Me.LBUsers)
        Me.GBoxUsers.Controls.Add(Me.BtAddUser)
        Me.GBoxUsers.Location = New System.Drawing.Point(238, 13)
        Me.GBoxUsers.Name = "GBoxUsers"
        Me.GBoxUsers.Size = New System.Drawing.Size(324, 286)
        Me.GBoxUsers.TabIndex = 25
        Me.GBoxUsers.TabStop = False
        Me.GBoxUsers.Text = "Usuaris"
        '
        'BtAssignDevices
        '
        Me.BtAssignDevices.BackColor = System.Drawing.SystemColors.ControlDark
        Me.BtAssignDevices.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.BtAssignDevices.Location = New System.Drawing.Point(218, 24)
        Me.BtAssignDevices.Name = "BtAssignDevices"
        Me.BtAssignDevices.Size = New System.Drawing.Size(100, 100)
        Me.BtAssignDevices.TabIndex = 32
        Me.BtAssignDevices.Text = "Assignar captadors a usuari"
        Me.BtAssignDevices.UseVisualStyleBackColor = False
        '
        'BtRemoveUser
        '
        Me.BtRemoveUser.Location = New System.Drawing.Point(218, 245)
        Me.BtRemoveUser.Name = "BtRemoveUser"
        Me.BtRemoveUser.Size = New System.Drawing.Size(100, 30)
        Me.BtRemoveUser.TabIndex = 30
        Me.BtRemoveUser.Text = "Eliminar usuari"
        Me.BtRemoveUser.UseVisualStyleBackColor = True
        '
        'BtModifyUser
        '
        Me.BtModifyUser.Location = New System.Drawing.Point(218, 211)
        Me.BtModifyUser.Name = "BtModifyUser"
        Me.BtModifyUser.Size = New System.Drawing.Size(100, 30)
        Me.BtModifyUser.TabIndex = 31
        Me.BtModifyUser.Text = "Modificar usuari"
        Me.BtModifyUser.UseVisualStyleBackColor = True
        '
        'LBUsers
        '
        Me.LBUsers.FormattingEnabled = True
        Me.LBUsers.Location = New System.Drawing.Point(10, 24)
        Me.LBUsers.Name = "LBUsers"
        Me.LBUsers.Size = New System.Drawing.Size(199, 251)
        Me.LBUsers.TabIndex = 0
        '
        'BtAddUser
        '
        Me.BtAddUser.Location = New System.Drawing.Point(218, 175)
        Me.BtAddUser.Name = "BtAddUser"
        Me.BtAddUser.Size = New System.Drawing.Size(100, 30)
        Me.BtAddUser.TabIndex = 29
        Me.BtAddUser.Text = "Afegir usuari"
        Me.BtAddUser.UseVisualStyleBackColor = True
        '
        'CkLBCaptadors
        '
        Me.CkLBCaptadors.CheckOnClick = True
        Me.CkLBCaptadors.ColumnWidth = 132
        Me.CkLBCaptadors.FormattingEnabled = True
        Me.CkLBCaptadors.Location = New System.Drawing.Point(9, 25)
        Me.CkLBCaptadors.Name = "CkLBCaptadors"
        Me.CkLBCaptadors.Size = New System.Drawing.Size(206, 214)
        Me.CkLBCaptadors.TabIndex = 23
        '
        'BtSelectAll
        '
        Me.BtSelectAll.Location = New System.Drawing.Point(12, 245)
        Me.BtSelectAll.Name = "BtSelectAll"
        Me.BtSelectAll.Size = New System.Drawing.Size(100, 30)
        Me.BtSelectAll.TabIndex = 25
        Me.BtSelectAll.Text = "Seleccionar tots"
        Me.BtSelectAll.UseVisualStyleBackColor = True
        '
        'BtDeselectAll
        '
        Me.BtDeselectAll.Location = New System.Drawing.Point(118, 245)
        Me.BtDeselectAll.Name = "BtDeselectAll"
        Me.BtDeselectAll.Size = New System.Drawing.Size(100, 30)
        Me.BtDeselectAll.TabIndex = 27
        Me.BtDeselectAll.Text = "Deseleccionar"
        Me.BtDeselectAll.UseVisualStyleBackColor = True
        '
        'GBoxCaptadors
        '
        Me.GBoxCaptadors.Controls.Add(Me.CkLBCaptadors)
        Me.GBoxCaptadors.Controls.Add(Me.BtSelectAll)
        Me.GBoxCaptadors.Controls.Add(Me.BtDeselectAll)
        Me.GBoxCaptadors.Location = New System.Drawing.Point(8, 13)
        Me.GBoxCaptadors.Name = "GBoxCaptadors"
        Me.GBoxCaptadors.Size = New System.Drawing.Size(224, 286)
        Me.GBoxCaptadors.TabIndex = 28
        Me.GBoxCaptadors.TabStop = False
        Me.GBoxCaptadors.Text = "Captadors"
        '
        'GBoxConfigAndTest
        '
        Me.GBoxConfigAndTest.Controls.Add(Me.BtTestMail)
        Me.GBoxConfigAndTest.Controls.Add(Me.BtConfigMail)
        Me.GBoxConfigAndTest.Location = New System.Drawing.Point(588, 13)
        Me.GBoxConfigAndTest.Name = "GBoxConfigAndTest"
        Me.GBoxConfigAndTest.Size = New System.Drawing.Size(121, 111)
        Me.GBoxConfigAndTest.TabIndex = 29
        Me.GBoxConfigAndTest.TabStop = False
        Me.GBoxConfigAndTest.Text = "Config. i test mail"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BtTestSMS)
        Me.GroupBox1.Controls.Add(Me.CBoxSerialPort)
        Me.GroupBox1.Location = New System.Drawing.Point(588, 142)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(121, 109)
        Me.GroupBox1.TabIndex = 30
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Test SMS"
        '
        'BtClose
        '
        Me.BtClose.Location = New System.Drawing.Point(579, 269)
        Me.BtClose.Name = "BtClose"
        Me.BtClose.Size = New System.Drawing.Size(130, 30)
        Me.BtClose.TabIndex = 31
        Me.BtClose.Text = "Tancar finestra"
        Me.BtClose.UseVisualStyleBackColor = True
        '
        'ConfigAlerts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(719, 313)
        Me.Controls.Add(Me.BtClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GBoxConfigAndTest)
        Me.Controls.Add(Me.GBoxCaptadors)
        Me.Controls.Add(Me.GBoxUsers)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(735, 351)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(735, 351)
        Me.Name = "ConfigAlerts"
        Me.Text = "Configuració Alertes"
        Me.GBoxUsers.ResumeLayout(False)
        Me.GBoxCaptadors.ResumeLayout(False)
        Me.GBoxConfigAndTest.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtTestMail As System.Windows.Forms.Button
    Friend WithEvents BtConfigMail As System.Windows.Forms.Button
    Friend WithEvents BtTestSMS As System.Windows.Forms.Button
    Friend WithEvents SerialPortTest As System.IO.Ports.SerialPort
    Friend WithEvents CBoxSerialPort As System.Windows.Forms.ComboBox
    Friend WithEvents GBoxUsers As System.Windows.Forms.GroupBox
    Friend WithEvents CkLBCaptadors As System.Windows.Forms.CheckedListBox
    Friend WithEvents BtSelectAll As System.Windows.Forms.Button
    Friend WithEvents BtDeselectAll As System.Windows.Forms.Button
    Friend WithEvents GBoxCaptadors As System.Windows.Forms.GroupBox
    Friend WithEvents BtAddUser As System.Windows.Forms.Button
    Friend WithEvents BtRemoveUser As System.Windows.Forms.Button
    Friend WithEvents BtModifyUser As System.Windows.Forms.Button
    Friend WithEvents LBUsers As System.Windows.Forms.ListBox
    Friend WithEvents BtAssignDevices As System.Windows.Forms.Button
    Friend WithEvents GBoxConfigAndTest As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtClose As System.Windows.Forms.Button
End Class
