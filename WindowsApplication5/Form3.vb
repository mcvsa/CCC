Imports System
Imports System.IO
Imports System.Text

Public Class FormConfigMail

    Private Sub BtDefaults_Click(sender As Object, e As EventArgs) Handles BtDefaults.Click
        'Carreguem els valors per defecte

        Dim res = MsgBox("Voleu configurar els valors per defecte?", vbYesNo)
        If res = 6 Then
            Defaults()
            Me.Close()
        End If

    End Sub

    Sub Defaults()
        'Reset to defaults mail configuration

        TBoxLogin.Text = MailConfiguration.LOGIN
        TBoxPassword.Text = MailConfiguration.PASSWORD
        TBoxPort.Text = MailConfiguration.PORT
        TBoxSMTPServer.Text = MailConfiguration.SMTP
        CBoxSSL.Checked = True
        writeSMSConfig(MailConfiguration.SMTP, "ON", MailConfiguration.PORT, MailConfiguration.LOGIN, MailConfiguration.PASSWORD)

    End Sub

    Sub writeSMSConfig(ByVal SMTPServer, ByVal SSLServer, ByVal portServer, ByVal loginServer, ByVal passwdServer)
        'Escriu al fitxer del servidor les dades corresponents

        Dim text2write = SMTPServer & vbCrLf & SSLServer & vbCrLf & portServer & vbCrLf & loginServer & vbCrLf & passwdServer
        IOTextFiles.writeFile(MailConfiguration.MAIL_CONFIG, text2write)

    End Sub

    Private Sub BtConfigMailNow_Click(sender As Object, e As EventArgs) Handles BtConfigMailNow.Click
        'Guarda la configuració del servidor de correu.
        Dim sslConf As String

        If CBoxSSL.Checked = True Then
            sslConf = "ON"
        Else
            sslConf = "OFF"
        End If
        writeSMSConfig(TBoxSMTPServer.Text, sslConf, TBoxPort.Text, TBoxLogin.Text, TBoxPassword.Text)

        Me.Close()

    End Sub
End Class