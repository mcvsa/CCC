
Imports System
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Net.Mail
Imports Newtonsoft.Json

Public Class CCC
    Public Shared connectStablished As Boolean = False
    Public Shared tancant As Boolean = False
    Public Shared freeSpace As Boolean = True
    Dim threadDiskSpace As New Thread(AddressOf SpaceWorker)
    Delegate Sub MostraHistorialCallback([nomCaptador] As String)
    Delegate Sub UpdateLastDataCallback()
    Delegate Sub ChangeLbConnectCallback([conexio] As Integer)
    Delegate Sub UpdateDataGridViewCallback([capta] As Captador)
    Dim vectorSMS As New List(Of String)
    Public threadSMSON As Boolean = False
    Public Shared json As New JsonFile

    Public Const VELOCIDADPUERTO As Integer = 9600 '115200, 9600
    Const TIME4THREAD As Integer = 5000 'Cada quant de temps mirem els SMS rebuts: 1000
    Const TIME4SPACE As Integer = 5000 'Cada quant de temps mirem l'espai disponible de disc.
    Const LOWHDD As Long = 314572800 '300 Megues: avís de poc espai al disc dur.
    Const NUMMAXOFSMS As Integer = 30 'Número màxim de missatges al panell de darrers avisos.
    Const MAXLOGENTRIES As Integer = 1000 'Número màxim de línies al log.
    Const NUM_COLS As Integer = 5 'Número de columnes del DataGridView.
    Const TIME2SMS As Integer = 5000 'Temps d'espera per a donar temps al mòdem a processar el SMS anterior

    Public ReadOnly SETTINGSFILE As String = Application.StartupPath & "\resources\settings.json"
    Public ReadOnly STARTUP_PATH = Application.StartupPath
    ReadOnly LOG As String = Application.StartupPath & "\resources\log.txt"
    ReadOnly LOG1 As String = Application.StartupPath & "\resources\log.txt.1"
    ReadOnly HISTORIC As String = Application.StartupPath & "\resources\historic"
    Public ReadOnly PATH_REGISTRES As String = Application.StartupPath & "\data\"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not My.Computer.FileSystem.DirectoryExists(PATH_REGISTRES) Then
            My.Computer.FileSystem.CreateDirectory(PATH_REGISTRES)
        End If

        JsonFile.updateJsonVar()

        Dim captador As New Captador

        For Each captador In json.devices
            If Not My.Computer.FileSystem.FileExists(PATH_REGISTRES & "\" & captador.Nom) Then
                captador.creaRegistreCaptador()
            End If
        Next

        If My.Computer.FileSystem.FileExists(HISTORIC) Then
            Dim historicReader As System.IO.StreamReader
            historicReader = My.Computer.FileSystem.OpenTextFileReader(HISTORIC)

            vectorSMS.Clear()
            While Not historicReader.EndOfStream
                Dim strLine = historicReader.ReadLine
                vectorSMS.Add(strLine)
            End While
            historicReader.Close()
            UpdateLastData()
        Else
            IOTextFiles.createFile(HISTORIC)
        End If

        InitializeDataGridView()

        ChangeConnectSign(-1)
        ActivateOptions(False)
        UpdatePortsList()

        If threadDiskSpace.IsAlive = True Then
            threadDiskSpace.Abort()
        End If
        threadDiskSpace.Start()
        DataGridView.ClearSelection()

        Dim item As Object
        Dim index As Integer = 0
        If LBoxPorts.Items.Count > 0 Then
            For Each item In LBoxPorts.Items
                If json.comPort = item.ToString Then
                    LBoxPorts.SelectedIndex = index
                    Connect2Port()
                    Exit For
                End If
                index += 1
            Next
        End If

    End Sub

    Sub InitializeDataGridView()

        DataGridView.ColumnCount = NUM_COLS
        DataGridView.ColumnHeadersVisible = True

        Dim columnHeaderStyle As New DataGridViewCellStyle()
        columnHeaderStyle.ForeColor = Color.Black
        columnHeaderStyle.Font = New Font(DataGridView.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold)

        DataGridView.ColumnHeadersDefaultCellStyle = columnHeaderStyle

        'DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        With DataGridView
            .Columns(0).Name = "Captador"
            .Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(1).Name = "Últim estat conegut"
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(2).Name = "Darrer missatge rebut"
            .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(3).Name = "Data"
            .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(4).Name = "Alarmes activades"
            .Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
        End With

        Dim captador As New Captador

        DataGridView.Rows.Clear()
        For Each captador In json.devices
            updateDataGridView(captador)
        Next

    End Sub

    Sub updateDataGridView(ByVal [capta] As Captador)
        'Actualitza l'estat del captador al GridView
        'Un thread tindrà accés al control:

        If Me.DataGridView.InvokeRequired Then
            Dim d As New UpdateDataGridViewCallback(AddressOf updateDataGridView)
            Me.Invoke(d, New Object() {[capta]})

        Else
            If Not tancant Then
                Dim crrntRow As Integer
                Dim indexa As Integer
                Dim fecha As String = ""

                'Recuperem el darrer estat i missatge coneguts
                capta.FuncioDarrerEstatConegut()

                Dim form = capta.LastMessage
                Dim lastMessage = ""

                indexa = form.IndexOf(" , ")
                If indexa >= 0 Then
                    fecha = form.Substring(0, indexa).Trim
                    indexa = form.LastIndexOf(" , ")
                    lastMessage = form.Substring(indexa + 2).Trim
                Else
                    fecha = ""
                End If

                crrntRow = -1
                If DataGridView.DisplayedRowCount(False) <> 0 Then
                    crrntRow = DataGridView.CurrentRow.Index
                End If

                Dim fila() As String = {capta.Nom, capta.LastState, lastMessage, fecha, capta.Actiu}
                'Busquem si el captador ja està al DataGridView per actualitzar-lo i no afegir-lo si existeix
                Dim rowindex As Integer = -1
                For Each row As DataGridViewRow In DataGridView.Rows
                    If row.Cells.Item(0).Value = capta.Nom Then
                        rowindex = row.Index
                    End If
                Next
                If rowindex >= 0 Then
                    DataGridView.Rows(rowindex).SetValues(fila)
                Else
                    DataGridView.Rows.Add(fila)
                    rowindex = DataGridView.Rows.Count - 1
                End If
                Select Case capta.LastState
                    Case Captador.ALARMA
                        DataGridView.Item(1, rowindex).Style.BackColor = Color.Red
                    Case Captador.ALARMA_RED
                        DataGridView.Item(1, rowindex).Style.BackColor = Color.Red
                    Case Captador.FIN_CICLO
                        DataGridView.Item(1, rowindex).Style.BackColor = Color.Yellow
                    Case Captador.WARNING
                        DataGridView.Item(1, rowindex).Style.BackColor = Color.Yellow
                    Case Captador.RED_OK
                        DataGridView.Item(1, rowindex).Style.BackColor = Color.GreenYellow
                    Case Captador.UNKNOWN_MESSAGE
                        DataGridView.Item(1, rowindex).Style.BackColor = Color.White
                    Case Captador.FIN_FILTRO
                        DataGridView.Item(1, rowindex).Style.BackColor = Color.GreenYellow
                    Case Captador.MENSAJE_TEST
                        DataGridView.Item(1, rowindex).Style.BackColor = Color.GreenYellow
                    Case Else
                        DataGridView.Item(1, rowindex).Style.BackColor = Color.White
                End Select

                If crrntRow <> -1 And crrntRow < DataGridView.Rows.Count Then
                    DataGridView.CurrentCell = DataGridView(0, crrntRow)
                Else
                    DataGridView.ClearSelection()
                End If
            End If
        End If

    End Sub

    Sub UpdateLastData()
        'Actualitza les darreres dades rebudes al panell i al fitxer pertinent
        'Ordenem els SMS de més nou a més antic.

        If Me.LVLastMessages.InvokeRequired Then
            Dim x As New UpdateLastDataCallback(AddressOf UpdateLastData)
            Try
                Me.Invoke(x)
            Catch ex As Exception
                RoundLog(ex.Message)
            End Try
        Else
            vectorSMS.Sort()
            vectorSMS.Reverse()
            'Si el vector és massa gran, no voldrem presentar tants missatges per pantalla: eliminem el darrer element
            While vectorSMS.Count >= NUMMAXOFSMS
                vectorSMS.RemoveAt(vectorSMS.Count - 1)
            End While

            Dim aux As String
            'Esborrem l'arxiu i el llistat
            If My.Computer.FileSystem.FileExists(HISTORIC) Then
                My.Computer.FileSystem.WriteAllText(HISTORIC, "", False)
            Else
                IOTextFiles.createFile(HISTORIC)
            End If
            LVLastMessages.Clear()
            'Tornem a omplir l'arxiu i el llistat amb els valors nous i ordenats
            For Each aux In vectorSMS
                LVLastMessages.Items.Add(aux)
                My.Computer.FileSystem.WriteAllText(HISTORIC, aux & Chr(13), True)
            Next
        End If

    End Sub

    Function buscaCaptador(ByRef nom As String)
        'Busca el captador que tingui per nom el paràmetre nom

        Dim captador As New Captador

        For Each captador In json.devices
            If captador.Nom = nom Then
                Return json.devices.IndexOf(captador)
            End If
        Next
        Return (-1)

    End Function

    Function buscaTelefon(ByRef telefon As String)
        'Busca el telèfon que li passem per paràmetre en els captadors
        Dim captador As New Captador

        For Each captador In json.devices
            If captador.Telefon = telefon Then
                Return json.devices.IndexOf(captador)
            End If
        Next
        Return (-1)

    End Function

    Function comprovaTelefon(ByRef telefon As String)
        'Comprova que el telèfon té un format correcte
        'Retorna:
        '-1 si el telefon no té un format correcte
        '-2 si el telèfon té un format correcte i no està repetit
        'El número d'índex de l'array de captadors on s'ha trobat el telèfon

        Dim numTelefon As Long

        '        If telefon.Length <> 9 Then
        'Return -1
        'End If

        'If Not telefon.StartsWith("6") And Not telefon.StartsWith("7") And Not telefon.StartsWith("9") Then
        'Return -1
        'End If

        Try
            numTelefon = CLng(telefon)
        Catch ex As Exception
            Return -1
        End Try

        Dim trobat As Integer = buscaTelefon(telefon)
        If trobat <> -1 Then
            Return trobat
        End If

        Return -2

    End Function

    Sub MostraHistorial(ByVal [nomCaptador] As String)
        'Mostra l'historial del captador seleccionat
        'Un thread tindrà accés a un control:

        If Me.TBoxHistoric.InvokeRequired Then
            Dim d As New MostraHistorialCallback(AddressOf MostraHistorial)
            Try
                Me.Invoke(d, New Object() {[nomCaptador]})
            Catch ex As Exception
                RoundLog("MostraHistorial Thread exception: " & ex.Message)
            End Try
        Else
            If Not tancant Then
                Dim filereader As String

                filereader = ""
                TBoxHistoric.Text = ""

                If nomCaptador <> Nothing Then
                    If My.Computer.FileSystem.FileExists(PATH_REGISTRES + nomCaptador) Then
                        filereader = My.Computer.FileSystem.ReadAllText(PATH_REGISTRES + nomCaptador)
                        TBoxHistoric.Text = filereader

                        'Else
                        '   MsgBox("No s'ha trobat l'arxiu de dades associades a aquest captador", vbOKOnly)
                    End If
                End If
            End If
        End If

    End Sub

    Function OpenningPort(ByRef port As String) As String
        'Funció per a connectar al mòdem série
        Dim cont As Integer = 0
        connectStablished = False
        Cursor = System.Windows.Forms.Cursors.WaitCursor
        While (threadSMSON)
            Thread.Sleep(1000)
            cont += 1
            If cont >= 10 Then
                Exit While
            End If
        End While
        If Not threadSMSON Then
            ChangeConnectSign(-1)

            Dim res = SMSConfiguration.openPort(port, SerialPort1)

            Dim reset = SMSConfiguration.resetModem(SerialPort1)

            If res = "OK" And reset = "OK" Then
                connectStablished = True
                ChangeConnectSign(0)
                json.comPort = port
                IOTextFiles.updateJsonFile(json)

                Dim threadSMS As New Thread(AddressOf SMSWorker)
                If Not threadSMS.IsAlive Then
                    threadSMS.Start()
                    threadSMSON = True
                End If
                OpenningPort = vbOK
            Else
                OpenningPort = "Obrint port: " & res & " - " & "Reset port: " & reset
                connectStablished = False
                Cursor = System.Windows.Forms.Cursors.Default
            End If
        Else
            OpenningPort = "Error desconnectant el port"
            Close()
        End If

    End Function

    Private Sub BtNom_Click(sender As Object, e As EventArgs) Handles BtNom.Click
        'Canvia el nom d'un captador.
        Dim nouNom As String
        Dim indexCaptador As Integer
        Dim captador As New Captador
        Dim nomAntic As String

        nomAntic = Me.DataGridView(0, DataGridView.CurrentRow.Index).Value

        If nomAntic <> Nothing Then
            nouNom = InputBox("Quin nom voleu pel captador?", "Nou nom pel captador").Trim()
            If nouNom <> "" Then
                indexCaptador = buscaCaptador(nouNom)
                If indexCaptador = -1 Then
                    json.devices(buscaCaptador(nomAntic)).Nom = nouNom
                    IOTextFiles.updateJsonFile(json)
                    captador.creaRegistreCaptador()
                    InitializeDataGridView()
                    DataGridView.Focus()
                Else
                    MsgBox("Aquest captador ja existeix", vbOKOnly)
                End If
            End If
        End If
    End Sub

    Private Sub BtTelefon_Click(sender As Object, e As EventArgs) Handles BtTelefon.Click
        'Canvia el telèfon d'un captador.
        Dim nouTelefon As String
        Dim captador As New Captador
        Dim nomCaptador As String

        nomCaptador = Me.DataGridView(0, DataGridView.CurrentRow.Index).Value

        If nomCaptador <> Nothing Then
            nouTelefon = InputBox("Introduïu el número de telèfon." + vbCrLf + "(només caràcters numèrics. Ex.: 987654321):", "Nou número de telèfon associat al captador")
            nouTelefon = nouTelefon.Replace(".", "")
            If nouTelefon <> "" Then
                If comprovaTelefon(nouTelefon) < -1 Then
                    json.devices(buscaCaptador(nomCaptador)).Telefon = nouTelefon
                    TBoxTelefon.Text = captador.Telefon
                    InitializeDataGridView()
                    DataGridView.Focus()
                Else
                    MsgBox("Telèfon incorrecte o assignat a un altre captador", vbOKOnly)
                End If
            End If
        End If

    End Sub

    Private Sub BtAdd_Click(sender As Object, e As EventArgs) Handles BtAdd.Click
        'Afegeix un captador a la llista dels captadors i al vector de captadors, sempre que no hi sigui

        Dim nom As String
        Dim telefon As String
        Dim captador As New Captador

        nom = InputBox("Nom: ", "Nom del nou captador", "Captador").Trim()

        If nom <> "" Then
            If buscaCaptador(nom) = -1 Then
                telefon = InputBox("Introduïu el número de telèfon." + vbCrLf + "(només caràcters numèrics. Ex.: 987654321):", "Telèfon associat al nou captador").Trim()
                telefon = telefon.Replace(".", "")
                If telefon <> "" Then
                    If comprovaTelefon(telefon) < -1 Then
                        captador.Nom = nom
                        captador.Telefon = telefon
                        captador.Actiu = True

                        JsonFile.addDevice(captador)
                        captador.creaRegistreCaptador()
                        InitializeDataGridView()
                        DataGridView.Focus()
                    Else
                        MsgBox("Telèfon incorrecte o assignat a un altre captador", vbOKOnly)
                    End If
                End If
            Else
                MsgBox("El Captador ja existeix", vbOKOnly)
            End If
        End If

    End Sub

    Private Sub ActivateOptions(ByRef state As Boolean)

        BtNom.Enabled = state
        BtTelefon.Enabled = state
        BtRemove.Enabled = state
        CboxActivar.Enabled = state

        If state = False Then
            GBoxSetup.Text = ""
        Else
            GBoxSetup.Text = Me.DataGridView(0, DataGridView.CurrentRow.Index).Value
        End If

    End Sub
    Private Sub BtRemove_Click(sender As Object, e As EventArgs) Handles BtRemove.Click
        'Esborrar captador de la llista e captadors. S'han d'esborrar les dades del captador també?
        Dim esborrar As Integer
        Dim captador As New Captador
        Dim nom As String

        nom = Me.DataGridView(0, DataGridView.CurrentRow.Index).Value

        If nom <> "" Then
            esborrar = MsgBox("Voleu esborrar aquest captador? Es perdran totes les dades referents al mateix.", vbYesNo)
            If esborrar = 6 Then
                JsonFile.removeDevice(buscaCaptador(nom))
                InitializeDataGridView()
                TBoxHistoric.Text = ""
                TBoxTelefon.Text = ""
            End If
        End If

        ActivateOptions(False)

    End Sub

    Private Sub LBoxPorts_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles LBoxPorts.MouseDoubleClick
        'Doble click sobre el port fa la connexió del port
        BtSelectPort_Click(sender, e)
    End Sub

    Private Sub BtSave_Click(sender As Object, e As EventArgs) Handles BtSave.Click
        'Guarda historial d'un captador allà on vulgui l'usuari.
        If TBoxHistoric.Text <> "" Then
            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Filter = "Documento de texto|*.txt"
            saveFileDialog1.Title = "Deseu les dades"
            saveFileDialog1.ShowDialog()
            If saveFileDialog1.FileName <> "" Then
                My.Computer.FileSystem.WriteAllText(saveFileDialog1.FileName, TBoxHistoric.Text, False)
            End If
        End If
    End Sub

    Sub UpdatePortsList()
        'Show all available COM ports.
        LBoxPorts.Items.Clear()

        For Each sp As String In My.Computer.Ports.SerialPortNames
            LBoxPorts.Items.Add(sp)
        Next
        If LBoxPorts.SelectedIndex = -1 Then
            BtSelectPort.Enabled = False
        End If
    End Sub

    Private Sub BtActualitzaPorts_Click(sender As Object, e As EventArgs) Handles BtActualitzaPorts.Click

        UpdatePortsList()

    End Sub

    Private Sub BtSelectPort_Click(sender As Object, e As EventArgs) Handles BtSelectPort.Click
        Connect2Port()
    End Sub

    Sub Connect2Port()
        'Seleccionar port i connectar
        Dim port As String
        Dim connectat As String

        If LBoxPorts.SelectedIndex <> -1 Then
            port = LBoxPorts.SelectedItem.ToString

            If port <> "" Then
                connectat = OpenningPort(port)

                If connectat = "1" Then
                    connectStablished = True
                    ChangeConnectSign(1)
                Else
                    connectStablished = False
                    ChangeConnectSign(-1)
                    MsgBox(connectat, vbCritical)
                    RoundLog("Error with connection: " & connectat)
                End If
            End If
        End If

    End Sub

    Sub ClosePort(portserie)
        'Tanca el port de connexions

        Dim i As Integer = 0
        portserie.Close()
        connectStablished = False
        ChangeConnectSign(-1)

    End Sub

    Sub RoundLog(ByVal message As String)
        'Guarda missatges d'error al log (amb marca de temps). Serà un log de com a màxim 100 línies i
        'quan arribem a les 100 línies copiarem el log a log.txt.1 i començarem de zero el nou log.
        Dim marcaTemps As Date
        marcaTemps = Now

        message = marcaTemps & "-" & message

        If Not My.Computer.FileSystem.FileExists(LOG) Then
            IOTextFiles.createFile(LOG)
        End If
        Dim logLines() As String = File.ReadAllLines(LOG)

        If logLines.Length >= MAXLOGENTRIES Then
            'Copia el que hi ha al fitxer a log1.txt.1 (i matxaca el que hi havia prèviament)
            System.IO.File.Copy(LOG, LOG1, True)

            'Esborra el log inicial i el torna a crear per a poder continuar afegint-hi línies
            System.IO.File.Delete(LOG)
            'System.IO.File.Create(LOG)
            IOTextFiles.createFile(LOG)
        End If
        My.Computer.FileSystem.WriteAllText(LOG, message & vbCrLf, True)

    End Sub

    Private Sub LBoxPorts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LBoxPorts.SelectedIndexChanged
        'Activa el botó per a començar la connexió
        If LBoxPorts.SelectedIndex <> -1 Then
            BtSelectPort.Enabled = True
        End If
    End Sub

    Function FormatString(ByRef str As String)
        'Elimina les cometes i els espais finals i del principi d'un text
        str = str.Trim()
        str = str.Trim(Chr(34))
        Return (str)
    End Function

    Public Sub SMSWorker()
        'Thread: Ha de llegir (cada 'TIMEFORTHREAD' mil·lisegons els SMS rebuts, posar-los al fitxer que toqui_
        'i eilminar els SMS llegits
        threadSMSON = True

        If connectStablished Then

            Dim returnstr As String = ""
            Dim response As String = ""
            Dim finalChain As String = "OK" & vbCr & ""
            Dim texts As New List(Of SMSText)
            Dim unsentTexts As New List(Of unsentWorkaround)

            'Forcem mode 'detalls' al mòdem (que no mostri números, sino 'OK' i detalls)
            SMSConfiguration.sendToModem(SerialPort1, "ATV1")
            response = SMSConfiguration.readFromModem(SerialPort1, "OK")
            If response = "ERROR" Then
                RoundLog("& Error: " & "ATV1")
                connectStablished = False
                ChangeConnectSign(-1)
            End If

            'Forcem mode text al mòdem
            SMSConfiguration.sendToModem(SerialPort1, "AT+CMGF=1")
            response = SMSConfiguration.readFromModem(SerialPort1, "OK")
            If response = "ERROR" Then
                RoundLog("& Error: " & "AT+CMGF=1")
                connectStablished = False
                ChangeConnectSign(-1)
            End If

            While connectStablished
                returnstr = ""
                response = ""

                SMSConfiguration.sendToModem(SerialPort1, "AT+CMGL=" & Chr(34) & "ALL" & Chr(34) & Chr(13))

                response = SMSConfiguration.readFromModem(SerialPort1, finalChain)
                If response = "ERROR" Then
                    RoundLog("& Error: " & "AT+CMGL")
                    connectStablished = False
                    ChangeConnectSign(-1)
                End If

                returnstr = response

                'Preparem la resposta per a no tenir problemes amb '\0D' o vbCr
                returnstr = returnstr.Replace("\0D", vbCr)
                'Debug
                returnstr = returnstr.Replace("\0A", vbCr)
                ' End Debug

                While returnstr.Length > 59 '59 serà el mínim número de caràcters si rebem un SMS
                    'Generem un nou element SMS
                    Dim txt As New SMSText
                    'Buscar "+CMGL: "
                    Dim indexa = returnstr.IndexOf("+CMGL:")
                    If indexa < 0 Then
                        'No hi ha res més de nou per a llegir
                        Exit While
                    End If
                    Dim indexb As Integer = 0
                    'Esborrem la primera part del text (que no ens serveix)
                    returnstr = returnstr.Remove(0, indexa + 6)
                    'Tornem a indexar
                    indexa = 0
                    'El número de SMS serà de indexa fins a la ','
                    indexb = returnstr.IndexOf(",")
                    Dim txtRead As String
                    txtRead = returnstr.Substring(indexa, indexb - indexa)
                    'Busquem la cadena 'READ'
                    indexa = returnstr.IndexOf("READ")
                    'Eliminem fins a l'inici del número de telèfon (7 caràcters): READ","
                    returnstr = returnstr.Remove(0, indexa + 7)
                    'El final del número de telèfon és: ",
                    indexb = returnstr.IndexOf(",") - 1
                    'Si és un número que comença per '+34' haurem d'eliminar el '+34'
                    indexa = returnstr.IndexOf("+")
                    If indexa > 1 Then 'No hi ha +34
                        indexa = 0
                    Else 'Si hi ha +34 (o qualsevol altre codi de país)
                        indexa = indexa + 3
                    End If
                    txt.Phone = returnstr.Substring(indexa, indexb - indexa)

                    'Eliminem part del missatge que ja no ens interessa
                    returnstr = returnstr.Remove(0, indexb)
                    'Busquem el nom del captador
                    'Mirem on comença el nom del captador
                    indexb = returnstr.IndexOf(vbCr)
                    'Esborrem fins on comença el nom del captador
                    returnstr = returnstr.Remove(0, indexb + 1)
                    indexb = 0
                    indexa = returnstr.IndexOf("+CMGL")
                    If indexa < 0 Then
                        'El darrer caràcter capturat hauria de ser "OK"
                        indexa = returnstr.IndexOf("OK")
                        If indexa < 0 Then
                            indexa = returnstr.Length - 1
                        End If
                    End If
                    Dim ashole = returnstr.Substring(indexb, indexa)
                    If ashole.Length < 59 Then
                        returnstr.Remove(indexb, indexa - indexb)
                        Continue While
                    End If

                    'Busquem el final del nom del captador
                    indexa = returnstr.IndexOf(vbCr)
                    'El nom serà des del final del vbcrlf fins l'espai anterior al '\'
                    txt.Name = returnstr.Substring(indexb, indexa - indexb - 1).Trim
                    'Eliminem part del missatge que ja no ens interessa
                    returnstr = returnstr.Remove(0, indexa + 1)
                    'Busquem data i hora d'enviament
                    indexa = returnstr.IndexOf(vbCr)
                    Dim datahora As DateTime
                    Try
                        datahora = returnstr.Substring(0, indexa)
                        txt.DataHora = datahora.ToString("u")
                    Catch ex As Exception
                        RoundLog("SMSWorker: SMS error data conversion-" & ex.Message)
                        indexa = returnstr.IndexOf("+CMGL")
                        If indexa < 0 Then
                            indexa = returnstr.Length - 1
                            returnstr.Remove(0, indexa)
                            Continue While
                        End If
                    End Try
                    'Eliminem la part del missatge que no ens interessa
                    returnstr = returnstr.Remove(0, indexa + 1)
                    'Busquem el número de filtre
                    indexb = returnstr.IndexOf("#")
                    indexa = returnstr.IndexOf("/")
                    Try
                        txt.Filter = CInt(returnstr.Substring(indexb + 1, indexa - indexb - 1))
                        'Busquem total filtres:
                        indexb = returnstr.IndexOf(vbCr)
                        txt.NumFilters = CInt(returnstr.Substring(indexa + 1, indexb - indexa - 1))
                    Catch ex As Exception
                        RoundLog("SMSWorker: SMS error NumFilters-" & ex.Message)
                        indexa = returnstr.IndexOf("+CMGL")
                        If indexa < 0 Then
                            indexa = returnstr.Length - 1
                            returnstr.Remove(0, indexa)
                            Continue While
                        End If
                    End Try
                    'Esborrem part del missatge llegida. També esborrem el 'vbCr'
                    returnstr = returnstr.Remove(0, indexb + 1)
                    'El cos del missatge està des del principi, i fins el proper SMS
                    indexa = returnstr.IndexOf("+CMGL")
                    If indexa < 0 Then
                        'El darrer caràcter capturat hauria de ser "OK"
                        indexa = returnstr.IndexOf("OK")
                        If indexa < 0 Then
                            indexa = returnstr.Length - 1
                        End If
                    End If
                    'Workaround "OKAT"
                    Dim auxTxtBody = returnstr.Substring(0, indexa)
                    Dim auxIndex = auxTxtBody.IndexOf("OKAT")
                    If auxIndex < 0 Then
                        txt.Body = returnstr.Substring(0, indexa)
                    Else
                        txt.Body = returnstr.Substring(0, auxIndex)
                        RoundLog("Found OKAT in message " & auxTxtBody & " from: " & txt.Name)
                    End If
                    'End workaround
                    txt.AllMessage = ashole
                    'Apuntem les dades corresponents al captador que toqui
                    Dim indexCaptador As Integer
                    indexCaptador = comprovaTelefon(txt.Phone)
                    If indexCaptador >= 0 Or indexCaptador = -2 Then
                        'Si indexCaptador = -2: Cal mostrar el missatge per a que l'usuari
                        'pugui crear el captador i associar-li el telèfon
                        Dim form As String
                        form = ""
                        form = txt.DataHora & " , " & txt.Name & " , " & txt.Phone & " , " & "Filtre: " _
                               & txt.Filter & " de " & txt.NumFilters & " , " & txt.Body

                        vectorSMS.Add(form)
                        'Ordenem el vector, per si arriben missatges del mateix captador desordenats
                        vectorSMS.Sort()
                        UpdateLastData()

                        If indexCaptador >= 0 Then
                            json.devices(indexCaptador).addData(form)
                            'Si el captador està seleccionat mostrarem l'historial actualitzat

                            If DataGridView.CurrentRow.Index <> Nothing Then
                                If DataGridView.CurrentRow.Index = indexCaptador Then
                                    MostraHistorial(json.devices(indexCaptador).Nom)
                                End If
                            End If
                            'Actualitzem el panell dels darrers missatges i la resta de dades per pantalla
                            '...sempre que el captador estigui actiu
                            If json.devices(indexCaptador).Actiu Then
                                updateDataGridView(json.devices(indexCaptador))

                                'Envia mails amb un nou thread
                                Dim threadMailerDaemon As New Thread(AddressOf MailerDaemonWorker)
                                threadMailerDaemon.Start(txt)

                                'Envia SMS
                                Dim phone As String = ""
                                Dim deviceIndex As Integer = buscaTelefon(txt.Phone)
                                If deviceIndex >= 0 Then
                                    Dim userID As Integer
                                    For Each userID In json.devices(deviceIndex).UsersList
                                        phone = JsonFile.getPhone(userID)
                                        If phone <> Nothing Then
                                            Dim resSMS = SMSConfiguration.sendSMS(phone, SerialPort1, txt.AllMessage)
                                            If resSMS <> "OK" Then
                                                'MsgBox(resSMS, vbOKOnly)
                                                RoundLog("& Error sending SMS: " & txt.AllMessage)
                                                Dim unsent As New unsentWorkaround
                                                unsent.phone = phone
                                                unsent.message = txt.AllMessage
                                                unsentTexts.Add(unsent)
                                            End If
                                            'Thread.Sleep(TIME2SMS)
                                            'Abans d'enviar el següent SMS mirem si hi ha missatges nous rebuts per a no perdre'ls en cas extrem
                                            If connectStablished Then
                                                SMSConfiguration.sendToModem(SerialPort1, "AT+CMGL=" & Chr(34) & "REC UNREAD" & Chr(34) & Chr(13))
                                                response = SMSConfiguration.readFromModem(SerialPort1, finalChain)
                                                If response = "ERROR" Then
                                                    RoundLog("& Error: " & "AT+CMGL 2nd time")
                                                    'connectStablished = False
                                                    'ChangeConnectSign(-1)
                                                End If
                                                returnstr += response
                                                returnstr.Replace("\0D", vbCr)
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                        End If

                    ElseIf (indexCaptador = -1) Then
                        RoundLog("Telèfon " & txt.Phone & " erroni")
                    Else
                        RoundLog("Error amb el telèfon " & txt.Phone)
                    End If

                    'Debug
                    'Esborrem el missatge llegit:
                    SMSConfiguration.sendToModem(SerialPort1, "AT+CMGD=" & txtRead & Chr(13))
                    response = SMSConfiguration.readFromModem(SerialPort1, "OK")
                    'End Debug

                End While
                ChangeConnectSign(1)
                Thread.Sleep(TIME4THREAD)
                Dim indexUnsent As New List(Of Integer)
                Dim i As Integer = 0
                For Each unsentMessage In unsentTexts
                    Dim resUnsentSMS = SMSConfiguration.sendSMS(unsentMessage.phone, SerialPort1, unsentMessage.message)
                    If resUnsentSMS = "OK" Then
                        indexUnsent.add(i)
                    Else
                        RoundLog("& Error sending again unsent SMS: " & unsentMessage.message & "-Phone: " & unsentMessage.phone)
                    End If
                    i += 1
                Next
                For Each index In indexUnsent
                    unsentTexts.RemoveAt(index)
                Next
            End While
        Else
            ClosePort(SerialPort1)
        End If
        threadSMSON = False
    End Sub

    Public Sub MailerDaemonWorker(ByVal smsTxt As SMSText)

        Dim addresses As New ArrayList
        Dim deviceIndex As Integer = buscaTelefon(smsTxt.Phone)
        If deviceIndex >= 0 Then
            Dim userId As Integer
            For Each userId In CCC.json.devices(deviceIndex).UsersList
                addresses.Add(JsonFile.getMail(userId))
            Next
        End If
        If addresses.Count > 0 Then
            MailConfiguration.sendMail(addresses, smsTxt.AllMessage, smsTxt.Name, MailPriority.Normal)
        End If
    End Sub

    Sub ChangeConnectSign(ByVal [conexio] As Integer)
        'Canviar el rètol: CONNECTAT/CONNECTANT.../DESCONNECTAT. Accessible també des el thread el control LbConnect
        '- DESCONNECTAT: conexio = -1
        '- CONNECTANT...: conexio = 0
        '- CONNECTAT: conexio = 1

        If Me.LbConnect.InvokeRequired Then
            Dim l As New ChangeLbConnectCallback(AddressOf ChangeConnectSign)
            Try
                Me.Invoke(l, New Object() {[conexio]})
            Catch ex As Exception
                RoundLog("ChangeConnectSign Thread exception: " & ex.Message)
            End Try
        Else
            Select Case conexio
                Case 1
                    LbConnect.Text = "CONNECTAT"
                    LbConnect.ForeColor = Color.Green
                    If LBoxPorts.SelectedIndex <> -1 Then
                        BtSelectPort.Enabled = True
                    End If
                    Cursor = System.Windows.Forms.Cursors.Default
                Case 0
                    LbConnect.Text = "CONNECTANT..."
                    LbConnect.ForeColor = Color.Orange
                    BtSelectPort.Enabled = False
                    Cursor = System.Windows.Forms.Cursors.WaitCursor
                Case -1
                    LbConnect.Text = "DESCONNECTAT"
                    LbConnect.ForeColor = Color.Red
                    If LBoxPorts.SelectedIndex <> -1 Then
                        BtSelectPort.Enabled = True
                    End If
                    Cursor = System.Windows.Forms.Cursors.Default
            End Select
        End If
    End Sub

    Public Sub SpaceWorker()
        'La funció d'aquest thread ha de ser monitoritzar l'espai disponible al disc dur i avisar si cal buidar arxius.
        Dim HDDDrive As System.IO.DriveInfo
        While freeSpace = True
            Try
                HDDDrive = My.Computer.FileSystem.GetDriveInfo(Application.StartupPath)
                Dim freeHDD = HDDDrive.AvailableFreeSpace
                If freeHDD < LOWHDD Then
                    MsgBox("Queda poc espai al disc dur", vbCritical)
                    RoundLog("Queda poc espai al disc dur")
                End If
                Thread.Sleep(TIME4SPACE)
            Catch ex As Exception
                MsgBox(ex.Message)
                RoundLog("Error with SpaceWorker: " & ex.Message)
                freeSpace = False
            End Try
        End While

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Abans de tancar el programa principal cal parar els threads.
        tancant = True
        Dim i As Integer
        i = 0

        connectStablished = False
        freeSpace = False
        Cursor = System.Windows.Forms.Cursors.WaitCursor
        While SerialPort1.IsOpen
            Thread.Sleep(2000)
            i += 1
            If i >= 5 Then
                ClosePort(SerialPort1)
                Exit While
            End If
        End While
        ChangeConnectSign(-1)
        i = 0
        While threadSMSON = True Or threadDiskSpace.IsAlive
            i += 1
            If i >= 5 Then
                If threadDiskSpace.IsAlive Then
                    threadDiskSpace.Abort()
                End If
                Exit While
            End If
            Thread.Sleep(2000)
        End While
        ' Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub LVLastMessages_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVLastMessages.SelectedIndexChanged
        'Si l'usuari clica sobre un dels elements de la llista, aquest deixarà d'estar en negreta.
        If LVLastMessages.SelectedItems.Count > 0 Then
            Dim elemento = LVLastMessages.SelectedItems(0)
            elemento.Font = New Font(LVLastMessages.SelectedItems(0).Font, FontStyle.Regular)
        End If

    End Sub

    Private Sub LVLastMessages_DoubleClick(sender As Object, e As EventArgs) Handles LVLastMessages.DoubleClick
        'Si l'usuari fa doble click sobre un dels elements de la llista, aquest es marcarà en negreta.
        If LVLastMessages.SelectedItems.Count > 0 Then
            Dim elemento = LVLastMessages.SelectedItems(0)
            elemento.Font = New Font(LVLastMessages.SelectedItems(0).Font, FontStyle.Bold)
        End If

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim webAddress As String = "http://www.mcvsa.com/"
        Process.Start(webAddress)
    End Sub

    Private Sub CBoxActivar_CheckedChanged(sender As Object, e As EventArgs) Handles CboxActivar.Click
        Dim captador As New Captador
        Dim nomCaptador As String

        nomCaptador = Me.DataGridView(0, DataGridView.CurrentRow.Index).Value

        If nomCaptador <> Nothing Then
            captador = json.devices(buscaCaptador(nomCaptador))
            captador.Actiu = CboxActivar.Checked
            updateDataGridView(captador)
            DataGridView.Focus()
        End If
    End Sub

    Private Sub DataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellContentClick, DataGridView.CellClick
        ' Quan es selecciona un captador:

        Dim captador As New Captador
        Dim nomCaptador As String
        If DataGridView.RowCount > 0 Then
            nomCaptador = Me.DataGridView(0, DataGridView.CurrentRow.Index).Value

            ' 1) Mostrem l'historial
            MostraHistorial(nomCaptador)

            ' 2) Mostrar telèfon del captador i activar les opcions possibles
            Dim indexCaptador As Integer

            'TBoxHistoric.Text = ""
            indexCaptador = buscaCaptador(nomCaptador)
            If indexCaptador > json.devices.Count Or indexCaptador = -1 Then
                TBoxTelefon.Text = ""
            Else
                TBoxTelefon.Text = json.devices(indexCaptador).Telefon
                captador = json.devices(indexCaptador)
                CboxActivar.Checked = captador.Actiu
            End If

            MostraHistorial(captador.Nom)
            'TBoxHistoric.Focus()
            ActivateOptions(True)
        End If
    End Sub

    Private Sub defocus(sender As Object, e As EventArgs) Handles MyBase.Click
        'Unfocus when clicking anywhere on the form
        Me.Focus()

        If DataGridView.SelectedCells.Count > 0 Then
            DataGridView.SelectedCells(0).Selected = False
        End If

        ActivateOptions(False)
        TBoxHistoric.Clear()

    End Sub

    Private Sub BtAlertes_Click(sender As Object, e As EventArgs) Handles BtAlertes.Click
        ConfigAlerts.Show()
    End Sub

End Class

Public Class Captador

    Public Const ALARMA As String = "ALARMA"
    Public Const ALARMA_RED As String = "ALARMA XARXA"
    Public Const RED_OK As String = "XARXA OK"
    Public Const WARNING As String = "WARNING!"
    Public Const FIN_CICLO As String = "CICLE FINALITZAT"
    Public Const UNKNOWN_MESSAGE As String = "MISSATGE NO RECONEGUT"
    Public Const FIN_FILTRO As String = "OK FI DE FILTRE"
    Public Const MENSAJE_TEST As String = "MISSATGE DE TEST"
    Public Const NO_MESSAGE As String = "-"

    'Classe captador: nom, telèfon associat i estat (actiu o no)

    Public Nom As String
    Public Telefon As String
    Public Actiu As Boolean = False
    Public LastMessage As String = ""
    Public LastState As String = ""
    Public alarmMail As Boolean = False
    Public alarmSMS As Boolean = False
    Public UsersList As New List(Of Integer)

    Public Function FuncioDarrerMissatge()
        Dim linea As String

        If My.Computer.FileSystem.FileExists(CCC.PATH_REGISTRES & Nom) Then
            linea = ""
            Dim stReader As System.IO.StreamReader
            stReader = My.Computer.FileSystem.OpenTextFileReader(CCC.PATH_REGISTRES & Nom)
            While Not stReader.EndOfStream
                linea = stReader.ReadLine
                If linea <> "" Then
                    LastMessage = linea
                End If
            End While
            stReader.Close()
        End If

        Return LastMessage

    End Function

    Public Function FuncioDarrerEstatConegut()
        'Actualitzem el darrer missatge
        FuncioDarrerMissatge()

        If LastMessage.IndexOf("Alarma") >= 0 Then
            LastState = ALARMA
        ElseIf LastMessage.IndexOf("Sin RED") >= 0 Then
            LastState = ALARMA_RED
        ElseIf LastMessage.IndexOf("RED OK") >= 0 Then
            LastState = RED_OK
        ElseIf LastMessage.IndexOf("Escobillas") >= 0 Then
            LastState = WARNING
        ElseIf LastMessage.IndexOf("Ciclo finalizado") >= 0 Then
            LastState = FIN_CICLO
        ElseIf LastMessage.IndexOf("Filtro finaliz") >= 0 Then
            LastState = FIN_FILTRO
        ElseIf LastMessage.IndexOf("test") >= 0 Then
            LastState = MENSAJE_TEST
        ElseIf LastMessage = "" Then
            LastState = NO_MESSAGE
        Else
            LastState = UNKNOWN_MESSAGE
        End If

        Return LastState

    End Function

    Public Sub creaRegistreCaptador()
        'Crea el registre del captador.
        Dim rutaRegistre As String

        If Not My.Computer.FileSystem.FileExists(CCC.PATH_REGISTRES & Nom) Then

            rutaRegistre = CCC.PATH_REGISTRES & Nom

            IOTextFiles.createFile(rutaRegistre)
        End If

    End Sub

    Sub addData(ByRef text2write As String)
        'Afegeix dades al registre del captador

        Dim file2write As String

        file2write = CCC.PATH_REGISTRES & Nom

        If Not My.Computer.FileSystem.FileExists(file2write) Then
            creaRegistreCaptador()
        End If

        My.Computer.FileSystem.WriteAllText(file2write, text2write & vbCrLf, True)

    End Sub

End Class

Public Class SMSText
    'Classe SMS: telèfon que l'envia, capçalera, data i hora enviament, filtre actual, filtres instal·lats, missatge

    Public Phone As String
    Public Name As String
    Public DataHora As String
    Public Filter As Integer
    Public NumFilters As Integer
    Public Body As String
    Public AllMessage As String

End Class

Public Class MailServerConfig
    Public smtp As String
    Public ssl As String
    Public port As String
    Public login As String
    Public password As String
End Class

Public Class User
    Public id As Integer
    Public name As String
    Public phone As String
    Public mail As String
    Public activeUser As Boolean

End Class

Public Class JsonFile
    Public devices As New List(Of Captador)
    Public mailServerConfig As New MailServerConfig
    Public comPort As String
    Public users As New List(Of User)

    Public Shared Sub updateJsonVar()
        Try
            Dim filereader = My.Computer.FileSystem.ReadAllText(CCC.SETTINGSFILE)

            CCC.json = Newtonsoft.Json.JsonConvert.DeserializeObject(Of JsonFile)(filereader)

        Catch ex As Exception
            CCC.RoundLog(ex.ToString)
            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\resources")
            Dim newJson As String = JsonConvert.SerializeObject(CCC.json)
            My.Computer.FileSystem.WriteAllText(CCC.SETTINGSFILE, newJson, False)
        End Try
    End Sub

    Public Shared Sub addDevice(ByRef nouCaptador As Captador)
        CCC.json.devices.Add(nouCaptador)
        IOTextFiles.updateJsonFile(CCC.json)
    End Sub

    Public Shared Sub removeDevice(ByRef index As Integer)
        CCC.json.devices.RemoveAt(index)
        IOTextFiles.updateJsonFile(CCC.json)
    End Sub

    Public Shared Sub addUser(ByRef newUser As User)
        For Each usuari In CCC.json.users
            If usuari.phone = newUser.phone And usuari.mail = newUser.mail And usuari.name = newUser.name Then
                usuari.activeUser = True
                IOTextFiles.updateJsonFile(CCC.json)
                Exit Sub
            End If
        Next
        newUser.id = CCC.json.users.Count
        CCC.json.users.Add(newUser)

        IOTextFiles.updateJsonFile(CCC.json)
    End Sub

    Public Shared Sub removeUser(ByRef idUser As Integer)
        CCC.json.users(idUser).activeUser = False
        IOTextFiles.updateJsonFile(CCC.json)
    End Sub

    Public Shared Function getActiveUsers()
        Dim usuari As New User
        Dim ids As New List(Of Integer)
        For Each usuari In CCC.json.users
            If usuari.activeUser Then
                ids.Add(usuari.id)
            End If
        Next
        Return ids
    End Function

    Public Shared Function getId(ByRef userName As String)
        Dim id As Integer = -1

        For Each User In CCC.json.users
            If User.name = userName And User.activeUser = True Then
                Return User.id
            End If
        Next
        Return id
    End Function

    Public Shared Function getPhone(ByVal userId As Integer)
        Return (CCC.json.users(userId).phone)
    End Function

    Public Shared Function getMail(ByVal userId As Integer)
        Return (CCC.json.users(userId).mail)
    End Function

    Public Shared Function getName(ByVal userId As Integer)
        Return (CCC.json.users(userId).name)
    End Function

    Public Shared Sub setPhone(ByVal userId As Integer, phone As String)
        CCC.json.users(userId).phone = phone
        IOTextFiles.updateJsonFile(CCC.json)
    End Sub

    Public Shared Sub setMail(ByVal userId As Integer, mail As String)
        CCC.json.users(userId).mail = mail
        IOTextFiles.updateJsonFile(CCC.json)
    End Sub

    Public Shared Sub setName(ByVal userId As Integer, name As String)
        CCC.json.users(userId).name = name
        IOTextFiles.updateJsonFile(CCC.json)
    End Sub

    Public Shared Function getAssignedToUser(ByVal userId As String)
        Dim devicesList As New List(Of String)
        For Each device In CCC.json.devices
            If device.UsersList.IndexOf(userId) >= 0 Then
                devicesList.Add(device.Nom)
            End If
        Next
        Return (devicesList)

    End Function

    Public Shared Sub setAssignedUser(ByVal deviceNames As List(Of String), ByRef userId As Integer)
        'Només rebem els captadors seleccionats, vol dir que la resta no hauran de notificar al userId
        Dim captador As New Captador

        For Each captador In CCC.json.devices
            Dim deviceIndex = CCC.buscaCaptador(captador.Nom) 'Busca el captador
            If deviceIndex >= 0 Then 'Si el captador existeix
                Dim userIndex = CCC.json.devices(deviceIndex).UsersList.IndexOf(userId) 'Mirem si l'usuari és al captador
                If deviceNames.IndexOf(captador.Nom) >= 0 Then 'Si el captador és a la llista dels seleccionats
                    If userIndex < 0 Then 'L'usuari no pertanyia al llistat del captador
                        CCC.json.devices(deviceIndex).UsersList.Add(userId) 'L'afegim
                    End If 'Si l'usuari ja pertanyia al llistat, no fem res

                Else 'Si el captador no està seleccionat cal eliminar l'usuari del llistat (si hi és)
                    If userIndex >= 0 Then 'Si l'usuari estava configurat
                        CCC.json.devices(deviceIndex).UsersList.RemoveAt(userIndex)
                    End If 'Si l'usuari no hi era no fem res
                End If
            End If 'Si el captador no existeix no fem res
        Next
        IOTextFiles.updateJsonFile(CCC.json)

    End Sub

End Class

Public Class unsentWorkaround
    Public phone As String
    Public message As String
End Class