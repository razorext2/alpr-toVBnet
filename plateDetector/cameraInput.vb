Imports AForge.Video
Imports AForge.Video.DirectShow
Imports System.IO
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Diagnostics

Public Class cameraInput

    Dim camera As VideoCaptureDevice
    Dim bmp As Bitmap
    Dim conn As TcpClient
    Dim process As Process
    Dim receiveThread As Thread
    Dim isConnected As Boolean = False
    Private receivedData As String = String.Empty ' Variable to store received data

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Dim cameras As New VideoCaptureDeviceForm()

        If cameras.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            ' Initialize the camera
            camera = cameras.VideoDevice
            AddHandler camera.NewFrame, AddressOf Captured

            Try
                camera.Start()
                MessageBox.Show("Camera started successfully.")
            Catch ex As Exception
                MessageBox.Show("Error starting camera: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        If camera IsNot Nothing AndAlso camera.IsRunning Then
            Try
                camera.SignalToStop()
                camera.WaitForStop()
                MessageBox.Show("Camera stopped successfully.")
            Catch ex As Exception
                MessageBox.Show("Error stopping camera: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub Captured(sender As Object, e As NewFrameEventArgs)
        ' Dispose of old bitmap if it exists
        If bmp IsNot Nothing Then
            bmp.Dispose()
        End If

        ' Create a new bitmap and set it to PictureBox
        bmp = DirectCast(e.Frame.Clone(), Bitmap)
        PictureBox1.Image = bmp
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        ' Ensure camera is stopped and resources are released
        If camera IsNot Nothing AndAlso camera.IsRunning Then
            camera.SignalToStop()
            camera.WaitForStop()
        End If

        ' Dispose of bitmap if it exists
        If bmp IsNot Nothing Then
            bmp.Dispose()
        End If

        MyBase.OnFormClosing(e)
    End Sub

    Private Sub btnCapture_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        ' Define the directory and file path for saving the captured image
        Dim captureDir As String = "C:\Users\Abdi\Documents\VS2015\Projects\plateDetector\plateDetector\bin\Debug\captureImg"
        Dim videoPath As String = Path.Combine(captureDir, "capture.jpg")

        ' Ensure the directory exists
        If Not Directory.Exists(captureDir) Then
            Directory.CreateDirectory(captureDir)
        End If

        Try
            ' Check if PictureBox contains an image
            If PictureBox1.Image IsNot Nothing Then
                ' Capture the current frame from PictureBox and save it
                Dim currentFrame As Bitmap = DirectCast(PictureBox1.Image, Bitmap)
                currentFrame.Save(videoPath, Imaging.ImageFormat.Jpeg)
                MessageBox.Show("Capture saved to " & videoPath)
            Else
                MessageBox.Show("No image to capture.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error capturing frame: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDetect_Click(sender As Object, e As EventArgs) Handles btnDetect.Click
        btnSave.Visible = True
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

    Private Sub cameraInput_Load(sender As Object, e As EventArgs) Handles Me.Load
        btnSave.Visible = False
    End Sub
End Class
