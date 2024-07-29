Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Class Form1

    Private conn As New TcpClient()
    Private thread As Thread

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Shell("cmd.exe /k python main.py")
        Try
            conn.Connect("localhost", 9000)
            thread = New Thread(AddressOf ReceiveData)
            thread.Start()
            Button1.BackColor = Color.LightGreen
            Label1.Text = "Connected to Python"
            Label1.ForeColor = Color.DarkGreen
        Catch ex As Exception
            Label1.Text = "Failed to Connected to Python"
            Button1.BackColor = Color.Red
            Label1.ForeColor = Color.Red
        End Try
    End Sub

    Private Sub ReceiveData()
        While True
            Try
                ' Menerima data dari server
                Dim bytes(conn.ReceiveBufferSize) As Byte
                Dim stream As NetworkStream = conn.GetStream()
                stream.Read(bytes, 0, conn.ReceiveBufferSize)
                Dim receivedData As String = Encoding.ASCII.GetString(bytes).TrimEnd(ControlChars.NullChar)

                ' Memperbarui GUI dengan data yang diterima
                Invoke(Sub()
                           txtScreen1.Text = receivedData.Split(",")(0)
                       End Sub)
            Catch ex As Exception
                ' Mengatasi exception
                Exit While
            End Try
        End While
        ' Menutup Koneksi
        conn.Close()
    End Sub
End Class