Imports System
Imports System.IO
Imports System.Text
Imports Newtonsoft.Json

Module IOTextFiles
    Dim writingLock As New Object
    Dim loggingLock As New Object
    Dim updatingLock As New Object

    Const MAXLOGENTRIES As Integer = 1000 'Número màxim de línies al log.

    Public Sub createFile(ByVal fileName As String)
        'Create a file.
        Dim fs As FileStream = File.Create(fileName)
        fs.Close()
    End Sub

    Public Sub writeFile(ByVal file2write As String, ByVal text2write As String)
        'Overwrites a file if it exists, if it doesn't creates the file and overwrites it.

        If Not My.Computer.FileSystem.FileExists(file2write) Then
            createFile(file2write)
        End If
        SyncLock (writingLock)
            My.Computer.FileSystem.WriteAllText(file2write, text2write, False)
        End SyncLock

    End Sub

    Public Sub updateJsonFile(ByVal json As Object)
        SyncLock (updatingLock)
            Dim jsonStr As String = JsonConvert.SerializeObject(json)
            writeFile(CCC.SETTINGSFILE, jsonStr)
        End SyncLock
    End Sub

    Sub RoundLog(ByVal message As String)
        'Guarda missatges d'error al log (amb marca de temps). Serà un log de com a màxim 100 línies i
        'quan arribem a les 100 línies copiarem el log a log.txt.1 i començarem de zero el nou log.
        Dim marcaTemps As Date
        marcaTemps = Now

        message = marcaTemps & "-" & message

        If Not My.Computer.FileSystem.FileExists(CCC.LOG) Then
            createFile(CCC.LOG)
        End If
        SyncLock (loggingLock)
            Dim logLines() As String = File.ReadAllLines(CCC.LOG)

            If logLines.Length >= MAXLOGENTRIES Then
                'Copia el que hi ha al fitxer a log1.txt.1 (i matxaca el que hi havia prèviament)
                System.IO.File.Copy(CCC.LOG, CCC.LOG1, True)

                'Esborra el log inicial i el torna a crear per a poder continuar afegint-hi línies
                System.IO.File.Delete(CCC.LOG)
                'System.IO.File.Create(LOG)
                createFile(CCC.LOG)
            End If
            My.Computer.FileSystem.WriteAllText(CCC.LOG, message & vbCrLf, True)
        End SyncLock

    End Sub
End Module
