Public Class mainMenu
    Private Sub btnRTSP_Click(sender As Object, e As EventArgs) Handles btnRTSP.Click
        btnRTSP.BackColor = Color.DarkMagenta
        btnUpload.BackColor = Color.SteelBlue
        Button1.BackColor = Color.SteelBlue
        Button2.BackColor = Color.SteelBlue
        rtspStream.Show()
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        btnRTSP.BackColor = Color.SteelBlue
        btnUpload.BackColor = Color.DarkMagenta
        Button1.BackColor = Color.SteelBlue
        Button2.BackColor = Color.SteelBlue
        Form1.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        btnRTSP.BackColor = Color.SteelBlue
        btnUpload.BackColor = Color.SteelBlue
        Button1.BackColor = Color.DarkMagenta
        Button2.BackColor = Color.SteelBlue
        Form2.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        btnRTSP.BackColor = Color.SteelBlue
        btnUpload.BackColor = Color.SteelBlue
        Button1.BackColor = Color.SteelBlue
        Button2.BackColor = Color.DarkMagenta
    End Sub
End Class