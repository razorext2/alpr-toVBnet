Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.IO
Imports System.Windows.Forms

Public Class Form2

    Private conn As New TcpClient()
    Private thread As Thread
    Private isConnected As Boolean = False
    Private receivedData As String = String.Empty ' Variable to store received data
    Private process As Process ' Variable to store the Python process

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not isConnected Then
            ' Connect to the Python script
            Try
                Process = New Process()
                Process.StartInfo.FileName = "python"
                Process.StartInfo.Arguments = "main.py"
                Process.StartInfo.UseShellExecute = False
                Process.StartInfo.RedirectStandardOutput = True
                Process.StartInfo.CreateNoWindow = True
                Process.Start()

                conn = New TcpClient("localhost", 9000)
                isConnected = True
                Label1.Text = "Terhubung ke python"
                Label1.ForeColor = Color.Green
                ' Start receiving data
                ReceiveData()
            Catch ex As Exception
                Label1.Text = "Gagal terhubung ke python"
                Label1.ForeColor = Color.Red
                MessageBox.Show("Failed to Connect to Python: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub ReceiveData()
        Try
            Dim stream As NetworkStream = conn.GetStream()
            Dim buffer(1024) As Byte

            ' Read the data from the stream
            Dim bytesRead As Integer = stream.Read(buffer, 0, buffer.Length)
            If bytesRead > 0 Then
                receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead).TrimEnd(ControlChars.NullChar)
                txtScreen1.Text = receivedData

                ' Close the connection and process after receiving data
                conn.Close()
                isConnected = False

                If Process IsNot Nothing AndAlso Not Process.HasExited Then
                    Process.Kill()
                End If

                MessageBox.Show("Data received: " & receivedData)
            End If
        Catch ex As Exception
            MessageBox.Show("Error receiving data: " & ex.Message)
        End Try
    End Sub

End Class
