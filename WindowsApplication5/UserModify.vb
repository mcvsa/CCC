Imports System.Net.Mail

Public Class UserModify

    Private Sub BtOK_Click(sender As Object, e As EventArgs) Handles BtOK.Click, MyBase.Enter

        Dim phone = TBUserPhone.Text.Trim
        Dim mail = TBUserMail.Text.Trim
        Dim name = TBUserName.Text.Trim

        If name <> "" Then
            If phone <> "" Then
                correcte = CCC.comprovaTelefon(phone)
                If correcte = -1 Then
                    MsgBox("El telèfon que heu introduit no és correcte", vbOKOnly)
                    Exit Sub
                End If
            End If

            If mail <> "" Then
                If MailConfiguration.comprovaMail(mail) = False Then
                    MsgBox("Direcció de correu invàlida", vbOKOnly)
                End If
            End If

            If ConfigAlerts.idToModify >= 0 Then
                JsonFile.setName(ConfigAlerts.idToModify, name)
                JsonFile.setPhone(ConfigAlerts.idToModify, phone)
                JsonFile.setMail(ConfigAlerts.idToModify, mail)
            End If

            ConfigAlerts.updateUsersList()
            Me.Close()
        End If

    End Sub

    Private Sub BtCancel_Click(sender As Object, e As EventArgs) Handles BtCancel.Click
        Me.Close()
    End Sub

    Private Sub UserModify_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown, TBUserPhone.KeyDown, TBUserName.KeyDown, TBUserMail.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class