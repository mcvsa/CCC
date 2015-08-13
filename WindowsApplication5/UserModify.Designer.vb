<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserModify
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
        Me.BtCancel = New System.Windows.Forms.Button()
        Me.BtOK = New System.Windows.Forms.Button()
        Me.LbUserPhone = New System.Windows.Forms.Label()
        Me.LbUserMail = New System.Windows.Forms.Label()
        Me.LbUserName = New System.Windows.Forms.Label()
        Me.TBUserPhone = New System.Windows.Forms.TextBox()
        Me.TBUserMail = New System.Windows.Forms.TextBox()
        Me.TBUserName = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'BtCancel
        '
        Me.BtCancel.Location = New System.Drawing.Point(182, 109)
        Me.BtCancel.Name = "BtCancel"
        Me.BtCancel.Size = New System.Drawing.Size(130, 30)
        Me.BtCancel.TabIndex = 4
        Me.BtCancel.Text = "Cancelar"
        Me.BtCancel.UseVisualStyleBackColor = True
        '
        'BtOK
        '
        Me.BtOK.Location = New System.Drawing.Point(19, 109)
        Me.BtOK.Name = "BtOK"
        Me.BtOK.Size = New System.Drawing.Size(130, 30)
        Me.BtOK.TabIndex = 3
        Me.BtOK.Text = "Acceptar"
        Me.BtOK.UseVisualStyleBackColor = True
        '
        'LbUserPhone
        '
        Me.LbUserPhone.AutoSize = True
        Me.LbUserPhone.Location = New System.Drawing.Point(16, 45)
        Me.LbUserPhone.Name = "LbUserPhone"
        Me.LbUserPhone.Size = New System.Drawing.Size(98, 13)
        Me.LbUserPhone.TabIndex = 33
        Me.LbUserPhone.Text = "Telèfon (si s'escau)"
        '
        'LbUserMail
        '
        Me.LbUserMail.AutoSize = True
        Me.LbUserMail.Location = New System.Drawing.Point(16, 71)
        Me.LbUserMail.Name = "LbUserMail"
        Me.LbUserMail.Size = New System.Drawing.Size(81, 13)
        Me.LbUserMail.TabIndex = 32
        Me.LbUserMail.Text = "Mail (si s'escau)"
        '
        'LbUserName
        '
        Me.LbUserName.AutoSize = True
        Me.LbUserName.Location = New System.Drawing.Point(16, 19)
        Me.LbUserName.Name = "LbUserName"
        Me.LbUserName.Size = New System.Drawing.Size(79, 13)
        Me.LbUserName.TabIndex = 31
        Me.LbUserName.Text = "Nom de l'usuari"
        '
        'TBUserPhone
        '
        Me.TBUserPhone.Location = New System.Drawing.Point(120, 42)
        Me.TBUserPhone.Name = "TBUserPhone"
        Me.TBUserPhone.Size = New System.Drawing.Size(192, 20)
        Me.TBUserPhone.TabIndex = 1
        '
        'TBUserMail
        '
        Me.TBUserMail.Location = New System.Drawing.Point(120, 68)
        Me.TBUserMail.Name = "TBUserMail"
        Me.TBUserMail.Size = New System.Drawing.Size(192, 20)
        Me.TBUserMail.TabIndex = 2
        '
        'TBUserName
        '
        Me.TBUserName.Location = New System.Drawing.Point(120, 16)
        Me.TBUserName.Name = "TBUserName"
        Me.TBUserName.Size = New System.Drawing.Size(192, 20)
        Me.TBUserName.TabIndex = 0
        '
        'UserModify
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(328, 155)
        Me.Controls.Add(Me.BtCancel)
        Me.Controls.Add(Me.BtOK)
        Me.Controls.Add(Me.LbUserPhone)
        Me.Controls.Add(Me.LbUserMail)
        Me.Controls.Add(Me.LbUserName)
        Me.Controls.Add(Me.TBUserPhone)
        Me.Controls.Add(Me.TBUserMail)
        Me.Controls.Add(Me.TBUserName)
        Me.MaximumSize = New System.Drawing.Size(344, 193)
        Me.MinimumSize = New System.Drawing.Size(344, 193)
        Me.Name = "UserModify"
        Me.Text = "UserModify"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtCancel As System.Windows.Forms.Button
    Friend WithEvents BtOK As System.Windows.Forms.Button
    Friend WithEvents LbUserPhone As System.Windows.Forms.Label
    Friend WithEvents LbUserMail As System.Windows.Forms.Label
    Friend WithEvents LbUserName As System.Windows.Forms.Label
    Friend WithEvents TBUserPhone As System.Windows.Forms.TextBox
    Friend WithEvents TBUserMail As System.Windows.Forms.TextBox
    Friend WithEvents TBUserName As System.Windows.Forms.TextBox
End Class
