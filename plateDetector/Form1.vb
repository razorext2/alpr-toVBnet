Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.IO
Imports System.Windows.Forms

Public Class Form1

    Private conn As New TcpClient()
    Private thread As Thread
    Private isConnected As Boolean = False

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not isConnected Then
            ' Connect to the Python script
            Try
                Shell("cmd.exe /k python main.py")
                conn.Connect("localhost", 9000)
                thread = New Thread(AddressOf ReceiveData)
                thread.Start()
                Button1.BackColor = Color.LightGreen
                Label1.Text = "Connected to Python"
                Label1.ForeColor = Color.DarkGreen
                isConnected = True
            Catch ex As Exception
                Label1.Text = "Failed to Connect to Python"
                Button1.BackColor = Color.Red
                Label1.ForeColor = Color.Red
            End Try
        Else
            ' Disconnect from the Python script
            Try
                If conn.Connected Then
                    conn.Close()
                End If
                If thread IsNot Nothing AndAlso thread.IsAlive Then
                    thread.Abort()
                End If
                Button1.BackColor = Color.LightGray
                Label1.Text = "Disconnected from Python"
                Label1.ForeColor = Color.Gray
                isConnected = False
            Catch ex As Exception
                MessageBox.Show("Error disconnecting: " & ex.Message)
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
        While True
            Try
                ' Receive data from server
                Dim bytes(conn.ReceiveBufferSize) As Byte
                Dim stream As NetworkStream = conn.GetStream()
                stream.Read(bytes, 0, conn.ReceiveBufferSize)
                Dim receivedData As String = Encoding.ASCII.GetString(bytes).TrimEnd(ControlChars.NullChar)

                ' Update GUI with the received data
                Invoke(Sub()
                           txtScreen1.Text = receivedData.Split(",")(0)
                       End Sub)
            Catch ex As Exception
                ' Handle exception
                Exit While
            End Try
        End While
        ' Close connection
        conn.Close()
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
        End If
    End Sub

End Class
