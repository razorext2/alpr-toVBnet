<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mainMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mainMenu))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnRTSP = New System.Windows.Forms.Button()
        Me.btnUpload = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(82, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(181, 48)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Automatic License" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Plate Recognition"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnRTSP
        '
        Me.btnRTSP.BackColor = System.Drawing.Color.SteelBlue
        Me.btnRTSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRTSP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRTSP.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnRTSP.Location = New System.Drawing.Point(12, 80)
        Me.btnRTSP.Name = "btnRTSP"
        Me.btnRTSP.Size = New System.Drawing.Size(144, 44)
        Me.btnRTSP.TabIndex = 1
        Me.btnRTSP.Text = "RTSP"
        Me.btnRTSP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRTSP.UseVisualStyleBackColor = False
        '
        'btnUpload
        '
        Me.btnUpload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpload.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpload.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnUpload.Location = New System.Drawing.Point(12, 142)
        Me.btnUpload.Name = "btnUpload"
        Me.btnUpload.Size = New System.Drawing.Size(144, 44)
        Me.btnUpload.TabIndex = 2
        Me.btnUpload.Text = "Image Upload"
        Me.btnUpload.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUpload.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.SteelBlue
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button1.Location = New System.Drawing.Point(12, 202)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(144, 44)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "From TXT"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.SteelBlue
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button2.Location = New System.Drawing.Point(12, 262)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(144, 44)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Camera"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.UseVisualStyleBackColor = False
        '
        'mainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(338, 318)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnUpload)
        Me.Controls.Add(Me.btnRTSP)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "mainMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Menu Utama"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btnRTSP As Button
    Friend WithEvents btnUpload As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class
