﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rtspStream
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnCapture = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnDetect = New System.Windows.Forms.Button()
        Me.txtScreen1 = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtUrl = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStart.Location = New System.Drawing.Point(12, 405)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(84, 26)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStop.Location = New System.Drawing.Point(112, 405)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(84, 26)
        Me.btnStop.TabIndex = 1
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnCapture
        '
        Me.btnCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCapture.Location = New System.Drawing.Point(213, 405)
        Me.btnCapture.Name = "btnCapture"
        Me.btnCapture.Size = New System.Drawing.Size(84, 26)
        Me.btnCapture.TabIndex = 2
        Me.btnCapture.Text = "Capture"
        Me.btnCapture.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(12, 46)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(647, 349)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(214, 20)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Stream tidak terkoneksi..."
        '
        'btnDetect
        '
        Me.btnDetect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDetect.Location = New System.Drawing.Point(315, 405)
        Me.btnDetect.Name = "btnDetect"
        Me.btnDetect.Size = New System.Drawing.Size(84, 26)
        Me.btnDetect.TabIndex = 5
        Me.btnDetect.Text = "Detect Image"
        Me.btnDetect.UseVisualStyleBackColor = True
        '
        'txtScreen1
        '
        Me.txtScreen1.Location = New System.Drawing.Point(419, 405)
        Me.txtScreen1.Multiline = True
        Me.txtScreen1.Name = "txtScreen1"
        Me.txtScreen1.Size = New System.Drawing.Size(170, 26)
        Me.txtScreen1.TabIndex = 6
        '
        'btnSave
        '
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(595, 405)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(64, 26)
        Me.btnSave.TabIndex = 7
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txtUrl
        '
        Me.txtUrl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUrl.Location = New System.Drawing.Point(77, 11)
        Me.txtUrl.Multiline = True
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.Size = New System.Drawing.Size(582, 26)
        Me.txtUrl.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "RTSP URL:"
        '
        'rtspStream
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(671, 444)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtUrl)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtScreen1)
        Me.Controls.Add(Me.btnDetect)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnCapture)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnStart)
        Me.Name = "rtspStream"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "rtspStream"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnStart As Button
    Friend WithEvents btnStop As Button
    Friend WithEvents btnCapture As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnDetect As Button
    Friend WithEvents txtScreen1 As TextBox
    Friend WithEvents btnSave As Button
    Friend WithEvents txtUrl As TextBox
    Friend WithEvents Label2 As Label
End Class
