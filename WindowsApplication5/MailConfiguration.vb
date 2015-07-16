Imports System.Net.Mail

Module MailConfiguration

    Public ReadOnly SMTP As String = "smtp.gmail.com"
    Public ReadOnly SSL As String = "ON"
    Public ReadOnly PORT As String = "587"
    Public ReadOnly LOGIN As String = "centrecontrolcaptadors@gmail.com"
    Public ReadOnly PASSWORD As String = "CCCmcvsa"

    Public ReadOnly DEFAULTMAILALARM As String = "0"
    Public ReadOnly DEFAULTSMSALARM As String = "0"
    Public ReadOnly DEFAULTCONFIGS As String = "MAIL=" & DEFAULTMAILALARM & vbCrLf & "SMS=" & DEFAULTSMSALARM

    Public ReadOnly MAIL_CONFIG As String = Form1.STARTUP_PATH & "\resources\mail.conf"
    Public ReadOnly ALARM_CONFIGS As String = Form1.STARTUP_PATH & "\resources\settings.conf"
    Public ReadOnly FILE_MAILS As String = Form1.STARTUP_PATH & "\resources\mail.list"

    Public SmtpConfig As String
    Public SslConfig As String
    Public PortConfig As String
    Public LoginConfig As String
    Public PasswdConfig As String

    Public mailAlarm As Integer
    Public smsAlarm As Integer

    Sub ReadConfigMail()
        'Reads mail configuration. I it is wrong or it doesn't exists, sets the default configuration

        If My.Computer.FileSystem.FileExists(MAIL_CONFIG) Then
            Dim fileReader As System.IO.StreamReader
            fileReader = My.Computer.FileSystem.OpenTextFileReader(MAIL_CONFIG)
            Dim stringReader As New List(Of String)
            While Not fileReader.EndOfStream
                stringReader.Add(fileReader.ReadLine().Trim)
            End While
            fileReader.Close()

            If stringReader.Count <> 5 Then
                Form1.RoundLog("Error llegint dades configuració mail")
                ConfigDefaults()
            Else
                If stringReader(1) <> "ON" And stringReader(1) <> "OFF" Then
                    Form1.RoundLog("Error llegint dades configuració mail (ON/OFF)")
                    ConfigDefaults()
                Else
                    SetConfigMail(stringReader)
                End If
            End If
        Else
            ConfigDefaults()
        End If

    End Sub

    Sub ConfigDefaults()

        loadDefaultAlarmConfigs()

        Dim stringReader As New List(Of String)
        FormConfigMail.Defaults()
        Dim fileReader = My.Computer.FileSystem.OpenTextFileReader(MAIL_CONFIG)
        stringReader.Clear()
        While Not fileReader.EndOfStream
            stringReader.Add(fileReader.ReadLine().Trim)
        End While
        fileReader.Close()

        SetConfigMail(stringReader)

    End Sub

    Sub SetConfigMail(ByRef stringConfs)
        'Sets mail configuration in the variables

        SmtpConfig = stringConfs(0)
        SslConfig = stringConfs(1)
        PortConfig = stringConfs(2)
        LoginConfig = stringConfs(3)
        PasswdConfig = stringConfs(4)
    End Sub

    Sub loadDefaultAlarmConfigs()
        'Default configuration: won't send mails or SMS
        IOTextFiles.writeFile(ALARM_CONFIGS, DEFAULTCONFIGS)

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

        Dim text2write As String = "MAIL=" & strMail & vbCrLf & "SMS=" & strSMS
        IOTextFiles.writeFile(ALARM_CONFIGS, text2write)

    End Sub

    Public Function sendMail(ByVal email As ArrayList, ByVal body As String, ByVal subject As String, ByVal priority As System.Net.Mail.MailPriority)

        Dim message As New MailMessage
        Dim smtp As New SmtpClient
        Dim receiver As String
        ReadConfigMail()

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
        If Not My.Computer.FileSystem.FileExists(ALARM_CONFIGS) Then
            loadDefaultAlarmConfigs()
            mailAlarm = "0"
            smsAlarm = "0"
        Else
            Dim stReader = My.Computer.FileSystem.OpenTextFileReader(ALARM_CONFIGS)

            While Not stReader.EndOfStream
                Dim linea = stReader.ReadLine
                If linea.IndexOf("MAIL=") <> -1 Then
                    mailAlarm = linea.Substring(linea.IndexOf("=") + 1)
                ElseIf linea.IndexOf("SMS=") <> -1 Then
                    smsAlarm = linea.Substring(linea.IndexOf("=") + 1)
                End If
            End While
            stReader.Close()

        End If

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
