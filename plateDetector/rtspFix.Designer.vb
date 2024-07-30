<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rtspFix
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtUrl = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtScreen1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnCapture = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "RTSP URL:"
        '
        'txtUrl
        '
        Me.txtUrl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUrl.Location = New System.Drawing.Point(79, 12)
        Me.txtUrl.Multiline = True
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.Size = New System.Drawing.Size(281, 26)
        Me.txtUrl.TabIndex = 18
        '
        'btnSave
        '
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(813, 12)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(79, 26)
        Me.btnSave.TabIndex = 17
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txtScreen1
        '
        Me.txtScreen1.Location = New System.Drawing.Point(501, 12)
        Me.txtScreen1.Multiline = True
        Me.txtScreen1.Name = "txtScreen1"
        Me.txtScreen1.Size = New System.Drawing.Size(306, 26)
        Me.txtScreen1.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(25, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(214, 20)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Stream tidak terkoneksi..."
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(14, 47)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(436, 349)
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'btnCapture
        '
        Me.btnCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCapture.Location = New System.Drawing.Point(366, 12)
        Me.btnCapture.Name = "btnCapture"
        Me.btnCapture.Size = New System.Drawing.Size(84, 26)
        Me.btnCapture.TabIndex = 12
        Me.btnCapture.Text = "Capture"
        Me.btnCapture.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Location = New System.Drawing.Point(456, 47)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(436, 349)
        Me.PictureBox2.TabIndex = 20
        Me.PictureBox2.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(456, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Output:"
        '
        'rtspFix
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(906, 406)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtUrl)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtScreen1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnCapture)
        Me.Name = "rtspFix"
        Me.Text = "rtspFix"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents txtUrl As TextBox
    Friend WithEvents btnSave As Button
    Friend WithEvents txtScreen1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnCapture As Button
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label3 As Label
End Class
