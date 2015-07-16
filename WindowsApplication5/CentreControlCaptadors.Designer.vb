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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.BtActualitzaPorts = New System.Windows.Forms.Button()
        Me.LBoxPorts = New System.Windows.Forms.ListBox()
        Me.BtRemove = New System.Windows.Forms.Button()
        Me.TBoxTelefon = New System.Windows.Forms.TextBox()
        Me.LbTelefon = New System.Windows.Forms.Label()
        Me.BtAdd = New System.Windows.Forms.Button()
        Me.BtTelefon = New System.Windows.Forms.Button()
        Me.BtNom = New System.Windows.Forms.Button()
        Me.BtSave = New System.Windows.Forms.Button()
        Me.TBoxHistoric = New System.Windows.Forms.TextBox()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.GBoxSetup = New System.Windows.Forms.GroupBox()
        Me.CboxActivar = New System.Windows.Forms.CheckBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GBoxAdd = New System.Windows.Forms.GroupBox()
        Me.BtAlertes = New System.Windows.Forms.Button()
        Me.GBoxConnect = New System.Windows.Forms.GroupBox()
        Me.LbConnect = New System.Windows.Forms.Label()
        Me.BtSelectPort = New System.Windows.Forms.Button()
        Me.LVLastMessages = New System.Windows.Forms.ListView()
        Me.DataGridView = New System.Windows.Forms.DataGridView()
        Me.GBoxTBoxHistoric = New System.Windows.Forms.GroupBox()
        Me.GBoxLVLastMessages = New System.Windows.Forms.GroupBox()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.GBoxSetup.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBoxAdd.SuspendLayout()
        Me.GBoxConnect.SuspendLayout()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBoxTBoxHistoric.SuspendLayout()
        Me.GBoxLVLastMessages.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtActualitzaPorts
        '
        Me.BtActualitzaPorts.Location = New System.Drawing.Point(169, 22)
        Me.BtActualitzaPorts.Name = "BtActualitzaPorts"
        Me.BtActualitzaPorts.Size = New System.Drawing.Size(130, 30)
        Me.BtActualitzaPorts.TabIndex = 18
        Me.BtActualitzaPorts.Text = "Actualitzar ports"
        Me.BtActualitzaPorts.UseVisualStyleBackColor = True
        '
        'LBoxPorts
        '
        Me.LBoxPorts.FormattingEnabled = True
        Me.HelpProvider1.SetHelpString(Me.LBoxPorts, "Port")
        Me.LBoxPorts.Location = New System.Drawing.Point(9, 22)
        Me.LBoxPorts.Name = "LBoxPorts"
        Me.HelpProvider1.SetShowHelp(Me.LBoxPorts, True)
        Me.LBoxPorts.Size = New System.Drawing.Size(146, 108)
        Me.LBoxPorts.TabIndex = 16
        '
        'BtRemove
        '
        Me.BtRemove.Location = New System.Drawing.Point(6, 89)
        Me.BtRemove.Name = "BtRemove"
        Me.BtRemove.Size = New System.Drawing.Size(130, 30)
        Me.BtRemove.TabIndex = 13
        Me.BtRemove.Text = "Esborrar captador"
        Me.BtRemove.UseVisualStyleBackColor = True
        '
        'TBoxTelefon
        '
        Me.TBoxTelefon.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TBoxTelefon.Enabled = False
        Me.TBoxTelefon.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TBoxTelefon.Location = New System.Drawing.Point(55, 148)
        Me.TBoxTelefon.Name = "TBoxTelefon"
        Me.TBoxTelefon.Size = New System.Drawing.Size(112, 20)
        Me.TBoxTelefon.TabIndex = 12
        '
        'LbTelefon
        '
        Me.LbTelefon.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LbTelefon.AutoSize = True
        Me.LbTelefon.Location = New System.Drawing.Point(6, 151)
        Me.LbTelefon.Name = "LbTelefon"
        Me.LbTelefon.Size = New System.Drawing.Size(43, 13)
        Me.LbTelefon.TabIndex = 11
        Me.LbTelefon.Text = "Telèfon"
        '
        'BtAdd
        '
        Me.BtAdd.Location = New System.Drawing.Point(6, 12)
        Me.BtAdd.Name = "BtAdd"
        Me.BtAdd.Size = New System.Drawing.Size(130, 30)
        Me.BtAdd.TabIndex = 10
        Me.BtAdd.Text = "Afegir nou captador"
        Me.BtAdd.UseVisualStyleBackColor = True
        '
        'BtTelefon
        '
        Me.BtTelefon.Location = New System.Drawing.Point(6, 53)
        Me.BtTelefon.Name = "BtTelefon"
        Me.BtTelefon.Size = New System.Drawing.Size(130, 30)
        Me.BtTelefon.TabIndex = 4
        Me.BtTelefon.Text = "Canviar Telefon"
        Me.BtTelefon.UseVisualStyleBackColor = True
        '
        'BtNom
        '
        Me.BtNom.Location = New System.Drawing.Point(6, 17)
        Me.BtNom.Name = "BtNom"
        Me.BtNom.Size = New System.Drawing.Size(130, 30)
        Me.BtNom.TabIndex = 1
        Me.BtNom.Text = "Canviar Nom"
        Me.BtNom.UseVisualStyleBackColor = True
        '
        'BtSave
        '
        Me.BtSave.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtSave.Location = New System.Drawing.Point(131, 340)
        Me.BtSave.Name = "BtSave"
        Me.BtSave.Size = New System.Drawing.Size(97, 30)
        Me.BtSave.TabIndex = 17
        Me.BtSave.Text = "Desar historial..."
        Me.BtSave.UseVisualStyleBackColor = True
        '
        'TBoxHistoric
        '
        Me.TBoxHistoric.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TBoxHistoric.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TBoxHistoric.Location = New System.Drawing.Point(431, 290)
        Me.TBoxHistoric.Multiline = True
        Me.TBoxHistoric.Name = "TBoxHistoric"
        Me.TBoxHistoric.ReadOnly = True
        Me.TBoxHistoric.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TBoxHistoric.Size = New System.Drawing.Size(338, 315)
        Me.TBoxHistoric.TabIndex = 15
        '
        'GBoxSetup
        '
        Me.GBoxSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBoxSetup.Controls.Add(Me.CboxActivar)
        Me.GBoxSetup.Controls.Add(Me.PictureBox1)
        Me.GBoxSetup.Controls.Add(Me.BtTelefon)
        Me.GBoxSetup.Controls.Add(Me.TBoxTelefon)
        Me.GBoxSetup.Controls.Add(Me.LbTelefon)
        Me.GBoxSetup.Controls.Add(Me.BtNom)
        Me.GBoxSetup.Controls.Add(Me.BtRemove)
        Me.GBoxSetup.Location = New System.Drawing.Point(785, 271)
        Me.GBoxSetup.Name = "GBoxSetup"
        Me.GBoxSetup.Size = New System.Drawing.Size(308, 180)
        Me.GBoxSetup.TabIndex = 19
        Me.GBoxSetup.TabStop = False
        '
        'CboxActivar
        '
        Me.CboxActivar.AutoSize = True
        Me.HelpProvider1.SetHelpString(Me.CboxActivar, "Si no voleu que un dispositiu mostri més alarmes desactiveu aquesta casella")
        Me.CboxActivar.Location = New System.Drawing.Point(6, 125)
        Me.CboxActivar.Name = "CboxActivar"
        Me.HelpProvider1.SetShowHelp(Me.CboxActivar, True)
        Me.CboxActivar.Size = New System.Drawing.Size(112, 17)
        Me.CboxActivar.TabIndex = 14
        Me.CboxActivar.Text = "Alarmes activades"
        Me.CboxActivar.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(179, 19)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(120, 120)
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'GBoxAdd
        '
        Me.GBoxAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBoxAdd.Controls.Add(Me.BtAlertes)
        Me.GBoxAdd.Controls.Add(Me.BtAdd)
        Me.GBoxAdd.Location = New System.Drawing.Point(785, 458)
        Me.GBoxAdd.Name = "GBoxAdd"
        Me.GBoxAdd.Size = New System.Drawing.Size(308, 48)
        Me.GBoxAdd.TabIndex = 20
        Me.GBoxAdd.TabStop = False
        '
        'BtAlertes
        '
        Me.BtAlertes.Location = New System.Drawing.Point(169, 12)
        Me.BtAlertes.Name = "BtAlertes"
        Me.BtAlertes.Size = New System.Drawing.Size(130, 30)
        Me.BtAlertes.TabIndex = 11
        Me.BtAlertes.Text = "Configuració alertes"
        Me.BtAlertes.UseVisualStyleBackColor = True
        '
        'GBoxConnect
        '
        Me.GBoxConnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBoxConnect.Controls.Add(Me.LbConnect)
        Me.GBoxConnect.Controls.Add(Me.BtSelectPort)
        Me.GBoxConnect.Controls.Add(Me.LBoxPorts)
        Me.GBoxConnect.Controls.Add(Me.BtActualitzaPorts)
        Me.GBoxConnect.Location = New System.Drawing.Point(785, 511)
        Me.GBoxConnect.Name = "GBoxConnect"
        Me.GBoxConnect.Size = New System.Drawing.Size(307, 135)
        Me.GBoxConnect.TabIndex = 21
        Me.GBoxConnect.TabStop = False
        Me.GBoxConnect.Text = "Connexió"
        '
        'LbConnect
        '
        Me.LbConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LbConnect.Location = New System.Drawing.Point(166, 98)
        Me.LbConnect.Name = "LbConnect"
        Me.LbConnect.Size = New System.Drawing.Size(130, 30)
        Me.LbConnect.TabIndex = 20
        Me.LbConnect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtSelectPort
        '
        Me.BtSelectPort.Location = New System.Drawing.Point(169, 58)
        Me.BtSelectPort.Name = "BtSelectPort"
        Me.BtSelectPort.Size = New System.Drawing.Size(130, 30)
        Me.BtSelectPort.TabIndex = 19
        Me.BtSelectPort.Text = "Connectar al port"
        Me.BtSelectPort.UseVisualStyleBackColor = True
        '
        'LVLastMessages
        '
        Me.LVLastMessages.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LVLastMessages.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LVLastMessages.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.LVLastMessages.Location = New System.Drawing.Point(9, 17)
        Me.LVLastMessages.MultiSelect = False
        Me.LVLastMessages.Name = "LVLastMessages"
        Me.LVLastMessages.Size = New System.Drawing.Size(388, 351)
        Me.LVLastMessages.TabIndex = 23
        Me.LVLastMessages.TileSize = New System.Drawing.Size(345, 30)
        Me.LVLastMessages.UseCompatibleStateImageBehavior = False
        Me.LVLastMessages.View = System.Windows.Forms.View.Tile
        '
        'DataGridView
        '
        Me.DataGridView.AllowUserToAddRows = False
        Me.DataGridView.AllowUserToDeleteRows = False
        Me.DataGridView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.HelpProvider1.SetHelpString(Me.DataGridView, "Dispositius monitoritzats")
        Me.DataGridView.Location = New System.Drawing.Point(12, 12)
        Me.DataGridView.MultiSelect = False
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.ReadOnly = True
        Me.DataGridView.RowHeadersVisible = False
        Me.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView.ShowEditingIcon = False
        Me.HelpProvider1.SetShowHelp(Me.DataGridView, True)
        Me.DataGridView.Size = New System.Drawing.Size(1081, 253)
        Me.DataGridView.TabIndex = 24
        '
        'GBoxTBoxHistoric
        '
        Me.GBoxTBoxHistoric.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBoxTBoxHistoric.Controls.Add(Me.BtSave)
        Me.GBoxTBoxHistoric.Location = New System.Drawing.Point(421, 271)
        Me.GBoxTBoxHistoric.Name = "GBoxTBoxHistoric"
        Me.GBoxTBoxHistoric.Size = New System.Drawing.Size(358, 375)
        Me.GBoxTBoxHistoric.TabIndex = 27
        Me.GBoxTBoxHistoric.TabStop = False
        Me.GBoxTBoxHistoric.Text = "Historial del captador"
        '
        'GBoxLVLastMessages
        '
        Me.GBoxLVLastMessages.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GBoxLVLastMessages.Controls.Add(Me.LVLastMessages)
        Me.GBoxLVLastMessages.Location = New System.Drawing.Point(8, 271)
        Me.GBoxLVLastMessages.Name = "GBoxLVLastMessages"
        Me.GBoxLVLastMessages.Size = New System.Drawing.Size(407, 375)
        Me.GBoxLVLastMessages.TabIndex = 28
        Me.GBoxLVLastMessages.TabStop = False
        Me.GBoxLVLastMessages.Text = "Darrers missatges rebuts"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1104, 658)
        Me.Controls.Add(Me.DataGridView)
        Me.Controls.Add(Me.GBoxAdd)
        Me.Controls.Add(Me.TBoxHistoric)
        Me.Controls.Add(Me.GBoxSetup)
        Me.Controls.Add(Me.GBoxConnect)
        Me.Controls.Add(Me.GBoxTBoxHistoric)
        Me.Controls.Add(Me.GBoxLVLastMessages)
        Me.Name = "Form1"
        Me.Text = "CentreControlCaptadors (CCC.beta)"
        Me.GBoxSetup.ResumeLayout(False)
        Me.GBoxSetup.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBoxAdd.ResumeLayout(False)
        Me.GBoxConnect.ResumeLayout(False)
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBoxTBoxHistoric.ResumeLayout(False)
        Me.GBoxLVLastMessages.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtTelefon As System.Windows.Forms.Button
    Friend WithEvents BtNom As System.Windows.Forms.Button
    Friend WithEvents BtAdd As System.Windows.Forms.Button
    Friend WithEvents LbTelefon As System.Windows.Forms.Label
    Friend WithEvents TBoxTelefon As System.Windows.Forms.TextBox
    Friend WithEvents BtRemove As System.Windows.Forms.Button
    Friend WithEvents TBoxHistoric As System.Windows.Forms.TextBox
    Friend WithEvents BtSave As System.Windows.Forms.Button
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents BtActualitzaPorts As System.Windows.Forms.Button
    Friend WithEvents LBoxPorts As System.Windows.Forms.ListBox
    Friend WithEvents GBoxSetup As System.Windows.Forms.GroupBox
    Friend WithEvents GBoxAdd As System.Windows.Forms.GroupBox
    Friend WithEvents GBoxConnect As System.Windows.Forms.GroupBox
    Friend WithEvents BtSelectPort As System.Windows.Forms.Button
    Friend WithEvents LVLastMessages As System.Windows.Forms.ListView
    Friend WithEvents LbConnect As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents CboxActivar As System.Windows.Forms.CheckBox
    Friend WithEvents GBoxTBoxHistoric As System.Windows.Forms.GroupBox
    Friend WithEvents GBoxLVLastMessages As System.Windows.Forms.GroupBox
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents BtAlertes As System.Windows.Forms.Button

End Class
