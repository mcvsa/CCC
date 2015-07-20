Imports System.Threading

Module SMSConfiguration
    Public ReadOnly SLEEPING_TIME As Integer = 1000 'Per a donar temps al mòdem a respondre
    Public ReadOnly FILE_SMS As String = Form1.STARTUP_PATH & "\resources\sms.list"

    Function openPort(ByVal serialPortName As String, ByRef serialPort As System.IO.Ports.SerialPort)
        Try
            With serialPort
                If .IsOpen Then
                    serialPort.Close()
                End If
                .PortName = serialPortName
                .BaudRate = Form1.VELOCIDADPUERTO
                .DataBits = 8
                .DtrEnable = False
                .StopBits = IO.Ports.StopBits.One
                .Parity = IO.Ports.Parity.None
                .Handshake = IO.Ports.Handshake.RequestToSend
                .WriteBufferSize = 1024 '1024
                .ReadBufferSize = 2048 '2048
                .WriteTimeout = 500
                .Encoding = System.Text.Encoding.Default
                .ReadTimeout = 7000
                .Open()
            End With
        Catch ex As Exception
            Return (ex.Message)
        End Try
        Return ("OK")
    End Function

    Function modeText(ByRef serialPort As System.IO.Ports.SerialPort)
        Dim response As String = ""
        Try
            Form1.RoundLog("30")
            serialPort.Write("AT+CMGF=1" & Chr(13))
            Form1.RoundLog("31")
            While (response <> "OK" & vbCr & "")
                Form1.RoundLog("32")
                response = serialPort.ReadLine
                Form1.RoundLog("Response = " & response)
            End While
        Catch ex As Exception
            Return ex.Message
            Form1.RoundLog(response)
        End Try
        Return ("OK")
    End Function

    Function sendSMS(ByVal phone As String, ByRef serialport As System.IO.Ports.SerialPort, ByRef message As String)
        phone = phone.Replace(".", "")
        Form1.RoundLog("10")
        serialport.DiscardInBuffer()
        serialport.DiscardOutBuffer()
        Form1.RoundLog("11")
        If Form1.comprovaTelefon(phone) = -1 Then
            Return ("Telèfon incorrecte")
        Else
            Form1.RoundLog("12")
            Dim resp As String = ""
            Try
                Form1.RoundLog("13")
                serialport.Write("AT+CMGS=" & Chr(34) & "+34" & phone & Chr(34) & Chr(13))
                Form1.RoundLog("14")
                Thread.Sleep(SLEEPING_TIME)
                'While (resp.IndexOf(">") < 0)
                'resp += serialport.ReadLine
                'End While
                'Thread.Sleep(SLEEPING_TIME)
                serialport.Write(message & vbCrLf & Chr(26))
                resp = ""
                While (resp.IndexOf("OK") < 0)
                    resp += serialport.ReadLine
                    Form1.RoundLog(resp & "-10")
                End While
            Catch ex As Exception
                Form1.RoundLog(resp)
                Return ex.Message
            End Try
            serialport.DiscardInBuffer()
            serialport.DiscardOutBuffer()

            Return "OK"
        End If

    End Function

    Function getPhonesList()

        If Not My.Computer.FileSystem.FileExists(FILE_SMS) Then
            IOTextFiles.createFile(FILE_SMS)
            Return Nothing
        End If

        Dim fileReader As System.IO.StreamReader
        fileReader = My.Computer.FileSystem.OpenTextFileReader(FILE_SMS)
        Dim phones As New List(Of String)
        While Not fileReader.EndOfStream
            phones.Add(fileReader.ReadLine().Trim)
        End While
        fileReader.Close()
        Return phones

    End Function

End Module
