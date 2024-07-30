Imports Vlc.DotNet.Forms
Imports System.IO
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Diagnostics

Public Class rtspStream

    Private conn As TcpClient
    Private isConnected As Boolean = False
    Private vlcControl As VlcControl
    Private receivedData As String = String.Empty ' Variable to store received data
    Private process As Process ' Variable to store the Python process

    Private Sub rtspStream_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize VlcControl
        vlcControl = New VlcControl()
        vlcControl.Dock = DockStyle.Fill

        ' Add VlcControl to the PictureBox
        PictureBox1.Controls.Add(vlcControl) ' Ensure PictureBox1 is your PictureBox control
        vlcControl.BringToFront()

        ' Path to VLC executable
        Dim vlcLibPath As String = "C:\Program Files (x86)\VideoLAN\VLC" ' Update this path if VLC is installed in a different location

        ' Initialize VLC player
        vlcControl.BeginInit()
        vlcControl.VlcLibDirectory = New IO.DirectoryInfo(vlcLibPath)
        vlcControl.EndInit()

        btnSave.Visible = False
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Label1.Visible = False
        ' Dim rtspUrl As String = "rtsp://admin:admin123@192.168.11.33:554/sub_stream"
        Dim rtspUrl As String = txtUrl.Text
        If rtspUrl = "" Then
            MessageBox.Show("Masukkan RTSP URL terlebih dahulu!")
        Else
            vlcControl.SetMedia(New Uri(rtspUrl))
            vlcControl.Play()
        End If

    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        Label1.Text = "Stream sedang berhenti"
        Label1.Visible = True
        vlcControl.Stop()
    End Sub

    Private Sub btnCapture_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        Dim captureDir As String = "C:\Users\Abdi\Documents\VS2015\Projects\plateDetector\plateDetector\bin\Debug\captureImg"
        Dim videoPath As String = Path.Combine(captureDir, "capture.jpg")

        ' Ensure the directory exists
        If Not Directory.Exists(captureDir) Then
            Directory.CreateDirectory(captureDir)
        End If

        Try
            ' Capture the current frame and save it
            vlcControl.TakeSnapshot(videoPath)
            MessageBox.Show("Capture saved to " & videoPath)

            ' Write the capture path to fileList.txt
            Dim fileListPath As String = "C:\Users\Abdi\Documents\VS2015\Projects\plateDetector\plateDetector\bin\Debug\fileList.txt"
            File.WriteAllText(fileListPath, videoPath)
        Catch ex As Exception
            MessageBox.Show("Error capturing frame: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDetect_Click(sender As Object, e As EventArgs) Handles btnDetect.Click
        If Not isConnected Then
            ' Connect to the Python script
            Try
                process = New Process()
                process.StartInfo.FileName = "python"
                process.StartInfo.Arguments = "main.py"
                process.StartInfo.UseShellExecute = False
                process.StartInfo.RedirectStandardOutput = True
                process.StartInfo.CreateNoWindow = True
                process.Start()

                conn = New TcpClient("localhost", 9000)
                isConnected = True

                ' Start receiving data
                ReceiveData()
            Catch ex As Exception
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
                btnSave.Visible = True

                If process IsNot Nothing AndAlso Not process.HasExited Then
                    process.Kill()
                End If

                MessageBox.Show("Data received: " & receivedData)
            End If
        Catch ex As Exception
            MessageBox.Show("Error receiving data: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim outputFilePath As String = "C:\Users\Abdi\Documents\VS2015\Projects\plateDetector\plateDetector\bin\Debug\outputList.txt"
            Dim textToSave As String = txtScreen1.Text

            ' Append the new text to the file with a new line
            File.AppendAllText(outputFilePath, textToSave & Environment.NewLine)

            MessageBox.Show("Output saved to " & outputFilePath)
        Catch ex As Exception
            MessageBox.Show("Error saving output: " & ex.Message)
        End Try
    End Sub
End Class
