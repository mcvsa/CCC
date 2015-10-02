Imports System.Net.Mail

Module MailConfiguration

    Public SmtpConfig As String
    Public SslConfig As String
    Public PortConfig As String
    Public LoginConfig As String
    Public PasswdConfig As String

    Sub SetMailConfig()
        'Reads mail configuration.
        SmtpConfig = CCC.json.mailServerConfig.smtp
        SslConfig = CCC.json.mailServerConfig.ssl
        PortConfig = CCC.json.mailServerConfig.port
        LoginConfig = CCC.json.mailServerConfig.login
        PasswdConfig = CCC.json.mailServerConfig.password

    End Sub

    Sub saveMailConfig(ByVal SMTPServer, ByVal SSLServer, ByVal portServer, ByVal loginServer, ByVal passwdServer)
        'Guarda les dades de configuració del servidor

        CCC.json.mailServerConfig.smtp = SMTPServer
        CCC.json.mailServerConfig.ssl = SSLServer
        CCC.json.mailServerConfig.port = portServer
        CCC.json.mailServerConfig.login = loginServer
        CCC.json.mailServerConfig.password = passwdServer
        IOTextFiles.updateJsonFile(CCC.json)

    End Sub

    Public Function sendMail(ByVal email As ArrayList, ByVal body As String, ByVal subject As String, ByVal priority As System.Net.Mail.MailPriority)

        Dim message As New MailMessage
        Dim smtp As New SmtpClient
        Dim receiver As String = ""
        IOTextFiles.RoundLog("Comencem a enviar mail")
        SetMailConfig()
        Try
            message.From = New MailAddress(LoginConfig)
            For Each receiver In email
                IOTextFiles.RoundLog("Enviant mail a : " & receiver.ToString)
                If receiver <> "" Then
                    message.To.Clear()
                    message.To.Add(New MailAddress(receiver))
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
                    smtp.Send(message)
                End If
            Next

        Catch ex As Exception
            IOTextFiles.RoundLog("Error mail: " & ex.Message & "-Missatge: " & body & "-Receptors: " & receiver)
            Return ex.Message
        End Try

        Return "OK"

    End Function


    Function comprovaMail(ByRef mail As String)
        'Comprova que la direcció de correu té una mica de sentit. Si no en té retorna "FALSE", i
        'si la direcció es considera correcta retorna "TRUE"
        Dim message As New MailMessage
        Try
            message.To.Add(New MailAddress(mail))
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

End Module
