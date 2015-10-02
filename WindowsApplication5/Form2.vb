Imports System.Net.Mail
Imports System
Imports System.IO
Imports System.Text
Imports System.Threading

Public Class ConfigAlerts

    Public ReadOnly TEST_MESSAGE = "Missatge de prova"
    Public Shared idToModify As Integer = -1

    Private Sub BtTestMail_Click(sender As Object, e As EventArgs) Handles BtTestMail.Click
        'Prova que el mail funcioni. Demana una direcció on enviar.
        'Si alguna configuració fos errònia mostrarà un error.
        Dim response As String
        Dim email As New ArrayList

        response = InputBox("Enviar correu a la direcció: ", "Prova configuració mail", "").Trim()
        If response <> "" Then
            email.Add(response)
            Dim message As String
            Dim body As String = "Això és un missatge de prova"
            Dim subject As String = "Prova CentreControlCaptadors"

            message = MailConfiguration.sendMail(email, body, subject, MailPriority.Normal)
            MsgBox(message, vbOKOnly)
        End If

    End Sub

    Private Sub BtConfigMail_Click(sender As Object, e As EventArgs) Handles BtConfigMail.Click
        'Mostra a la pantalla la configuració actual del mail i obre el form de configuració del mail.

        MailConfiguration.SetMailConfig()

        FormConfigMail.TBoxSMTPServer.Text = MailConfiguration.SmtpConfig
        If MailConfiguration.SslConfig = "ON" Then
            FormConfigMail.CBoxSSL.Checked = True
        Else
            FormConfigMail.CBoxSSL.Checked = False
        End If

        FormConfigMail.TBoxPort.Text = MailConfiguration.PortConfig
        FormConfigMail.TBoxLogin.Text = MailConfiguration.LoginConfig
        FormConfigMail.TBoxPassword.Text = MailConfiguration.PasswdConfig

        FormConfigMail.Show()
    End Sub

    Private Sub ConfigAlerts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Load Form. Carreguem les dades al form.

        'Carreguem els ports serial disponibles
        CBoxSerialPort.Items.Clear()

        For Each sp As String In My.Computer.Ports.SerialPortNames
            CBoxSerialPort.Items.Add(sp)
        Next
        If CBoxSerialPort.SelectedIndex = -1 Then
            BtTestSMS.Enabled = False
        End If

        'Carreguem el llistat de captadors
        Dim captador As New Captador

        For Each captador In CCC.json.devices
            CkLBCaptadors.Items.Add(captador.Nom)
        Next

        'Carreguem el llistat d'usuaris actius
        updateUsersList()

    End Sub

    Sub updateUsersList()
        LBUsers.Items.Clear()
        For Each user In CCC.json.users
            If user.activeUser Then
                LBUsers.Items.Add(user.name)
            End If
        Next
    End Sub
    Private Sub BtTestSMS_Click(sender As Object, e As EventArgs) Handles BtTestSMS.Click
        'Test del SMS
        If CCC.threadSMSON Then
            Dim res = MsgBox("Una vegada acabat el test haureu de restaurar la connexió manualment a la finestra principal. Voleu continuar?", vbYesNo)
            If res = vbNo Then
                Exit Sub
            Else
                Dim i As Integer = 0
                CCC.connectStablished = False
                CCC.ClosePort(CCC.SerialPort1)
                CCC.ChangeConnectSign(-1)
                While CCC.threadSMSON = True
                    Cursor = System.Windows.Forms.Cursors.WaitCursor
                    Thread.Sleep(1000)
                    i += 1
                    If i >= 5 Then
                        IOTextFiles.RoundLog("ThreadSMS not dying!")
                        Exit While
                    End If
                End While
                Cursor = System.Windows.Forms.Cursors.Default
            End If
        End If

        Dim serialportname As String = CBoxSerialPort.SelectedItem
        Dim response As String = ""

        If serialportname <> "" Then
            response = SMSConfiguration.openPort(serialportname, SerialPortTest)
            If response <> "OK" Then
                IOTextFiles.RoundLog("& problem accessing serial port from Form2")
                MsgBox("No s'ha pogut accedir correctament al port", vbCritical)
                CBoxSerialPort.SelectedItem = Nothing
                BtTestSMS.Enabled = False
                Exit Sub
            End If
        End If
        'Forcem mode "detalls" al mòdem
        SMSConfiguration.sendToModem(SerialPortTest, "ATV1")
        response = SMSConfiguration.readFromModem(SerialPortTest, "OK")
        If response = "ERROR" Then
            IOTextFiles.RoundLog("& Error: " & "ATV1-TEST")
            MsgBox("No s'ha pogut accedir correctament al port", vbCritical)
            CBoxSerialPort.SelectedItem = Nothing
            BtTestSMS.Enabled = False
            Exit Sub
        End If
        'Forcem mode text al mòdem
        SMSConfiguration.sendToModem(SerialPortTest, "AT+CMGF=1")
        response = SMSConfiguration.readFromModem(SerialPortTest, "OK")
        If response = "ERROR" Then
            IOTextFiles.RoundLog("& Error: " & "AT+CMGF=1-TEST")
            MsgBox("No s'ha pogut accedir correctament al port", vbCritical)
            CBoxSerialPort.SelectedItem = Nothing
            BtTestSMS.Enabled = False
            Exit Sub
        End If
        Dim phone = InputBox("Telèfon on voleu enviar el SMS? (9 dígits)", "Test SMS")
        If phone <> Nothing Then
            Dim ret = SMSConfiguration.sendSMS(phone, SerialPortTest, TEST_MESSAGE)
            MsgBox(ret, vbOKOnly)
        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBoxSerialPort.SelectedIndexChanged
        'ComboBox amb els ports 'COM' disponibles
        If CBoxSerialPort.SelectedItem <> Nothing Then
            BtTestSMS.Enabled = True
        Else
            BtTestSMS.Enabled = False
        End If
    End Sub

    Private Sub BtSelectAll_Click(sender As Object, e As EventArgs) Handles BtSelectAll.Click
        Dim i As Integer = 0
        While i < CkLBCaptadors.Items.Count
            CkLBCaptadors.SetItemChecked(i, True)
            i += 1
        End While

    End Sub

    Private Sub BtDeselectAll_Click(sender As Object, e As EventArgs) Handles BtDeselectAll.Click
        Dim i As Integer = 0
        While i < CkLBCaptadors.Items.Count
            CkLBCaptadors.SetItemChecked(i, False)
            i += 1
        End While
    End Sub

    Private Sub BtAddUser_Click(sender As Object, e As EventArgs) Handles BtAddUser.Click
        UserForm.ShowDialog(Me)
    End Sub

    Private Sub BtRemoveUser_Click(sender As Object, e As EventArgs) Handles BtRemoveUser.Click
        If LBUsers.SelectedItem <> Nothing Then
            Dim userName = LBUsers.Items(LBUsers.SelectedIndex)
            Dim id = JsonFile.getId(userName)
            If id >= 0 Then
                JsonFile.removeUser(id)
            End If
            updateUsersList()
        End If
    End Sub

    Private Sub BtModifyUser_Click(sender As Object, e As EventArgs) Handles BtModifyUser.Click

        If LBUsers.SelectedIndex >= 0 Then
            Dim userName = LBUsers.Items(LBUsers.SelectedIndex)
            Dim id = JsonFile.getId(userName)
            If id >= 0 Then
                UserModify.TBUserName.Text = userName
                UserModify.TBUserPhone.Text = JsonFile.getPhone(id)
                UserModify.TBUserMail.Text = JsonFile.getMail(id)
                idToModify = id
                UserModify.ShowDialog(Me)
            End If
        Else
            updateUsersList()
        End If
    End Sub

    Private Sub BtClose_Click(sender As Object, e As EventArgs) Handles BtClose.Click
        Me.Close()
    End Sub

    Private Sub LBUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LBUsers.SelectedIndexChanged
        'Marcar els captadors que té seleccionats aquest usuari.
        If LBUsers.SelectedIndex >= 0 Then
            Dim selectedUser As String = LBUsers.SelectedItem.ToString
            Dim devicesList As New List(Of String)
            Dim userId = JsonFile.getId(selectedUser)

            devicesList = JsonFile.getAssignedToUser(userId)
            Dim ending = CkLBCaptadors.Items.Count
            Dim i As Integer = 0
            While i < ending
                Dim index = devicesList.IndexOf(CkLBCaptadors.Items(i))
                If index >= 0 Then
                    CkLBCaptadors.SetItemCheckState(i, CheckState.Checked)
                Else
                    CkLBCaptadors.SetItemCheckState(i, CheckState.Unchecked)
                End If
                i += 1
            End While
        End If
    End Sub

    Private Sub LBUsers_KeyDown(sender As Object, e As KeyEventArgs) Handles LBUsers.KeyDown
        If e.KeyCode = Keys.Delete Then
            BtRemoveUser_Click(sender, e)
        End If
    End Sub

    Private Sub BtAssignDevices_Click(sender As Object, e As EventArgs) Handles BtAssignDevices.Click
        'Tots els captadors marcats seran els captadors assignats a l'usuari, independentment dels captadors que tenia abans
        Dim user As String = LBUsers.SelectedItem
        Dim id As Integer = JsonFile.getId(user)

        Dim devices As New List(Of String)
        Dim device As String

        For Each device In CkLBCaptadors.CheckedItems
            devices.Add(device)
        Next
        JsonFile.setAssignedUser(devices, id)

    End Sub
End Class

