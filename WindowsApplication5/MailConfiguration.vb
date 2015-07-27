Imports System.Net.Mail

Module MailConfiguration

    Public ReadOnly FILE_MAILS As String = Form1.STARTUP_PATH & "\resources\mail.list"

    Public SmtpConfig As String
    Public SslConfig As String
    Public PortConfig As String
    Public LoginConfig As String
    Public PasswdConfig As String

    Public mailAlarm As Integer
    Public smsAlarm As Integer

    Sub SetMailConfig()
        'Reads mail configuration. If it is wrong or it doesn't exists: Warning, sets the default configuration

        SmtpConfig = GetSetting(Settings.APPNAME, Settings.SECTIONMAIL, Settings.KEYSMTP)
        SslConfig = GetSetting(Settings.APPNAME, Settings.SECTIONMAIL, Settings.KEYSSL)
        PortConfig = GetSetting(Settings.APPNAME, Settings.SECTIONMAIL, Settings.KEYPORT)
        LoginConfig = GetSetting(Settings.APPNAME, Settings.SECTIONMAIL, Settings.KEYLOGIN)
        PasswdConfig = GetSetting(Settings.APPNAME, Settings.SECTIONMAIL, Settings.KEYPASSWORD)

    End Sub

    Sub saveMailConfig(ByVal SMTPServer, ByVal SSLServer, ByVal portServer, ByVal loginServer, ByVal passwdServer)
        'Guarda les dades de configuració del servidor

        SaveSetting(Settings.APPNAME, Settings.SECTIONMAIL, Settings.KEYSMTP, SMTPServer)
        SaveSetting(Settings.APPNAME, Settings.SECTIONMAIL, Settings.KEYSSL, SSLServer)
        SaveSetting(Settings.APPNAME, Settings.SECTIONMAIL, Settings.KEYPORT, portServer)
        SaveSetting(Settings.APPNAME, Settings.SECTIONMAIL, Settings.KEYLOGIN, loginServer)
        SaveSetting(Settings.APPNAME, Settings.SECTIONMAIL, Settings.KEYPASSWORD, passwdServer)

    End Sub

    Sub activateMailSmsAlarms(ByVal statusMail As Boolean, ByVal statusSms As Boolean)
        'Writes alarms status in its config file

        Dim strMail As String = ""
        Dim strSMS As String = ""

        If statusMail Then
            strMail = "1"
        Else
            strMail = "0"
        End If
        If statusSms Then
            strSMS = "1"
        Else
            strSMS = "0"
        End If

        SaveSetting(Settings.APPNAME, Settings.SECTIONALARMS, Settings.KEYSMSALARM, strSMS)
        SaveSetting(Settings.APPNAME, Settings.SECTIONALARMS, Settings.KEYMAILALARM, strMail)

    End Sub

    Public Function sendMail(ByVal email As ArrayList, ByVal body As String, ByVal subject As String, ByVal priority As System.Net.Mail.MailPriority)

        Dim message As New MailMessage
        Dim smtp As New SmtpClient
        Dim receiver As String
        SetMailConfig()

        message.From = New MailAddress(LoginConfig)
        Try
            For Each receiver In email
                message.To.Add(New MailAddress(receiver))
            Next
        Catch ex As Exception
            Return ex.Message
        End Try

        message.Body = body
        message.Subject = subject
        message.Priority = priority

        If SslConfig = "ON" Then
            smtp.EnableSsl = True
        Else
            smtp.EnableSsl = False
        End If

        smtp.Port = PortConfig
        smtp.Host = SmtpConfig
        smtp.Credentials = New Net.NetworkCredential(LoginConfig, PasswdConfig)
        Try
            smtp.Send(message)
        Catch ex As Exception
            Form1.RoundLog("Error testing mail: " & ex.Message)
            Return ex.Message
        End Try

        Return "OK"
    End Function

    Public Sub getAlarmConfigs()

        Dim smsAlarmConfig = GetSetting(Settings.APPNAME, Settings.SECTIONALARMS, Settings.KEYSMSALARM)
        Dim mailAlarmConfig = GetSetting(Settings.APPNAME, Settings.SECTIONALARMS, Settings.KEYMAILALARM)

        If smsAlarmConfig = "" Then
            smsAlarmConfig = "0"
        End If
        If mailAlarmConfig = "" Then
            mailAlarmConfig = "0"
        End If

        mailAlarm = CInt(mailAlarmConfig)
        smsAlarm = CInt(smsAlarmConfig)

    End Sub

    Function get_addresses()

        If Not My.Computer.FileSystem.FileExists(FILE_MAILS) Then
            IOTextFiles.createFile(FILE_MAILS)
            Return Nothing
        End If

        Dim fileReader As System.IO.StreamReader
        fileReader = My.Computer.FileSystem.OpenTextFileReader(FILE_MAILS)
        Dim addresses As New ArrayList
        While Not fileReader.EndOfStream
            addresses.Add(fileReader.ReadLine().Trim)
        End While
        fileReader.Close()
        Return addresses

    End Function

End Module
