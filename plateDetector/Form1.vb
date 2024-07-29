Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Class Form1
    Private conn As TcpClient
    Private thread As Thread

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Define the path to the Python executable in the virtual environment
        Dim pythonExe As String = "C:\Program Files\Python36\python.exe"
        ' Define the path to the main script
        Dim scriptPath As String = "C:\Users\Abdi\Documents\VS2015\Projects\plateDetector\Main.py"

        ' Start Python script
        Dim startInfo As New ProcessStartInfo(pythonExe, scriptPath)
        startInfo.WindowStyle = ProcessWindowStyle.Minimized
        Process.Start(startInfo)

        Try
            conn = New TcpClient("localhost", 8186)
            thread = New Thread(AddressOf ReceiveData)
            thread.Start()
            Button1.BackColor = Color.LightGreen
            Label1.Text = "Connected to Python"
            Label1.ForeColor = Color.DarkGreen
        Catch ex As Exception
            Label1.Text = "Failed to Connect to Python: " & ex.Message
            Button1.BackColor = Color.Red
            Label1.ForeColor = Color.Red
        End Try
    End Sub

    Private Sub ReceiveData()
        Try
            Dim bytes(1024) As Byte
            Dim stream As NetworkStream = conn.GetStream()
            While True
                Dim numBytesRead As Integer = stream.Read(bytes, 0, bytes.Length)
                If numBytesRead > 0 Then
                    Dim receivedData As String = Encoding.ASCII.GetString(bytes, 0, numBytesRead).TrimEnd(ControlChars.NullChar)
                    ' Update GUI with received data
                    Invoke(Sub()
                               If Not String.IsNullOrWhiteSpace(receivedData) Then
                                   txtScreen1.Text = receivedData
                               End If
                           End Sub)
                End If
            End While
        Catch ex As Exception
            ' Handle exception
            Label1.Text = "Error receiving data: " & ex.Message
            Label1.ForeColor = Color.Red
        Finally
            ' Ensure connection is closed
            If conn IsNot Nothing AndAlso conn.Connected Then
                conn.Close()
            End If
        End Try
    End Sub

End Class
