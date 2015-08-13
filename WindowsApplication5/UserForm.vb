Imports System.Net.Mail
Imports System
Imports System.IO
Imports System.Text

Public Class UserForm

    Private Sub BtOK_Click(sender As Object, e As EventArgs) Handles BtOK.Click
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

            Dim newUser As New User

            newUser.name = name
            newUser.phone = phone
            newUser.mail = mail
            newUser.activeUser = True

            JsonFile.addUser(newUser)
            ConfigAlerts.updateUsersList()

            Me.Close()
        End If
    End Sub

    Private Sub BtCancel_Click(sender As Object, e As EventArgs) Handles BtCancel.Click
        Me.Close()
    End Sub

    Private Sub UserForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown, TBUserPhone.KeyDown, TBUserName.KeyDown, TBUserMail.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            BtOK_Click(sender, e)
        End If
    End Sub
End Class