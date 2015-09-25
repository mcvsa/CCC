Imports System
Imports System.IO
Imports System.Text
Imports Newtonsoft.Json

Module IOTextFiles
    'Create a file.

    Public Sub createFile(ByVal fileName As String)
        Dim fs As FileStream = File.Create(fileName)
        fs.Close()
    End Sub

    Public Sub writeFile(ByVal file2write As String, ByVal text2write As String)
        'Overwrites a file if it exists, if it doesn't creates the file and overwrites it.

        If Not My.Computer.FileSystem.FileExists(file2write) Then
            createFile(file2write)
        End If
        Dim done As Boolean = False
        While done = False
            Try
                My.Computer.FileSystem.WriteAllText(file2write, text2write, False)
                done = True
            Catch ex As Exception
                Continue While
            End Try
        End while

    End Sub

    Public Sub updateJsonFile(ByVal json As Object)
        Dim jsonStr As String = JsonConvert.SerializeObject(json)
        writeFile(CCC.SETTINGSFILE, jsonStr)
    End Sub

End Module
