<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.LbTelefon = New System.Windows.Forms.Label()
        Me.BtAdd = New System.Windows.Forms.Button()
        Me.ListCaptadors = New System.Windows.Forms.ListBox()
        Me.CbActiu = New System.Windows.Forms.CheckBox()
        Me.BtTelefon = New System.Windows.Forms.Button()
        Me.BtNom = New System.Windows.Forms.Button()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.LbTelefon)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtAdd)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ListCaptadors)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CbActiu)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtTelefon)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtNom)
        Me.SplitContainer1.Size = New System.Drawing.Size(692, 387)
        Me.SplitContainer1.SplitterDistance = 286
        Me.SplitContainer1.TabIndex = 0
        '
        'LbTelefon
        '
        Me.LbTelefon.AutoSize = True
        Me.LbTelefon.Location = New System.Drawing.Point(221, 34)
        Me.LbTelefon.Name = "LbTelefon"
        Me.LbTelefon.Size = New System.Drawing.Size(0, 13)
        Me.LbTelefon.TabIndex = 11
        '
        'BtAdd
        '
        Me.BtAdd.Location = New System.Drawing.Point(87, 239)
        Me.BtAdd.Name = "BtAdd"
        Me.BtAdd.Size = New System.Drawing.Size(116, 92)
        Me.BtAdd.TabIndex = 10
        Me.BtAdd.Text = "Button1"
        Me.BtAdd.UseVisualStyleBackColor = True
        '
        'ListCaptadors
        '
        Me.ListCaptadors.FormattingEnabled = True
        Me.ListCaptadors.Location = New System.Drawing.Point(39, 34)
        Me.ListCaptadors.Name = "ListCaptadors"
        Me.ListCaptadors.Size = New System.Drawing.Size(164, 121)
        Me.ListCaptadors.TabIndex = 9
        '
        'CbActiu
        '
        Me.CbActiu.AutoSize = True
        Me.CbActiu.Location = New System.Drawing.Point(224, 136)
        Me.CbActiu.Name = "CbActiu"
        Me.CbActiu.Size = New System.Drawing.Size(59, 17)
        Me.CbActiu.TabIndex = 5
        Me.CbActiu.Text = "Activar"
        Me.CbActiu.UseVisualStyleBackColor = True
        '
        'BtTelefon
        '
        Me.BtTelefon.Location = New System.Drawing.Point(222, 97)
        Me.BtTelefon.Name = "BtTelefon"
        Me.BtTelefon.Size = New System.Drawing.Size(91, 28)
        Me.BtTelefon.TabIndex = 4
        Me.BtTelefon.Text = "Canviar Telefon"
        Me.BtTelefon.UseVisualStyleBackColor = True
        '
        'BtNom
        '
        Me.BtNom.Location = New System.Drawing.Point(223, 63)
        Me.BtNom.Name = "BtNom"
        Me.BtNom.Size = New System.Drawing.Size(91, 28)
        Me.BtNom.TabIndex = 1
        Me.BtNom.Text = "Canviar Nom"
        Me.BtNom.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 387)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents BtTelefon As System.Windows.Forms.Button
    Friend WithEvents BtNom As System.Windows.Forms.Button
    Friend WithEvents CbActiu As System.Windows.Forms.CheckBox
    Friend WithEvents ListCaptadors As System.Windows.Forms.ListBox
    Friend WithEvents BtAdd As System.Windows.Forms.Button
    Friend WithEvents LbTelefon As System.Windows.Forms.Label

End Class
