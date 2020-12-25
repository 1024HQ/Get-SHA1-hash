Imports System.IO
Imports System.Net
Imports System.Net.WebClient
Imports System.ComponentModel
Imports System.Security.Principal
Imports System.IO.Compression
Imports Microsoft.VisualBasic.CompilerServices
Imports System.Security.Cryptography
Imports System.Text
Imports System.Web.Script.Serialization

Public Class Form1
    Dim cookie As Integer = 1
    WithEvents webclient1 As New WebClient
    Dim appPath As String = Application.StartupPath()
    'HASH CHECKED
    Private Function ByteArrayToString(ByVal arrInput() As Byte) As String
        Dim sb As New System.Text.StringBuilder(arrInput.Length * 2)
        For i As Integer = 0 To arrInput.Length - 1
            sb.Append(arrInput(i).ToString("X2"))
        Next
        Return sb.ToString().ToLower
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "x_pathfileforsend"
        openFileDialog1.Filter = "0m files (*.0m)|*.0m|jmd files (*.jmd)|*.jmd|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 5
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            TextBox1.Text = openFileDialog1.FileName
        End If
        openFileDialog1.Dispose()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'status server
        Try
            Dim guycity As New SHA1Managed ' SHA1Managed / SHA256Managed
            Dim fileBytes() As Byte = IO.File.ReadAllBytes(TextBox1.Text)
            Dim hash() As Byte = guycity.ComputeHash(fileBytes)


            Dim calculatedHash As String = ByteArrayToString(hash)
            'MsgBox(calculatedHash = referenceHash) 'outputs True
            'MsgBox(calculatedHash, MsgBoxStyle.Information, "test")
            TextBox3.Text = calculatedHash
        Catch ex As Exception
            MsgBox("ห้ามว่างเปล่า", MsgBoxStyle.Information, "แจ้งเตือน")
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Clipboard.SetText(TextBox3.Text)
            MsgBox("copy success", MsgBoxStyle.Information, "แจ้งเตือน")
        Catch ex As Exception
            MsgBox("ห้ามว่างเปล่า", MsgBoxStyle.Information, "แจ้งเตือน")
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim uriString As String = TextBox2.Text
        Dim uri As New Uri(uriString)
        'make http
        Dim Request As HttpWebRequest = HttpWebRequest.Create(uri)
        Request.Method = "GET"
        'get http
        Dim Response As HttpWebResponse = Request.GetResponse()
        'read http
        Dim Read = New StreamReader(Response.GetResponseStream())
        Dim Raw As String = Read.ReadToEnd()

        Dim dict As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(Raw)

        For Each item As Object In dict
            Dim cookie2, cookie3, cookie4 As String
            cookie2 = item("ChecksumHash").ToString
            cookie3 = item("File").ToString
            cookie4 = item("Download").ToString
            MsgBox(cookie2 & vbCrLf & cookie3 & vbCrLf & cookie4, MsgBoxStyle.Information, "TEST")

        Next




    End Sub

   
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'guycitycyberpunk += 1
        'Label3.Text = guycitycyberpunk
    End Sub
End Class
