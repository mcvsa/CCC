Imports System
Imports System.IO
Imports System.Text

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
        My.Computer.FileSystem.WriteAllText(file2write, text2write, False)
    End Sub

End Module
