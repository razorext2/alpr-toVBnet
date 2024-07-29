Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.IO
Imports System.Windows.Forms

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

    Private Sub WriteDirectoryToFile(filePath As String)
        Try
            ' get the directory from the file path
            Dim directoryPath As String = Path.GetDirectoryName(filePath)

            ' speciy the path for the text file where directory will be saved
            Dim outputPath As String = "directory.txt"

            ' write the directory path to the text file
            File.WriteAllText(outputPath, directoryPath)

            MessageBox.Show("Directory saved to file successfully")
        Catch ex As Exception
            MessageBox.Show("error writing to file: " & ex.Message)
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Title = "Select a File"
        openFileDialog.Filter = "All Files (*.*)|*.*"

        ' show the dialog and check if a file was selected
        If openFileDialog.ShowDialog() = DialogResult.OK Then

            ' get the selected file's path
            Dim selectedFilePath As String = openFileDialog.FileName

            ' call the method to write the directory path to the file
            WriteDirectoryToFile(selectedFilePath)
        End If
    End Sub
End Class