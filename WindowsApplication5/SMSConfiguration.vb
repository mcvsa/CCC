Imports System.Threading

Module SMSConfiguration
    Public ReadOnly SLEEPING_TIME As Integer = 1000 'Per a donar temps al mòdem a respondre

    Function resetModem(serialport)
        'Reset mòdem
        sendToModem(serialPort, "ATZ")
        Dim resp = readFromModem(serialPort, "OK")

        If resp <> "ERROR" Then
            Thread.Sleep(SLEEPING_TIME)
            Return "OK"
        End If
        Return "& ERROR with ATZ command"
    End Function


    Function openPort(ByVal serialPortName As String, ByRef serialPort As System.IO.Ports.SerialPort)
        Try
            IOTextFiles.RoundLog("Obrint port")
            With serialPort
                If .IsOpen Then
                    serialPort.Close()
                End If
                .PortName = serialPortName
                .BaudRate = CCC.VELOCIDADPUERTO
                .DataBits = 8
                .DtrEnable = False
                .StopBits = IO.Ports.StopBits.One
                .Parity = IO.Ports.Parity.None
                .Handshake = IO.Ports.Handshake.RequestToSend
                .WriteBufferSize = 1024 '1024
                .ReadBufferSize = 4096 '2048
                .WriteTimeout = 500
                .Encoding = System.Text.Encoding.Default
                .ReadTimeout = 8000
                .RtsEnable = False
                .Open()
            End With

        Catch ex As Exception
            Return (ex.Message)
        End Try

        Return "OK"
    End Function

    Function sendSMS(ByVal phone As String, ByRef serialport As System.IO.Ports.SerialPort, ByRef message As String)
        IOTextFiles.RoundLog("Enviant SMS al tlf: " & phone)
        phone = phone.Replace(".", "")
        If CCC.comprovaTelefon(phone) = -1 Then
            Return ("Telèfon incorrecte")
        Else
            Dim resp As String = ""
            IOTextFiles.RoundLog("Comencem a enviar SMS amb el text: " & message)
            sendToModem(serialport, "AT+CMGS=" & Chr(34) & "+34" & phone & Chr(34) & Chr(13))
            IOTextFiles.RoundLog("Resposta = " & resp)
            resp = readFromModem(serialport, ">")
            IOTextFiles.RoundLog("resposta modem = " & resp)
            If resp = "ERROR" Then
                IOTextFiles.RoundLog("& Error writing message body")
            End If
            'Thread.Sleep(SLEEPING_TIME)
            sendToModem(serialport, message & vbCrLf & Chr(26))
            resp = readFromModem(serialport, "OK")
            If resp = "ERROR" Then
                IOTextFiles.RoundLog("& Error sending SMS")
                Return resp
            End If
            Return "OK"
        End If

    End Function

    Public Sub sendToModem(ByRef serialport As System.IO.Ports.SerialPort, ByVal dataToSend As String)
        If serialport.IsOpen Then
            Try
                'serialport.DiscardOutBuffer()
                serialport.Write(dataToSend & Chr(13))
                'serialport.DiscardOutBuffer()
            Catch ex As Exception
                IOTextFiles.RoundLog("Error sending: " & dataToSend & "-" & ex.Message)
                'MsgBox(ex.Message, vbCritical)
            End Try
        End If

    End Sub

    Public Function readFromModem(ByRef serialport As System.IO.Ports.SerialPort, ByVal finalChar As String)
        'If serialport.IsOpen Then
        Dim response As String = ""
        Try
            If finalChar = ">" Then
                'No podem fer servir el métode 'ReadLine' perquè el que envia el mòdem és AT... + vbCrLF + >
                'Suposo que el vbCrLf va que el 'ReadLine' pensi que ha acabat de rebre totes les dades.
                response = serialport.ReadExisting
                While (response.IndexOf(">") < 0)
                    response += serialport.ReadExisting
                    If response.IndexOf("ERROR") > 0 Then
                        Throw New System.Exception("Timeout Exception")
                    End If
                End While
            Else
                'El métode ReadExisting dóna problemes i 'timeouts'
                response = serialport.ReadLine

                While (response.IndexOf(finalChar) < 0)
                    response += serialport.ReadLine
                End While
            End If
        Catch ex As Exception
            IOTextFiles.RoundLog("Error receiving " & finalChar & "-" & ex.Message)
            Return "ERROR"
        End Try
        Return response

        'End If
        'Return "ERROR"
    End Function

End Module
