Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.IO
Imports System.Windows.Forms

Public Class Form1

    Private conn As TcpClient
    Private thread As Thread
    Private isConnected As Boolean = False
    Private process As Process ' Variable to store the Python process
    Private receivedData As String = String.Empty ' Variable to store received data

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not isConnected Then
            ' Connect to the Python script
            Try
                process = New Process()
                process.StartInfo.FileName = "python"
                If ComboBox1.Text = "Hitam" Then
                    process.StartInfo.Arguments = "main.py"
                ElseIf ComboBox1.Text = "Putih" Then
                    MessageBox.Show("Putih")
                ElseIf ComboBox1.Text = "Kuning" Then
                    MessageBox.Show("Kuning")
                Else
                    MessageBox.Show("Pilih tipe plat")
                End If
                process.StartInfo.UseShellExecute = False
                process.StartInfo.RedirectStandardOutput = True
                process.StartInfo.CreateNoWindow = True
                process.Start()

                conn = New TcpClient("localhost", 9000)
                isConnected = True

                Label1.Text = "Terhubung ke python"
                Label1.ForeColor = Color.Green

                ' Start receiving data
                ReceiveData()
            Catch ex As Exception
                MessageBox.Show("Failed to Connect to Python: " & ex.Message)
                Label1.Text = "Gagal terhubung ke python"
                Label1.ForeColor = Color.Red
            End Try
        End If
    End Sub

    Private Sub WritePathToFile(filePath As String)
        Try
            ' Replace backslashes with forward slashes
            Dim formattedPath As String = filePath.Replace("\", "/")

            ' Specify the path for the text file where the formatted file path will be saved
            Dim outputPath As String = "fileList.txt"

            ' Write the formatted file path to the text file
            File.WriteAllText(outputPath, formattedPath)

            ' Update the text box with the formatted file path
            txtDirectory.Text = formattedPath

            MessageBox.Show("File path saved to file successfully")
        Catch ex As Exception
            MessageBox.Show("Error writing to file: " & ex.Message)
        End Try
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

                If process IsNot Nothing AndAlso Not process.HasExited Then
                    process.Kill()
                End If

                MessageBox.Show("Data received: " & receivedData)
            End If
        Catch ex As Exception
            MessageBox.Show("Error receiving data: " & ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Title = "Select an Image File"
        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;"

        ' Show the dialog and check if a file was selected
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Get the selected file's path
            Dim selectedFilePath As String = openFileDialog.FileName

            ' Call the method to write the directory and file name to the file
            WritePathToFile(selectedFilePath)

            ' Send the image path to the Python script
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Text = "-- Pilih tipe plat --"
    End Sub
End Class
