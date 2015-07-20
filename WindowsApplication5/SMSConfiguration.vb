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
                .WriteBufferSize = 1024
                .ReadBufferSize = 2048
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
        Try
            serialPort.Write("AT+CMGF=1" & Chr(13))
            Dim response = serialPort.ReadLine.ToString

            While (response <> "OK" & vbCr & "")
                response = serialPort.ReadLine.ToString
            End While
        Catch ex As Exception
            Return ex.Message
        End Try
        Return ("OK")
    End Function

    Function sendSMS(ByVal phone As String, ByRef serialport As System.IO.Ports.SerialPort, ByRef message As String)
        phone = phone.Replace(".", "")
        If Form1.comprovaTelefon(phone) = -1 Then
            Return ("Telèfon incorrecte")
        Else
            Dim resp As String = ""
            Try
                If serialport.IsOpen Then
                    serialport.Write("AT+CMGS=" & Chr(34) & "+34" & phone & Chr(34) & Chr(13))
                    Thread.Sleep(SLEEPING_TIME)
                    serialport.Write(message & vbCrLf & Chr(26))
                    Thread.Sleep(SLEEPING_TIME)
                    resp = ""
                End If
                While (resp.IndexOf("OK") < 0 And serialport.BytesToRead > 0)
                    resp = serialport.ReadExisting
                End While
            Catch ex As Exception
                Return ex.Message
            End Try
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
