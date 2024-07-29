Imports AForge.Video
Imports AForge.Video.DirectShow
Imports System.IO
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Diagnostics

Public Class cameraInput

    Private conn As TcpClient
    Private isConnected As Boolean = False
    Private videoSource As VideoCaptureDevice
    Private filterInfoCollection As FilterInfoCollection
    Private process As Process ' Variable to store the Python process
    Private receivedData As String = String.Empty ' Variable to store received data
    Private receiveThread As Thread ' Renamed thread variable

    Private Sub cameraInput_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Get available video devices
        filterInfoCollection = New FilterInfoCollection(FilterCategory.VideoInputDevice)
        If filterInfoCollection.Count = 0 Then
            MessageBox.Show("No video devices found.")
            Return
        End If

        ' Initialize video source with the first available device
        videoSource = New VideoCaptureDevice(filterInfoCollection(0).MonikerString)
        AddHandler videoSource.NewFrame, AddressOf VideoSource_NewFrame

        btnSave.Visible = False
    End Sub

    Private Sub VideoSource_NewFrame(sender As Object, e As NewFrameEventArgs)
        ' Display the video frame in PictureBox
        If PictureBox1.InvokeRequired Then
            PictureBox1.Invoke(Sub()
                                   PictureBox1.Image = e.Frame.Clone()
                               End Sub)
        Else
            PictureBox1.Image = e.Frame.Clone()
        End If
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If videoSource IsNot Nothing AndAlso Not videoSource.IsRunning Then
            videoSource.Start()
            Label1.Visible = False
        Else
            MessageBox.Show("No video source available or video source is already running.")
        End If
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        If videoSource IsNot Nothing AndAlso videoSource.IsRunning Then
            videoSource.SignalToStop()
            videoSource.WaitForStop()
            Label1.Text = "Stream sedang berhenti"
            Label1.Visible = True
        End If
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
            If PictureBox1.Image IsNot Nothing Then
                Dim currentFrame As Bitmap = CType(PictureBox1.Image, Bitmap)
                currentFrame.Save(videoPath, Imaging.ImageFormat.Jpeg)
                MessageBox.Show("Capture saved to " & videoPath)

                ' Write the capture path to fileList.txt
                Dim fileListPath As String = "C:\Users\Abdi\Documents\VS2015\Projects\plateDetector\plateDetector\bin\Debug\fileList.txt"
                File.WriteAllText(fileListPath, videoPath)
            Else
                MessageBox.Show("No image to capture.")
            End If
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
                receiveThread = New Thread(AddressOf ReceiveData)
                receiveThread.Start()
            Catch ex As Exception
                MessageBox.Show("Failed to Connect to Python: " & ex.Message)
            End Try
        Else
            ' Disconnect from the Python script
            Try
                If conn.Connected Then
                    conn.Close()
                End If
                If receiveThread IsNot Nothing AndAlso receiveThread.IsAlive Then
                    receiveThread.Abort()
                End If
                btnDetect.BackColor = Color.LightGray
                Label1.Text = "Disconnected from Python"
                Label1.ForeColor = Color.Gray
                isConnected = False
            Catch ex As Exception
                MessageBox.Show("Error disconnecting: " & ex.Message)
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
                txtScreen1.Invoke(Sub()
                                      txtScreen1.Text = receivedData
                                  End Sub)

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
