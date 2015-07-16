Imports System.Net.Mail
Imports System
Imports System.IO
Imports System.Text
Imports System.Threading

Public Class ConfigAlerts

    Public ReadOnly TEST_MESSAGE = "Missatge de prova"

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

        MailConfiguration.ReadConfigMail()

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

    Private Sub BtRemoveMail_Click(sender As Object, e As EventArgs) Handles BtRemoveMail.Click
        'Esborra la direcció de mail seleccionada a la llista.
        LBoxMails.Items.Remove(LBoxMails.SelectedItem)
        updateListBox(LBoxMails.Items, MailConfiguration.FILE_MAILS)
    End Sub

    Private Sub BtAddMail_Click(sender As Object, e As EventArgs) Handles BtAddMail.Click
        'Afegeix un mail al llistat de mails

        Dim mail As String
        Dim mailRep As String
        Dim trobat As Boolean = False
        Dim message As New MailMessage

        mail = InputBox("Nou destinatari", "Introduïu la direcció del nou destinatari").Trim

        If mail <> "" Then
            Try
                message.To.Add(New MailAddress(mail))
            Catch ex As Exception
                MsgBox("Direcció de correu invàlida", vbOKOnly)
                mail = ""
            End Try

            If mail <> "" Then
                For Each mailRep In LBoxMails.Items
                    If mail = mailRep Then
                        trobat = True
                    End If
                Next

                If Not trobat Then
                    LBoxMails.Items.Add(mail)
                    updateListBox(LBoxMails.Items, MailConfiguration.FILE_MAILS)
                End If
            End If
        End If
    End Sub


    Private Sub ConfigAlerts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Load Form. Carreguem les dades al form.

        'Carreguem la configuració d'alertes
        If Not My.Computer.FileSystem.FileExists(MailConfiguration.ALARM_CONFIGS) Then
            IOTextFiles.createFile(MailConfiguration.ALARM_CONFIGS)
            MailConfiguration.loadDefaultAlarmConfigs()
        Else
            MailConfiguration.getAlarmConfigs()
        End If
        If MailConfiguration.mailAlarm = "0" Then
            CBoxMail.Checked = False
        Else
            CBoxMail.Checked = True
        End If
        If MailConfiguration.smsAlarm = "0" Then
            CBoxSMS.Checked = False
        Else
            CBoxSMS.Checked = True
        End If

        'Carreguem el llistat de mails
        If My.Computer.FileSystem.FileExists(MailConfiguration.FILE_MAILS) Then
            Dim address As String = ""
            Dim stReader As System.IO.StreamReader
            stReader = My.Computer.FileSystem.OpenTextFileReader(MailConfiguration.FILE_MAILS)
            While Not stReader.EndOfStream
                address = stReader.ReadLine
                If address <> "" Then
                    LBoxMails.Items.Add(address)
                End If
            End While
            stReader.Close()
        End If

        'Carreguem els ports serial disponibles
        CBoxSerialPort.Items.Clear()

        For Each sp As String In My.Computer.Ports.SerialPortNames
            CBoxSerialPort.Items.Add(sp)
        Next
        If CBoxSerialPort.SelectedIndex = -1 Then
            BtTestSMS.Enabled = False
        End If

        'Carreguem el llistat de telèfons on enviar SMS
        If My.Computer.FileSystem.FileExists(SMSConfiguration.FILE_SMS) Then
            Dim phone As String = ""
            Dim phoneReader As System.IO.StreamReader
            phoneReader = My.Computer.FileSystem.OpenTextFileReader(SMSConfiguration.FILE_SMS)
            While Not phoneReader.EndOfStream
                phone = phoneReader.ReadLine
                If phone <> "" Then
                    LBoxSMS.Items.Add(phone)
                End If
            End While
            phoneReader.Close()
        End If
    End Sub

    Private Sub LBoxMails_KeyDown(sender As Object, e As KeyEventArgs) Handles LBoxMails.KeyDown
        'Tecla "Supr" és el mateix que esborrar

        If e.KeyCode = Keys.Delete Then
            BtRemoveMail_Click(sender, e)
        End If
    End Sub

    Private Sub LBoxSMS_KeyDown(sender As Object, e As KeyEventArgs) Handles LBoxSMS.KeyDown
        'Tecla "Supr" és el mateix que esborrar

        If e.KeyCode = Keys.Delete Then
            BtRemovePhone_Click(sender, e)
        End If
    End Sub

    Private Sub CBoxMail_CheckedChanged(sender As Object, e As EventArgs) Handles CBoxMail.CheckedChanged
        MailConfiguration.activateMailSmsAlarms(CBoxMail.Checked, CBoxSMS.Checked)
    End Sub

    Private Sub CBoxSMS_CheckedChanged(sender As Object, e As EventArgs) Handles CBoxSMS.CheckedChanged
        MailConfiguration.activateMailSmsAlarms(CBoxMail.Checked, CBoxSMS.Checked)
    End Sub

    Private Sub BtTestSMS_Click(sender As Object, e As EventArgs) Handles BtTestSMS.Click
        'Test del SMS
        If Form1.threadsmson Then
            Dim res = MsgBox("Una vegada acabat el test haureu de restaurar la connexió manualment a la finestra principal. Voleu continuar?", vbYesNo)
            If res = vbNo Then
                Exit Sub
            Else
                Dim i As Integer = 0
                Form1.connectStablished = False
                Form1.ClosePort(Form1.SerialPort1)
                Form1.ChangeConnectSign(-1)
                Cursor = System.Windows.Forms.Cursors.WaitCursor
                While Form1.threadSMSON = True
                    Thread.Sleep(1000)
                    i += 1
                    If i >= 5 Then
                        Form1.RoundLog("ThreadSMS not dying!")
                        Exit While
                    End If
                End While
                Cursor = System.Windows.Forms.Cursors.Default
            End If
        End If

        Dim serialportname As String = CBoxSerialPort.SelectedItem
        If serialportname <> "" Then
            SMSConfiguration.openPort(serialportname, SerialPortTest)
        End If

        Dim phone = InputBox("Telèfon on voleu enviar el SMS? (9 dígits)", "Test SMS")
        If phone <> Nothing Then
            Dim ret = SMSConfiguration.sendSMS(phone, SerialPortTest, TEST_MESSAGE)
            MsgBox(ret, vbOKOnly)
        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBoxSerialPort.SelectedIndexChanged

        If CBoxSerialPort.SelectedItem <> Nothing Then
            BtTestSMS.Enabled = True
        Else
            BtTestSMS.Enabled = False
        End If
    End Sub

    Private Sub BtRemovePhone_Click(sender As Object, e As EventArgs) Handles BtRemovePhone.Click
        'Esborra el número de telèfon seleccionat a la llista.
        LBoxSMS.Items.Remove(LBoxSMS.SelectedItem)
        updateListBox(LBoxSMS.Items, SMSConfiguration.FILE_SMS)
    End Sub

    Private Sub BtNewPhone_Click(sender As Object, e As EventArgs) Handles BtNewPhone.Click
        'Afegeix un telèfon al llistat de telèfons

        Dim phone As String
        Dim phoneRep As String
        Dim trobat As Boolean = False
        Dim correcte As Integer = -1

        phone = InputBox("Nou destinatari de SMS", "Introduïu el telèfon del nou destinatari").Trim

        If phone <> "" Then
            correcte = Form1.comprovaTelefon(phone)
            If correcte <> -1 Then
                For Each phoneRep In LBoxSMS.Items
                    If phone = phoneRep Then
                        trobat = True
                    End If
                Next
                If Not trobat Then
                    LBoxSMS.Items.Add(phone)
                    updateListBox(LBoxSMS.Items, SMSConfiguration.FILE_SMS)
                End If
            Else
                MsgBox("El telèfon no és correcte", vbOKOnly)
            End If
        End If
    End Sub

    Sub updateListBox(ByVal listOfItems As ListBox.ObjectCollection, ByRef file2update As String)
        'Actualitza el fitxer de telèfons.

        Dim item As New Object

        If Not My.Computer.FileSystem.FileExists(file2update) Then
            IOTextFiles.createFile(file2update)
        End If
        'Esborrem el fitxer
        My.Computer.FileSystem.WriteAllText(file2update, "", False)
        'Tornem a omplir el fitxer
        For Each item In listOfItems
            My.Computer.FileSystem.WriteAllText(file2update, CStr(item) & vbCrLf, True)
        Next
    End Sub
End Class
