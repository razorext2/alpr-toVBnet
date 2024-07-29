Imports Vlc.DotNet.Forms
Imports System.IO
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Class rtspStream

    Private conn As New TcpClient()
    Private thread As Thread
    Private isConnected As Boolean = False
    Private vlcControl As VlcControl

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
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Label1.Visible = False
        Dim rtspUrl As String = "rtsp://admin:admin123@192.168.11.33:554/sub_stream"
        vlcControl.SetMedia(New Uri(rtspUrl))
        vlcControl.Play()
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

            MessageBox.Show("File path saved to " & fileListPath)
        Catch ex As Exception
            MessageBox.Show("Error capturing frame: " & ex.Message)
        End Try
    End Sub


    Private Sub btnDetect_Click(sender As Object, e As EventArgs) Handles btnDetect.Click
        If Not isConnected Then
            ' Connect to the Python script
            Try
                Shell("cmd.exe /k python main.py")
                conn.Connect("localhost", 9000)
                thread = New Thread(AddressOf ReceiveData)
                thread.Start()
                btnDetect.BackColor = Color.LightGreen
                Label1.Text = "Connected to Python"
                Label1.ForeColor = Color.DarkGreen
                isConnected = True
            Catch ex As Exception
                Label1.Text = "Failed to Connect to Python"
                btnDetect.BackColor = Color.Red
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

End Class
