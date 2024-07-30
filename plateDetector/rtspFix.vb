Imports Vlc.DotNet.Forms
Imports System.IO
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Diagnostics

Public Class rtspFix

    Private conn As TcpClient
    Private isConnected As Boolean = False
    Private vlcControl As VlcControl
    Private receivedData As String = String.Empty ' Variable to store received data
    Private process As Process ' Variable to store the Python process
    Private currentState As String = "stopped" ' To track the current state of the application

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
        btnCapture.Text = "Start" ' Initial state
        PictureBox2.BackColor = Color.Black

    End Sub

    Private Sub btnCapture_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        If currentState = "stopped" Then
            StartAndDetect()
        Else
            StopStream()
        End If
    End Sub

    Private Async Sub StartAndDetect()
        Label1.Text = "Stream Terkoneksi..."
        Label1.ForeColor = Color.Green
        Dim rtspUrl As String = txtUrl.Text
        If rtspUrl = "" Then
            MessageBox.Show("Masukkan RTSP URL terlebih dahulu!")
        Else
            ' Start streaming
            vlcControl.SetMedia(New Uri(rtspUrl))
            vlcControl.Play()

            ' Wait for the stream to stabilize
            Await Task.Delay(3000) ' Wait for 3 seconds

            ' Capture image and detect plate
            CaptureAndDetect()

            ' Update state
            currentState = "started"
            btnCapture.Text = "Stop"
            btnCapture.BackColor = Color.MediumSpringGreen
        End If
    End Sub

    Private Sub CaptureAndDetect()
        ' Capture image
        Dim captureDir As String = "C:\Users\Abdi\Documents\VS2015\Projects\plateDetector\plateDetector\bin\Debug\captureImg"
        Dim timestamp As String = DateTime.Now.ToString("yyyyMMdd_HHmmss")
        Dim videoPath As String = Path.Combine(captureDir, $"capture_{timestamp}.jpg")

        ' Ensure the directory exists
        If Not Directory.Exists(captureDir) Then
            Directory.CreateDirectory(captureDir)
        End If

        Try
            ' Capture the current frame and save it
            vlcControl.TakeSnapshot(videoPath)
            ' MessageBox.Show("Capture saved to " & videoPath)
            MessageBox.Show("Sedang mendeteksi...")

            ' Load the captured image into PictureBox2
            PictureBox2.Image = Image.FromFile(videoPath)
            PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage ' Adjust image size mode as needed

            ' Write the capture path to fileList.txt (overwrite)
            Dim fileListPath As String = "C:\Users\Abdi\Documents\VS2015\Projects\plateDetector\plateDetector\bin\Debug\fileList.txt"
            File.WriteAllText(fileListPath, videoPath & Environment.NewLine)

            ' Detect plate
            DetectPlate()
        Catch ex As Exception
            MessageBox.Show("Error capturing frame: " & ex.Message)
        End Try
    End Sub

    Private Sub DetectPlate()
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

                MessageBox.Show("Plate terdeteksi: " & receivedData)
            End If
        Catch ex As Exception
            MessageBox.Show("Error receiving data: " & ex.Message)
        End Try
    End Sub

    Private Sub StopStream()
        vlcControl.Stop()
        Label1.Text = "Stream sedang berhenti"
        Label1.Visible = True
        currentState = "stopped"
        btnCapture.Text = "Start"
        PictureBox2.Image = Nothing
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
