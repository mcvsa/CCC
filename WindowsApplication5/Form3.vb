Imports System
Imports System.IO
Imports System.Text

Public Class FormConfigMail

    Private Sub BtConfigMailNow_Click(sender As Object, e As EventArgs) Handles BtConfigMailNow.Click
        'Guarda la configuració del servidor de correu.
        Dim sslConf As String

        If CBoxSSL.Checked = True Then
            sslConf = "ON"
        Else
            sslConf = "OFF"
        End If
        MailConfiguration.saveMailConfig(TBoxSMTPServer.Text, sslConf, TBoxPort.Text, TBoxLogin.Text, TBoxPassword.Text)

        Me.Close()

    End Sub

End Class