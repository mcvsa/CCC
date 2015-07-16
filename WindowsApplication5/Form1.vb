Public Class Form1
    Dim captadors As New List(Of Captador)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
       
    End Sub
    Sub actualitzaListCaptadors()
        'Actualitza el llistat de captadors a ListCaptadors

        Dim captador As New Captador
        ListCaptadors.Items.Clear()
        For Each captador In captadors
            ListCaptadors.Items.Add(captador.Nom)
        Next
        ListCaptadors.Refresh()
    End Sub
    Function buscaCaptador(ByRef nom As String)
        'Busca el captador que tingui per nom el paràmetre nom

        Dim captador As New Captador

        For Each captador In captadors
            If captador.Nom = nom Then
                Return captadors.IndexOf(captador)
            End If
        Next
        Return (-1)

    End Function
    Private Sub BtNom_Click(sender As Object, e As EventArgs) Handles BtNom.Click
        Dim nouNom As String
        Dim indexCaptador As Integer
        Dim captador As New Captador

        If ListCaptadors.SelectedItems.Count = 1 Then
            nouNom = InputBox("Quin nom voleu pel captador?")
            If nouNom <> "" Then
                indexCaptador = buscaCaptador(nouNom)
                If indexCaptador = -1 Then
                    captador = captadors(buscaCaptador(ListCaptadors.Text))
                    captador.Nom = nouNom
                    actualitzaListCaptadors()
                Else
                    MsgBox("Aquest captador ja existeix", vbOK)
                End If
            End If
        End If
    End Sub

    Private Sub BtTelefon_Click(sender As Object, e As EventArgs) Handles BtTelefon.Click
        Dim nouTelefon As String
        Dim captador As New Captador

        If ListCaptadors.SelectedItems.Count = 1 Then
            nouTelefon = InputBox("Nou número de telèfon associat al captador:")
            If nouTelefon <> "" Then
                captador = captadors(buscaCaptador(ListCaptadors.Text))
                captador.Telefon = nouTelefon
                LbTelefon.Text = captador.Telefon
            End If
        End If
    End Sub

    Private Sub CbActiu_CheckedChanged(sender As Object, e As EventArgs) Handles CbActiu.CheckedChanged

    End Sub

    Private Sub BtAdd_Click(sender As Object, e As EventArgs) Handles BtAdd.Click
        'Afegeix un captador a la llista dels captadors i al vector de captadors, sempre que no hi sigui

        Dim nom As String
        Dim telefon As String
        Dim captador As New Captador

        nom = InputBox("Nom del nou captador")

        If buscaCaptador(nom) = -1 Then
            telefon = InputBox("Telefon del nou captador")
            captador.Nom = nom
            captador.Telefon = telefon
            captador.Actiu = True

            captadors.Add(captador)
            ListCaptadors.Items.Add(captador.Nom)
        Else
            MsgBox("El Captador ja existeix", vbOKOnly)
        End If

    End Sub

    Private Sub ListCaptadors_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListCaptadors.SelectedIndexChanged
        'Mostra el telèfon del captador seleccionat per l'usuari a la llista dels captadors

        Dim nomCaptador As String
        Dim indexCaptador As Integer
        Dim captador As New Captador

        With ListCaptadors
            nomCaptador = .Text
        End With

        indexCaptador = buscaCaptador(nomCaptador)
        LbTelefon.Text = captadors(indexCaptador).Telefon

    End Sub
End Class

Public Class Captador
    'Classe captador: nom, telèfon associat i estat (actiu o no)

    Public Nom As String
    Public Telefon As String
    Public Actiu As Boolean = False
End Class